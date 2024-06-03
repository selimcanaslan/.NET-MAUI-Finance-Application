using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyaFinance.MVVM.Models
{
    [Table("Users")]
    public class User
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
        [Column("name"), Indexed, NotNull]
        public string Name {  get; set; }
        [Unique, NotNull, Column("email")]
        public string Email { get; set; }
        [NotNull, Column("password")]
        public string Password { get; set; }
    }
}
