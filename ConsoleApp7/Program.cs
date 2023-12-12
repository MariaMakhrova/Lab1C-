using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

class Program
{
	static void Main()
	{
		string connectionString = "Data Source=(local);Initial Catalog=<назва_групи>_<прізвище_студента>;Integrated Security=True";

		using (var dbContext = new YourDbContext(connectionString))
		{
			// a) Простий запит на вибірку
			var resultA = dbContext.Гуртожиток.ToList();
			DisplayResults(resultA);

			// b) Запит на вибірку з використанням спеціальних функцій
			Console.Write("Введіть значення для параметра корпус: ");
			string корпусValueB = Console.ReadLine();
			var resultB = dbContext.Гуртожиток.Where(g => EF.Functions.Like(g.Корпус, $"{корпусValueB}%")).ToList();
			DisplayResults(resultB);

			// c) Запит зі складним критерієм
			Console.Write("Введіть значення для параметра оплата: ");
			int оплатаValueC = int.Parse(Console.ReadLine());
			Console.Write("Введіть значення для параметра умови: ");
			string умовиValueC = Console.ReadLine();
			var resultC = dbContext.Гуртожиток.Where(g => g.Оплата > оплатаValueC && g.Умови == умовиValueC).ToList();
			DisplayResults(resultC);

		}
	}


	static void DisplayResults(System.Collections.Generic.List<Гуртожиток> results)
	{
		foreach (var item in results)
		{
			Console.WriteLine($"{item.Корпус}, {item.Кімнати}, {item.Студенти}, {item.Оплата}, {item.Умови}");
		}
		Console.WriteLine();
	}
}

public class YourDbContext : DbContext
{
	public YourDbContext(string connectionString) : base(new DbContextOptionsBuilder<YourDbContext>().UseSqlServer(connectionString).Options)
	{
	}

	public DbSet<Гуртожиток> Гуртожиток { get; set; }
}
public class Гуртожиток
{
	public string Корпус { get; set; }
	public int Кімнати { get; set; }
	public int Студенти { get; set; }
	public int Оплата { get; set; }
	public string Умови { get; set; }
}