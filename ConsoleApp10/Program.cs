using System;

class Program
{
	static void Main()
	{
		Console.WriteLine("Виберіть спосіб авторизації:");
		Console.WriteLine("1. Логін і пароль");
		Console.WriteLine("2. Авторизація кодом із СМС");
		Console.WriteLine("3. Авторизація за допомогою зовнішнього серверу");

		string choice = Console.ReadLine();

		switch (choice)
		{
			case "1":
				if (LoginWithUsernameAndPassword())
				{
					Console.WriteLine("Привіт!");
				}
				else
				{
					Console.WriteLine("Невірний логін або пароль.");
				}
				break;

			case "2":
				if (LoginWithSMSCode())
				{
					Console.WriteLine("Привіт!");
				}
				else
				{
					Console.WriteLine("Невірний код із СМС.");
				}
				break;

			case "3":
				if (LoginWithExternalServer())
				{
					Console.WriteLine("Привіт!");
				}
				else
				{
					Console.WriteLine("Помилка авторизації на зовнішньому сервері.");
				}
				break;

			default:
				Console.WriteLine("Невірний вибір способу авторизації.");
				break;
		}
	}

	static bool LoginWithUsernameAndPassword()
	{
		Console.Write("Введіть логін: ");
		string enteredUsername = Console.ReadLine();

		Console.Write("Введіть пароль: ");
		string enteredPassword = Console.ReadLine();

		return enteredUsername == "example_user" && enteredPassword == "example_password";
	}

	static bool LoginWithSMSCode()
	{
		string sentCode = "123456";

		Console.Write("Введіть код із СМС: ");
		string enteredCode = Console.ReadLine();

		return enteredCode == sentCode;
	}

	static bool LoginWithExternalServer()
	{
		Console.Write("Виберіть сервіс (Гугл, Фейсбук чи Дія): ");
		string selectedService = Console.ReadLine();

		Console.Write("Введіть логін: ");
		string enteredUsername = Console.ReadLine();

		return true;
	}
}