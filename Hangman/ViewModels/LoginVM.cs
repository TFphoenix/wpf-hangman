using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using Hangman.Commands;
using Hangman.Models;

namespace Hangman.ViewModels
{
    [Serializable]
    public class LoginVM : ViewModel
    {
        private readonly MainWindow mainWindow;

        [XmlArray]
        private ObservableCollection<User> userList;
        private User selectedUser;

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

        [XmlIgnore]
        public User SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand MainMenuCommand { get; }
        public RelayCommand ContinueCommand { get; }
        public RelayCommand NewUserCommand { get; }
        public RelayCommand DeleteUserCommand { get; }

        public LoginVM()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;

            MainMenuCommand = new RelayCommand(MainMenu);
            ContinueCommand = new RelayCommand(Continue, IsUserSelected);
            NewUserCommand = new RelayCommand(NewUser);
            DeleteUserCommand = new RelayCommand(DeleteUser, IsUserSelected);
        }

        public void SaveActiveUserScore()
        {
            int index = 0;
            foreach (var user in UserList)
            {
                if (user.Id == SessionSetUp.ActiveUser.Id)
                {
                    user.Score = SessionSetUp.ActiveUser.Score;
                    user.InGame = false;
                    mainWindow.LoginView.SerializeUsers();
                    return;
                }
                ++index;
            }
        }

        public void SaveActiveUserGame()
        {
            int index = 0;
            foreach (var user in UserList)
            {
                if (user.Id == SessionSetUp.ActiveUser.Id)
                {
                    user.Saves = SessionSetUp.ActiveUser.Saves;
                    user.InGame = true;
                    mainWindow.LoginView.SerializeUsers();
                    return;
                }
                ++index;
            }
        }

        private void NewUser(object obj)
        {
            mainWindow.ToNewUser();
        }

        private void DeleteUser(object obj)
        {
            int index = 0;
            foreach (var user in UserList)
            {
                if (user.Id == SelectedUser.Id)
                {
                    UserList.RemoveAt(index);
                    break;
                }
                ++index;
            }

            SelectedUser = null;
            mainWindow.LoginView.SerializeUsers();
        }

        private bool IsUserSelected(object obj)
        {
            return SelectedUser != null;
        }

        private void Continue(object obj)
        {
            SessionSetUp.ActiveUser = SelectedUser;

            if (SessionSetUp.ActiveUser.InGame)
                mainWindow.ToSavedGame();
            else
                mainWindow.ToCategories();
        }

        private void MainMenu(object obj)
        {
            mainWindow.ToMenu();
        }
    }
}
