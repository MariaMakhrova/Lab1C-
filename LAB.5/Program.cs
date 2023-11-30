using System;
using System.Data.SqlClient;

class Program
{
	static void Main()
	{
		string connectionString = "Data Source=(local);Initial Catalog=<назва_групи>_<прізвище_студента>;Integrated Security=True";

		using (SqlConnection connection = new SqlConnection(connectionString))
		{
			connection.Open();

			// a) Простий запит на вибірку
			string queryA = "SELECT * FROM гуртожиток";
			ExecuteQuery(connection, queryA);

			// b) Запит на вибірку з використанням спеціальних функцій
			string queryB = "SELECT * FROM гуртожиток WHERE корпус LIKE 'А%'";
			ExecuteQuery(connection, queryB);

			// c) Запит зі складним критерієм
			string queryC = "SELECT * FROM гуртожиток WHERE оплата > 500 AND умови = 'Комфорт'";
			ExecuteQuery(connection, queryC);

			// d) Запит з унікальними значеннями
			string queryD = "SELECT DISTINCT корпус FROM гуртожиток";
			ExecuteQuery(connection, queryD);

			// e) Запит з використанням обчислювального поля
			string queryE = "SELECT корпус, кімнати, студенти, оплата, умови, (оплата * 0.1) AS знижка FROM гуртожиток";
			ExecuteQuery(connection, queryE);

			// f) Запит з групуванням
			string queryF = "SELECT корпус, COUNT(*) AS кількість_місць FROM гуртожиток GROUP BY корпус";
			ExecuteQuery(connection, queryF);

			// g) Запит із сортуванням
			string queryG = "SELECT * FROM гуртожиток ORDER BY оплата ASC";
			ExecuteQuery(connection, queryG);

			// h) Запит з використанням дій по модифікації записів
			string queryH = "UPDATE гуртожиток SET оплата = 600 WHERE корпус = 'Б'";
			ExecuteNonQuery(connection, queryH);

			// Приклад іншого запиту після модифікації
			string queryAfterUpdate = "SELECT * FROM гуртожиток";
			ExecuteQuery(connection, queryAfterUpdate);
		}
	}

	static void ExecuteQuery(SqlConnection connection, string query)
	{
		using (SqlCommand command = new SqlCommand(query, connection))
		using (SqlDataReader reader = command.ExecuteReader())
		{
			while (reader.Read())
			{
				// Обробка результатів запиту
				Console.WriteLine($"{reader["корпус"]}, {reader["кімнати"]}, {reader["студенти"]}, {reader["оплата"]}, {reader["умови"]}");
			}
		}
		Console.WriteLine();
	}

	static void ExecuteNonQuery(SqlConnection connection, string query)
	{
		using (SqlCommand command = new SqlCommand(query, connection))
		{
			int rowsAffected = command.ExecuteNonQuery();
			Console.WriteLine($"Змінено {rowsAffected} рядків.");
		}
	}
}
