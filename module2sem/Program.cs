using System;
using System.Data.SqlClient;
public class KioskItem
{
	public string Title { get; set; }
	public string Type { get; set; }
	public int Quantity { get; set; }
	public decimal PricePerCopy { get; set; }
}
class Program
{
	static void Main()
	{
		string connectionString = "connection_string"; 

		using (SqlConnection connection = new SqlConnection(connectionString))
		{
			connection.Open();

			// Зчитування даних
			ReadData(connection);

			// Визначення загальної вартості газет
			decimal totalNewspaperCost = CalculateTotalNewspaperCost(connection);
			Console.WriteLine($"Total cost of newspapers: {totalNewspaperCost:C}");

			// Визначення кількості журналів за вказаним діапазоном цін
			int journalsInPriceRange = CountJournalsInPriceRange(connection, 10, 20); 
			Console.WriteLine($"Number of journals in price range: {journalsInPriceRange}");
		}
	}

	static void ReadData(SqlConnection connection)
	{
		string query = "SELECT * FROM Kiosk";
		using (SqlCommand command = new SqlCommand(query, connection))
		{
			using (SqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					Console.WriteLine($"{reader["Title"]}, {reader["Type"]}, {reader["Quantity"]}, {reader["PricePerCopy"]}");
				}
			}
		}
	}

	static decimal CalculateTotalNewspaperCost(SqlConnection connection)
	{
		string query = "SELECT SUM(Quantity * PricePerCopy) FROM Kiosk WHERE Type = 'Newspaper'";
		using (SqlCommand command = new SqlCommand(query, connection))
		{
			object result = command.ExecuteScalar();
			return result == DBNull.Value ? 0 : (decimal)result;
		}
	}

	static int CountJournalsInPriceRange(SqlConnection connection, decimal minPrice, decimal maxPrice)
	{
		string query = $"SELECT COUNT(*) FROM Kiosk WHERE Type = 'Journal' AND PricePerCopy BETWEEN {minPrice} AND {maxPrice}";
		using (SqlCommand command = new SqlCommand(query, connection))
		{
			return (int)command.ExecuteScalar();
		}
	}
}