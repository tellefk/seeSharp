using System;
using System.Collections.Generic; // Added for List
using System.Data.SqlClient;
using Program.Models; // Change 'YourNamespace' to something appropriate

namespace Program // Change 'YourNamespace' to something appropriate
{
    class Program
    {
        private static List<Computer> computers; // Made static

        private static bool isConnected = false;

        static void Main(string[] args)
        {
            string connectionString = "Data Source=YourServerName;Initial Catalog=YourDatabaseName;User ID=YourUsername;Password=YourPassword";

            if (isConnected)
            {
                ConnectToDatabase(connectionString);
                Console.WriteLine("Connected to SQL Server!");
            }
            else
            {
                Console.WriteLine("Failed to connect to SQL Server!");
                CreateComputers(); // Corrected syntax
            }
        }

        public static void ConnectToDatabase(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connected to SQL Server!");
                    // Database operations...
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to connect to SQL Server: " + ex.Message);
                }
            }
        }

        public static IEnumerable<Computer> CreateComputers()
        {
            if (computers != null)
            {
                return computers;
            }

            Computer computer = new Computer("ASUS", true, false, DateTime.Now, 999.99m, "NVIDIA GeForce RTX 3080");
            Computer computer2 = new Computer("INTEL", true, false, DateTime.Now, 11999.99m, "NVIDIA GeForce RTX 4070");

            computers = new List<Computer> { computer, computer2 };
            computers.Add(new Computer("AMD", true, false, DateTime.Now, 999.99m, "NVIDIA GeForce RTX 3080"));

            foreach (Computer comp in computers)
            {
                comp.PrintComputerDetails();
            }

            return computers;
        }
    }
}
