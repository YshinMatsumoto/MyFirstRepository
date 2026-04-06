// Основной функционал:
// 1. Запрос к API: https://api.hh.ru/vacancies?text=C%23+developer
// 2. Сохранение в JSON: jobs.json
// 3. Фильтрация через LINQ (по зарплате, городу, ключевым словам)
// 4. Чтение/запись в файлы (история поисков)
// 5. Экспорт в CSV/HTML

using static System.Net.WebRequestMethods;
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

string apiUrl = "https://api.hh.ru/vacancies?text=C%23+developer";
HttpClient client = new HttpClient();
client.DefaultRequestHeaders.UserAgent.ParseAdd("CSharpHHVacancyParser/1.0");
string exit = "0";
string userInput = string.Empty;
await Task.Delay(10); // Делает метод асинхронным

try {
    async Task<string> HttpGet()
    {
        await Task.Delay(2000); // 2 секунды, пока просто пустышка
        string responseBody = await client.GetStringAsync(apiUrl);
        return responseBody;
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
            string json = await HttpGet();
            Console.WriteLine(json.Substring(0, Math.Min(200, json.Length)));
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
} catch (Exception ex)
{
    Console.WriteLine($"Ошибка! {ex.Message}");
}

Console.WriteLine("Нажмите Enter чтобы выйти...");
Console.ReadLine();
