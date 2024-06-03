using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyaFinance.MVVM.Models;

namespace MyaFinance.Repositories
{
    public class UserRepository
    {
        SQLiteConnection connection;
        public string StatusMessage { get; set; }
        public UserRepository()
        {
            connection =
                new SQLiteConnection(Constants.DatabasePath, Constants.Flags);

            connection.CreateTable<User>();
        }
        public int Add(User user)
        {
            int result = 0;
            try
            {
                result = connection.Insert(user);
                StatusMessage = $"{result} rows affected by Insert";
                return result;
            }
            catch (Exception ex) { StatusMessage = $"Error : {ex.Message}"; }
            return result;
        }

        public List<User> GetAll()
        {
            try
            {
                return connection.Table<User>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error : {ex.Message}";
            }
            return null;
        }

        public List<User> GetAll2()
        {
            try
            {
                return connection.Query<User>("SELECT * FROM Users").ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error : {ex.Message}";
            }
            return null;
        }

        public User Get(string mail)
        {
            try
            {
                return connection.Table<User>()
                    .FirstOrDefault(x => x.Email == mail);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error : {ex.Message}";
            }
            return null;
        }

        public void AddOrUpdate(User user)
        {
            int result = 0;
            try
            {
                if (user.Id != 0)
                {
                    result =
                        connection.Update(user);
                    StatusMessage = $"{result} rows affected by update";
                }
                else
                {
                    result = connection.Insert(user);
                    StatusMessage = $"{result} rows affected by insert";
                }

            }
            catch (Exception ex) { StatusMessage = $"Error : {ex.Message}"; }
        }

        public void Delete(string userMail)
        {
            try
            {
                var user =
                    Get(userMail);
                connection.Delete(user);
            }
            catch (Exception ex) { StatusMessage = $"Error : {ex.Message}"; }
        }
    }
}
