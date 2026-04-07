// Основной функционал:
// 1. Запрос к API: https://api.hh.ru/vacancies?text=C%23+developer
// 2. Сохранение в JSON: jobs.json
// 3. Фильтрация через LINQ (по зарплате, городу, ключевым словам)
// 4. Чтение/запись в файлы (история поисков)
// 5. Экспорт в CSV/HTML

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;

// Объявление переменных
string apiUrl = "https://api.hh.ru/vacancies?text=C%23+developer";
HttpClient client = new HttpClient();
client.DefaultRequestHeaders.UserAgent.ParseAdd("CSharpHHVacancyParser/1.0"); // настройка User-Agent
string exit = "0";
string userInput = string.Empty;

await Task.Delay(10); // Делает метод асинхронным

try
{

    async Task<string> FetchVacanciesAsync(HttpClient client, string url)
    {
        // запрос к API и возврат строки
        string responseBody = await client.GetStringAsync(apiUrl);
        return responseBody;
    }

    static void SaveToFile(string path, string content)
    {
        // сохранение в файл
        File.WriteAllText("jobs.json", content);
        string jsonString = File.ReadAllText("jobs.json");
        Console.WriteLine("JSON сохранён в jobs.json");
    }

    static void PrintVacancyNames(string json)
    {
        using (JsonDocument document = JsonDocument.Parse(json))
        {
            JsonElement root = document.RootElement;
            JsonElement items = root.GetProperty("items");

            int counter = 1;
            foreach (JsonElement vacancy in items.EnumerateArray())
            {
                // Вот здесь вставляем твой блок
                if (vacancy.TryGetProperty("name", out JsonElement nameElement))
                {
                    string name = nameElement.GetString();
                    Console.WriteLine($"{counter}. {name}");
                    counter++;
                }
                else
                {
                    Console.WriteLine($"{counter}. (имя не указано)");
                    counter++;
                }
            }
        }
    }

    Console.WriteLine("Добро пожаловать в поисковик вакансий на C# developer!");
    Console.WriteLine("=====================================");

    while (exit != userInput)
    {
        Console.WriteLine("1 - Поиск");
        Console.WriteLine("0 - Выход");

        userInput = Console.ReadLine();

        switch (userInput)
        {
            case "1":
                string json = await FetchVacanciesAsync(client, apiUrl);
                SaveToFile("jobs.json", json);
                PrintVacancyNames(json);
                break;

            case "0":
                Console.WriteLine("=====================================");
                Console.WriteLine("Спасибо что воспользовались нашей программой");
                break;

            default:
                Console.WriteLine("Такой комманды не существует!");
                break;
        }

    }
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка! {ex.Message}");
}

Console.WriteLine("Нажмите Enter чтобы выйти...");
Console.ReadLine();
