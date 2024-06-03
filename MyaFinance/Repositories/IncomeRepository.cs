using MyaFinance.MVVM.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyaFinance.Repositories
{
    public class IncomeRepository
    {
        SQLiteConnection connection;
        public string StatusMessage { get; set; }

        public IncomeRepository()
        {
            connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
            connection.CreateTable<Income>();
        }

        public int Add(Income income)
        {
            int result = 0;
            try
            {
                result = connection.Insert(income);
                StatusMessage = $"{result} rows affected by Insert";
                return result;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error : {ex.Message}";
            }
            return result;
        }

        public List<Income> GetAll(int userId)
        {
            try
            {
                return connection.Table<Income>().Where(x => x.UserId == userId).ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error : {ex.Message}";
            }
            return null;
        }

        public Income Get(int incomeId)
        {
            try
            {
                return connection.Table<Income>().FirstOrDefault(x => x.Id == incomeId);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error : {ex.Message}";
            }
            return null;
        }

        public void Delete(int incomeId)
        {
            try
            {
                var income = Get(incomeId);
                connection.Delete(income);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error : {ex.Message}";
            }
        }

        public void Update(Income income)
        {
            try
            {
                connection.Update(income);
                StatusMessage = "Income updated successfully";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error : {ex.Message}";
            }
        }
    }
}
