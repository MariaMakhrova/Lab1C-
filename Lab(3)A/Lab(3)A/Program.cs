using System;
using System.Xml;

class Program
{
	static void Main()
	{
		// Створюємо XML-файл
		XmlDocument xmlDoc = new XmlDocument();

		XmlElement root = xmlDoc.CreateElement("Zavod");
		xmlDoc.AppendChild(root);

		// Додаємо працівників
		AddEmployee(xmlDoc, root, "Прізвище1", 1, "Посада1", 5, 5000);
		AddEmployee(xmlDoc, root, "Прізвище2", 2, "Посада2", 8, 7000);
		

		// Збереження XML-файлу
		xmlDoc.Save("Zavod.xml");

		// Перегляд файлу на консолі
		Console.WriteLine("Зміст XML-файлу:");
		Console.WriteLine(xmlDoc.InnerXml);

		// Виведення інформації за прізвищем
		string lastNameToSearch = "Прізвище1";
		XmlNode employeeNode = FindEmployee(xmlDoc, lastNameToSearch);

		if (employeeNode != null)
		{
			Console.WriteLine($"\nІнформація за прізвищем {lastNameToSearch}:");
			Console.WriteLine($"Номер цеху: {employeeNode.SelectSingleNode("DepartmentNumber").InnerText}");
			Console.WriteLine($"Посада: {employeeNode.SelectSingleNode("Position").InnerText}");
			Console.WriteLine($"Стаж роботи: {employeeNode.SelectSingleNode("Experience").InnerText}");
			Console.WriteLine($"Заробітна плата: {employeeNode.SelectSingleNode("Salary").InnerText}");
		}
		else
		{
			Console.WriteLine($"\nПрацівник із прізвищем {lastNameToSearch} не знайдений.");
		}

		// Обчислення середньої заробітної плати в цеху Х
		int departmentToCalculateAverage = 1; // Номер цеху для обчислення середньої зарплати
		double averageSalary = CalculateAverageSalary(xmlDoc, departmentToCalculateAverage);
		Console.WriteLine($"\nСередня заробітна плата в цеху {departmentToCalculateAverage}: {averageSalary}");
	}

	// Метод для додавання працівника до XML-файлу
	static void AddEmployee(XmlDocument xmlDoc, XmlElement root, string lastName, int departmentNumber, string position, int experience, int salary)
	{
		XmlElement employee = xmlDoc.CreateElement("Employee");

		XmlElement lastNameElement = xmlDoc.CreateElement("LastName");
		lastNameElement.InnerText = lastName;
		employee.AppendChild(lastNameElement);

		XmlElement departmentNumberElement = xmlDoc.CreateElement("DepartmentNumber");
		departmentNumberElement.InnerText = departmentNumber.ToString();
		employee.AppendChild(departmentNumberElement);

		XmlElement positionElement = xmlDoc.CreateElement("Position");
		positionElement.InnerText = position;
		employee.AppendChild(positionElement);

		XmlElement experienceElement = xmlDoc.CreateElement("Experience");
		experienceElement.InnerText = experience.ToString();
		employee.AppendChild(experienceElement);

		XmlElement salaryElement = xmlDoc.CreateElement("Salary");
		salaryElement.InnerText = salary.ToString();
		employee.AppendChild(salaryElement);

		root.AppendChild(employee);
	}

	// Метод для пошуку працівника за прізвищем
	static XmlNode FindEmployee(XmlDocument xmlDoc, string lastName)
	{
		XmlNodeList employees = xmlDoc.SelectNodes($"//Employee[LastName='{lastName}']");
		return employees.Count > 0 ? employees[0] : null;
	}

	// Метод для обчислення середньої заробітної плати в цеху
	static double CalculateAverageSalary(XmlDocument xmlDoc, int departmentNumber)
	{
		XmlNodeList employeesInDepartment = xmlDoc.SelectNodes($"//Employee[DepartmentNumber='{departmentNumber}']");
		if (employeesInDepartment.Count == 0)
			return 0;

		double totalSalary = 0;
		foreach (XmlNode employeeNode in employeesInDepartment)
		{
			totalSalary += Convert.ToDouble(employeeNode.SelectSingleNode("Salary").InnerText);
		}

		return totalSalary / employeesInDepartment.Count;
	}
}