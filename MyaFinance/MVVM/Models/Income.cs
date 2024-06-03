using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyaFinance.MVVM.Models
{
    [Table("Income")]
    public class Income
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
        [Column("title"), NotNull]
        public string Title { get; set; }

        [Column("description"), NotNull]
        public string Description { get; set; }

        [Column("amount"), NotNull]
        public double Amount { get; set; }

        [Column("date"), NotNull]
        public DateTime Date { get; set; }

        [Column("userId"), NotNull]
        public int UserId { get; set; }
    }
}
