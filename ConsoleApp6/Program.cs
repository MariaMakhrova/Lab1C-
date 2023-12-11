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
			Console.Write("Введіть частину корпусу (наприклад, 'А%'): ");
			string inputB = Console.ReadLine();
			string queryB = "SELECT * FROM гуртожиток WHERE корпус LIKE @Corpus";
			ExecuteQueryWithParameter(connection, queryB, "@Corpus", inputB);

			// c) Запит зі складним критерієм
			Console.Write("Введіть мінімальну суму оплати: ");
			decimal minPayment = Convert.ToDecimal(Console.ReadLine());
			string queryC = "SELECT * FROM гуртожиток WHERE оплата > @MinPayment AND умови = 'Комфорт'";
			ExecuteQueryWithParameter(connection, queryC, "@MinPayment", minPayment);

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
			Console.Write("Введіть нову суму оплати для корпусу 'Б': ");
			decimal newPaymentAmount = Convert.ToDecimal(Console.ReadLine());
			string queryH = "UPDATE гуртожиток SET оплата = @NewPayment WHERE корпус = 'Б'";
			ExecuteNonQueryWithParameter(connection, queryH, "@NewPayment", newPaymentAmount);

			// Приклад іншого запиту після модифікації
			string queryAfterUpdate = "SELECT * FROM гуртожиток";
			ExecuteQuery(connection, queryAfterUpdate);
		}
	}

	static void ExecuteQueryWithParameter(SqlConnection connection, string query, string parameterName, object parameterValue)
	{
		using (SqlCommand command = new SqlCommand(query, connection))
		{
			command.Parameters.AddWithValue(parameterName, parameterValue);

			using (SqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{

					Console.WriteLine($"{reader["корпус"]}, {reader["кімнати"]}, {reader["студенти"]}, {reader["оплата"]}, {reader["умови"]}");
				}
			}
			Console.WriteLine();
		}
	}

	static void ExecuteNonQueryWithParameter(SqlConnection connection, string query, string parameterName, object parameterValue)
	{
		using (SqlCommand command = new SqlCommand(query, connection))
		{
			command.Parameters.AddWithValue(parameterName, parameterValue);

			int rowsAffected = command.ExecuteNonQuery();
			Console.WriteLine($"Змінено {rowsAffected} рядків.");
		}
	}

}