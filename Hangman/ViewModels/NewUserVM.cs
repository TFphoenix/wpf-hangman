using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Hangman.Commands;
using Hangman.Models;

namespace Hangman.ViewModels
{
    public class NewUserVM : ViewModel
    {
        public struct ProfilePicture
        {
            public const int profilePicturesNumber = 30;
            private readonly int _index;

            public string ImageSource => $"{ResourceReference.ProfilePicturesStr}/{_index}.png";
            public int ImageIndex => _index;

            public ProfilePicture(int index)
            {
                this._index = index;
            }
        }

        private ProfilePicture[] profilePictures = new ProfilePicture[ProfilePicture.profilePicturesNumber]
        {
            new ProfilePicture(1),
            new ProfilePicture(2),
            new ProfilePicture(3),
            new ProfilePicture(4),
            new ProfilePicture(5),
            new ProfilePicture(6),
            new ProfilePicture(7),
            new ProfilePicture(8),
            new ProfilePicture(9),
            new ProfilePicture(10),
            new ProfilePicture(11),
            new ProfilePicture(12),
            new ProfilePicture(13),
            new ProfilePicture(14),
            new ProfilePicture(15),
            new ProfilePicture(16),
            new ProfilePicture(17),
            new ProfilePicture(18),
            new ProfilePicture(19),
            new ProfilePicture(20),
            new ProfilePicture(21),
            new ProfilePicture(22),
            new ProfilePicture(23),
            new ProfilePicture(24),
            new ProfilePicture(25),
            new ProfilePicture(26),
            new ProfilePicture(27),
            new ProfilePicture(28),
            new ProfilePicture(29),
            new ProfilePicture(30)
        };
        public ProfilePicture[] ProfilePictures
        {
            get => profilePictures;
            set
            {
                if (profilePictures != value)
                {
                    profilePictures = value;
                    OnPropertyChanged();
                }
            }
        }
        private ProfilePicture selectedProfilePicture;
        public ProfilePicture SelectedProfilePicture
        {
            get => selectedProfilePicture;
            set
            {
                selectedProfilePicture = value;
                OnPropertyChanged();
            }
        }

        private string selectedUserName;

        public string SelectedUserName
        {
            get => selectedUserName;
            set
            {
                selectedUserName = value;
                OnPropertyChanged();
            }
        }

        private readonly MainWindow mainWindow;
        public RelayCommand CreateUserCommand { get; }
        public RelayCommand BackCommand { get; }

        public NewUserVM()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;

            CreateUserCommand = new RelayCommand(CreateUser, CanCreateUser);
            BackCommand = new RelayCommand(Back);
        }

        private void CreateUser(object obj)
        {
            mainWindow.LoginView.loginVM.UserList.Add(new User(SelectedUserName, SelectedProfilePicture.ImageIndex));
            mainWindow.LoginView.SerializeUsers();
            mainWindow.ToLogin();
            mainWindow.NewUserView.ResetFields();
        }

        private bool CanCreateUser(object obj)
        {
            return !String.IsNullOrEmpty(SelectedUserName);
        }

        private void Back(object obj)
        {
            mainWindow.ToLogin();
            mainWindow.NewUserView.ResetFields();
        }
    }
}
