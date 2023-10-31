using System;
using System.Collections;

class Program
{
	static void Main()
	{
		// Створюємо Hashtable
		Hashtable hashtable = new Hashtable();

		// Додаємо елементи, які описують предметну область
		hashtable.Add("List", "Список");
		hashtable.Add("Dictionary", "Словник");
		hashtable.Add("Queue", "Черга");
		hashtable.Add("Stack", "Стек");

		// Виводимо елементи на консоль у прямому порядку
		Console.WriteLine("Елементи в прямому порядку:");
		PrintHashtable(hashtable);

		// Виводимо елементи на консоль у зворотному порядку
		Console.WriteLine("\nЕлементи в зворотному порядку:");
		PrintHashtableReverse(hashtable);

		// Виводимо кількість елементів у колекції
		Console.WriteLine($"\nКількість елементів у колекції: {hashtable.Count}");

		// Очищаємо колекцію
		hashtable.Clear();

		Console.ReadLine();
	}

	// Функція для виведення елементів Hashtable у прямому порядку
	static void PrintHashtable(Hashtable hashtable)
	{
		foreach (DictionaryEntry entry in hashtable)
		{
			Console.WriteLine($"{entry.Key}: {entry.Value}");
		}
	}

	// Функція для виведення елементів Hashtable у зворотному порядку
	static void PrintHashtableReverse(Hashtable hashtable)
	{
		ArrayList keys = new ArrayList(hashtable.Keys);
		keys.Reverse();

		foreach (var key in keys)
		{
			Console.WriteLine($"{key}: {hashtable[key]}");
		}
	}
}






