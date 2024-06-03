using MyaFinance.MVVM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyaFinance.MVVM.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public List<User> Users { get; set; }

        public User CurrentUser { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
