using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Hangman.Commands;
using Hangman.Models;

namespace Hangman.ViewModels
{
    public class StatisticsVM : ViewModel
    {
        private readonly MainWindow mainWindow;
        private ObservableCollection<User> userList;

        public ObservableCollection<User> UserList
        {
            get => userList;
            set
            {
                if (userList != value)
                {
                    userList = value;
                    OnPropertyChanged();
                }
            }
        }
        private LoginVM LoginViewModel => mainWindow.LoginView.DataContext as LoginVM;

        public RelayCommand MainMenuCommand { get; }

        public StatisticsVM()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;

            MainMenuCommand = new RelayCommand(MainMenu);
        }

        public void UpdateUserList()
        {
            mainWindow.LoginView.DeserializeUsers();
            UserList = LoginViewModel.UserList;
        }

        private void MainMenu(object param)
        {
            mainWindow.ToMenu();
        }
    }
}
