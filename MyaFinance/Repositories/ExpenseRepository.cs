using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MyaFinance.MVVM.Models;

namespace MyaFinance.Repositories
{
    public class ExpenseRepository
    {
        SQLiteConnection connection;
        public string StatusMessage { get; set; }

        public ExpenseRepository()
        {
            connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
            connection.CreateTable<Expense>();
        }

        public int Add(Expense expense)
        {
            int result = 0;
            try
            {
                result = connection.Insert(expense);
                StatusMessage = $"{result} rows affected by Insert";
                return result;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error : {ex.Message}";
            }
            return result;
        }

        public List<Expense> GetAll(int userId)
        {
            try
            {
                return connection.Table<Expense>().Where(x => x.UserId == userId).ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error : {ex.Message}";
            }
            return null;
        }

        public Expense Get(int expenseId)
        {
            try
            {
                return connection.Table<Expense>().FirstOrDefault(x => x.Id == expenseId);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error : {ex.Message}";
            }
            return null;
        }

        public void Delete(int expenseId)
        {
            try
            {
                var expense = Get(expenseId);
                connection.Delete(expense);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error : {ex.Message}";
            }
        }

        public void Update(Expense expense)
        {
            try
            {
                connection.Update(expense);
                StatusMessage = "Income updated successfully";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error : {ex.Message}";
            }
        }
    }
}

