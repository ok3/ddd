﻿using System;
using System.Configuration;
using System.Data.SqlClient;

namespace _11
{
    class Program
    {
        public void CreateUser(string userName)
        {
            var user = new User(
                new UserName(userName)
            );

            var userService = new UserService();
            if (userService.Exists(user))
            {
                throw new Exception($"{userName}은 이미 존재하는 사용자명임");
            }

            var connectionString = ConfigurationManager.ConnectionStrings["FooConnection"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "INSERT INTO users (id, name) VALUES(@id, @name)";
                command.Parameters.Add(new SqlParameter("@id", user.Id.Value));
                command.Parameters.Add(new SqlParameter("@name", user.Name.Value));
                command.ExecuteNonQuery();
            }
        }
    }
}
