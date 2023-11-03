using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

public class Employee
{
	public string LastName { get; set; }
	public int DepartmentNumber { get; set; }
	public string Position { get; set; }
	public int Experience { get; set; }
	public int Salary { get; set; }
}

public class ZavodData
{
	public List<Employee> Employees { get; set; }
}

class Program
{
	static void Main()
	{
		// Створення JSON-файлу
		ZavodData data = new ZavodData
		{
			Employees = new List<Employee>
			{
				new Employee { LastName = "Прізвище1", DepartmentNumber = 1, Position = "Посада1", Experience = 5, Salary = 5000 },
				new Employee { LastName = "Прізвище2", DepartmentNumber = 2, Position = "Посада2", Experience = 8, Salary = 7000 }
                // Додайте інші працівників за аналогією
            }
		};

		string json = JsonConvert.SerializeObject(data, Formatting.Indented);
		File.WriteAllText("Zavod.json", json);

		// Перегляд JSON-файлу на консолі
		Console.WriteLine("Зміст JSON-файлу:");
		Console.WriteLine(json);

		// Виведення інформації за прізвищем
		string lastNameToSearch = "Прізвище1";
		Employee foundEmployee = GetEmployeeByLastName("Zavod.json", lastNameToSearch);

		if (foundEmployee != null)
		{
			Console.WriteLine($"\nІнформація за прізвищем {lastNameToSearch}:");
			Console.WriteLine($"Номер цеху: {foundEmployee.DepartmentNumber}");
			Console.WriteLine($"Посада: {foundEmployee.Position}");
			Console.WriteLine($"Стаж роботи: {foundEmployee.Experience}");
			Console.WriteLine($"Заробітна плата: {foundEmployee.Salary}");
		}
		else
		{
			Console.WriteLine($"\nПрацівник із прізвищем {lastNameToSearch} не знайдений.");
		}

		// Обчислення середньої заробітної плати в цеху Х
		int departmentToCalculateAverage = 1; // Номер цеху для обчислення середньої зарплати
		double averageSalary = CalculateAverageSalary("Zavod.json", departmentToCalculateAverage);
		Console.WriteLine($"\nСередня заробітна плата в цеху {departmentToCalculateAverage}: {averageSalary}");
	}

	// Метод для читання JSON-файлу та пошуку працівника за прізвищем
	static Employee GetEmployeeByLastName(string filePath, string lastName)
	{
		if (File.Exists(filePath))
		{
			string json = File.ReadAllText(filePath);
			ZavodData data = JsonConvert.DeserializeObject<ZavodData>(json);

			return data.Employees.Find(employee => employee.LastName == lastName);
		}

		return null;
	}

	// Метод для обчислення середньої заробітної плати в цеху
	static double CalculateAverageSalary(string filePath, int departmentNumber)
	{
		if (File.Exists(filePath))
		{
			string json = File.ReadAllText(filePath);
			ZavodData data = JsonConvert.DeserializeObject<ZavodData>(json);

			List<Employee> employeesInDepartment = data.Employees.FindAll(employee => employee.DepartmentNumber == departmentNumber);

			if (employeesInDepartment.Count == 0)
				return 0;

			double totalSalary = employeesInDepartment.Sum(employee => employee.Salary);
			return totalSalary / employeesInDepartment.Count;
		}

		return 0;
	}
}
