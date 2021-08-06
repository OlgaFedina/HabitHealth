
using HealthApp.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthApp.Services
{
    class LoginSignupService
    {
        private string Username;
        private string EnteredPassword;
        private string UserEmail;
        private object username;
        private const string ConnectionString = "Server=database-health.cl29glwnac7r.us-east-2.rds.amazonaws.com; Port=5432; User Id=postgresolga; Password=parfate13stand; Database = postgres";

        public LoginSignupService(string username, string password)
        {
            Username = username;
            EnteredPassword = password;
        }

        public LoginSignupService(string username, string password, string email)
        {
            Username = username;
            EnteredPassword = password;
            UserEmail = email;
        }

        public LoginSignupService(object username)
        {
            this.username = username;
        }

        //TODO test code for DB connection
        public bool VerifyLogin () 
        {
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(ConnectionString);
                connection.Open();
                int rows;
            
                using (var cmd = new NpgsqlCommand("SELECT ISNULL(MAX(HashedPassword), 0) FROM users WHERE username = @Username AND password_hashed = @HashedPass" , connection))
                {
                    cmd.Parameters.AddWithValue("Username", Username);
                    cmd.Parameters.AddWithValue("HashedPass", HashPassword());
                    rows = cmd.ExecuteNonQuery();
                }
                if (rows == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        //TODO test new user addition
        private string AddNewUser()
        {
            DateTime now = DateTime.Now;
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(ConnectionString);
                connection.Open();

                //Checks if username already exists
                using (var cmd = new NpgsqlCommand("SELECT ISNULL(MAX(username), 0) FROM users WHERE username = @Username", connection))
                {
                    cmd.Parameters.AddWithValue("Username", Username);

                    if (cmd.ExecuteNonQuery() == 1)
                        return "ERROR: That username is taken.";
                    else
                    {
                        //proceed to add user to database
                        using (var cmd2 = new NpgsqlCommand("INSERT INTO user (username, password_hashed, account_created, email) VALUES (@Username, @HashedPass, @CreationTime, @Email)", connection))
                        {
                            cmd2.Parameters.AddWithValue("Username", Username);
                            cmd2.Parameters.AddWithValue("HashedPass", HashPassword());
                            cmd2.Parameters.AddWithValue("CreationTime", now);
                            cmd2.Parameters.AddWithValue("Email", UserEmail);
                            if (cmd.ExecuteNonQuery() == 1)
                            {   //in order to verify addition to database the username will be searched for
                                using (var cmd3 = new NpgsqlCommand("SELECT username FROM users WHERE username = @Username", connection))
                                {
                                    cmd3.Parameters.AddWithValue("Username", Username);
                                    
                                    if (cmd3.ExecuteNonQuery() == 1) //if username was found in a row, account created properly
                                       return "SUCCESS: Your account was created.";
                                    else
                                       return "ERROR: Unable to verify account creation.";  
                                }
                            }
                            else
                                return "ERROR: Unable to create account.";
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "ERROR: Unable to reach database.";
            }
        }


        //TODO test hashing
        private string HashPassword()
        {
            System.Security.Cryptography.SHA512Managed sha512 = new System.Security.Cryptography.SHA512Managed();
            string securityCode = "SHJAKEJV35SG56";
            Byte[] EncryptedSHA512 = sha512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(string.Concat(EnteredPassword, securityCode)));

            sha512.Clear();
            return Convert.ToBase64String(EncryptedSHA512);
            
        }
    }
}
