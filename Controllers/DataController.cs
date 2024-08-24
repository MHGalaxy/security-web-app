using Cyber_Security_App.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Cyber_Security_App.Controllers
{
    public class DataController : Controller
    {
        private readonly AppDbContext _context;
        public DataController(AppDbContext context)
        {
            _context = context;
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


        [HttpGet]
        public IActionResult EfSafeLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EfSafeLogin(string username, string password)
        {
            var users = _context.Users
                .Where(u => u.UserName == username && u.Password == password)
                .ToList();

            if(users.Any())
            {
                return Ok("Login successful.");
            }
            else
            {
                return Unauthorized("Invalid username or password.");
            }
        }

        [HttpGet]
        public IActionResult ValidatedLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ValidatedLogin(string username, string password)
        {
            if (!IsValidUsername(username))
            {
                return BadRequest("Invalid input");
            }


            var users = _context.Users
                .Where(u => u.UserName == username && u.Password == password)
                .ToList();

            if (users.Any())
            {
                return Ok("Login successful.");
            }
            else
            {
                return Unauthorized("Invalid username or password.");
            }
        }

        private bool IsValidUsername(string username)
        {
            // چک کردن اینکه نام کاربری فقط شامل حروف و اعداد باشد
            return Regex.IsMatch(username, @"^[a-zA-Z0-9]+$");
        }
    }
}
