# MyFirstRepository
It's my first repository, I think I don't want to tell something else

# hh.ru Vacancy Parser (C#)

A console application that fetches C# developer vacancies from hh.ru API, parses JSON, and displays results.

## Features
- Send HTTP request to hh.ru API with User-Agent header
- Parse JSON response using System.Text.Json
- Display vacancies as a numbered list
- Save raw JSON to `jobs.json`
- Can be compiled into a standalone `.exe`

## Tech stack
- C# / .NET 10
- HttpClient
- System.Text.Json
- async / await
- LINQ

## How to run

### Option 1: Run from source
```bash
dotnet run
