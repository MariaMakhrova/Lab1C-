using System;
using System.Collections.Generic;
using System.Linq;

// Компонент - базовий клас
// Використовуємо структурний шаблон "Декоратор".
interface ISearchEngine
{
	List<string> Search(string query);
}

// Конкретний компонент - реалізація базового класу
class SearchEngine : ISearchEngine
{
	private List<string> dummyResults = new List<string>
	{
		"https://example1.com",
		"https://example2.com",
		"https://example3.com",
        // ... (заглушка повертає однаковий список)
    };

	public List<string> Search(string query)
	{
		Console.WriteLine($"Отримано запит: {query}");
		return dummyResults;
	}
}

// Декоратор - клас, який додає додатковий функціонал
class LoggingDecorator : ISearchEngine
{
	private ISearchEngine searchEngine;
	private List<string> log = new List<string>();

	public LoggingDecorator(ISearchEngine searchEngine)
	{
		this.searchEngine = searchEngine;
	}

	public List<string> Search(string query)
	{
		var results = searchEngine.Search(query);
		log.Add($"{DateTime.Now}: Виконано пошук для запиту '{query}'");
		return results;
	}

	public void DisplayTopQueries(int topN)
	{
		var topQueries = log.GroupBy(q => q)
						   .OrderByDescending(group => group.Count())
						   .Take(topN)
						   .Select(group => $"{group.Key}: {group.Count()} разів");

		Console.WriteLine("ТОП запитів:");
		foreach (var query in topQueries)
		{
			Console.WriteLine(query);
		}
	}
}

class Program
{
	static void Main()
	{
		// Створення базового об'єкта та додавання до нього функціоналу
		ISearchEngine searchEngine = new SearchEngine();
		ISearchEngine decoratedSearchEngine = new LoggingDecorator(searchEngine);

		// Виклик методу з додатковим функціоналом
		var results = decoratedSearchEngine.Search("software development");

		// Вивід ТОП N запитів
		((LoggingDecorator)decoratedSearchEngine).DisplayTopQueries(3);
	}
}