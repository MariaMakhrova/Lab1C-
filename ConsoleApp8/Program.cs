using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

class Program
{
	static async Task Main()
	{
		string connectionString = "Data Source=(local);Initial Catalog=<назва_групи>_<прізвище_студента>;Integrated Security=True";

		using (var dbContext = new YourDbContext(connectionString))
		{
			// a) Простий асинхронний запит на вибірку
			var resultA = await dbContext.Гуртожиток.ToListAsync();
			DisplayResults(resultA);

			// b) Асинхронний запит на вибірку з використанням спеціальних функцій
			Console.Write("Введіть значення для параметра корпус: ");
			string корпусValueB = Console.ReadLine();
			var resultB = await dbContext.Гуртожиток
				.Where(g => EF.Functions.Like(g.Корпус, $"{корпусValueB}%"))
				.ToListAsync();
			DisplayResults(resultB);

			// c) Асинхронний запит зі складним критерієм
			Console.Write("Введіть значення для параметра оплата: ");
			int оплатаValueC = int.Parse(Console.ReadLine());
			Console.Write("Введіть значення для параметра умови: ");
			string умовиValueC = Console.ReadLine();
			var resultC = await dbContext.Гуртожиток
				.Where(g => g.Оплата > оплатаValueC && g.Умови == умовиValueC)
				.ToListAsync();
			DisplayResults(resultC);
		}
	}

	static void DisplayResults(List<Гуртожиток> results)
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