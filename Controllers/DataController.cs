using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Data.SqlClient;

namespace Cyber_Security_App.Controllers
{
    public class DataController : Controller
    {
        public DataController()
        {
            
        }

        private readonly string connectionString = "Data Source=CyberSecurityDb.db;";

        [HttpGet]
        public IActionResult VulnerableLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VulnerableLogin(string username, string password)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var query = $"SELECT * FROM Users WHERE Username = '{username}' AND Password = '{password}'";
                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return Ok("Login successful.");
                        }
                        else
                        {
                            return Unauthorized("Invalid username or password.");
                        }
                    }
                }
            }
        }


        [HttpGet]
        public IActionResult SafeLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SafeLogin(string username, string password)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var query = $"SELECT * FROM Users WHERE Username = @username AND Password = @password";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return Ok("Login successful.");
                        }
                        else
                        {
                            return Unauthorized("Invalid username or password.");
                        }
                    }
                }
            }
        }




    }
}
