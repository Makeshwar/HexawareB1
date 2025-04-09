using System;
namespace InsuranceManagementSystem.entity
{
	public class Users
	{
        
            private int userId;

            private string username;

            private string password;

            private string role;

            public int UserId { get => userId; set => userId = value; }
            public string Username { get => username; set => username = value; }
            public string Password { get => password; set => password = value; }
            public string Role { get => role; set => role = value; }

        public Users() { }

        public Users(int userId, string username, string password, string role)
        {
            this.UserId = userId;
            this.Username = username;
            this.Password = password;
            this.Role = role;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"UserId: {UserId}, Username: {Username}, Password: {Password}, Role: {Role}");
        }

    }
}

