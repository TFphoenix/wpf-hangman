using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.CompilerServices;
using Hangman.Models;
using Hangman.ViewModels;

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for GameWonView.xaml
    /// </summary>
    public partial class InGameMessageBox : UserControl
    {
        public enum Type
        {
            WonGame,
            LostGame,
            EndGame,
            ChangeCategories
        }

        // Links
        private readonly MainWindow mainWindow;
        private LoginVM loginViewModel;

        // Ctor
        public InGameMessageBox()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            InitializeComponent();
        }

        public void Show(Type type)
        {
            loginViewModel = mainWindow.LoginView.DataContext as LoginVM;

            switch (type)
            {
                case Type.WonGame:
                    Icon.Source = new BitmapImage(ResourceReference.GetIconUri("face_won"));
                    Title.Content = "GAME WON";
                    Message.Content = "What do you want to do next?";

                    PositiveButton.Content = "Play again";
                    PositiveButton.Click += PlayAgain;
                    NegativeButton.Content = "Logout";
                    NegativeButton.Click += Logout;
                    AuxiliaryButton.Content = "Exit game";
                    AuxiliaryButton.Click += ExitGame;
                    break;
                case Type.LostGame:
                    Icon.Source = new BitmapImage(ResourceReference.GetIconUri("face_lost"));
                    Title.Content = "GAME LOST";
                    Message.Content = "What do you want to do next?";

                    PositiveButton.Content = "Play again";
                    PositiveButton.Click += PlayAgain;
                    NegativeButton.Content = "Logout";
                    NegativeButton.Click += Logout;
                    AuxiliaryButton.Content = "Exit game";
                    AuxiliaryButton.Click += ExitGame;
                    break;
                case Type.EndGame:
                    Icon.Source = new BitmapImage(ResourceReference.GetIconUri("save"));
                    Title.Content = "End Game";
                    Message.Content = "Would you like to save your current game?\n(IF NOT, IT WILL BE SCORED AS LOST)";

                    PositiveButton.Content = "Yes";
                    PositiveButton.Click += EndGameSave;
                    NegativeButton.Content = "No";
                    NegativeButton.Click += EndGameNoSave;
                    AuxiliaryButton.Content = "Cancel";
                    AuxiliaryButton.Click += Cancel;
                    break;
                case Type.ChangeCategories:
                    Icon.Source = new BitmapImage(ResourceReference.GetIconUri("categories"));
                    Title.Content = "Change Categories";
                    Message.Content = "Would you like to change categories?\n(CURRENT GAME WILL BE SCORED AS LOST)";

                    PositiveButton.Content = "Yes";
                    PositiveButton.Click += ChangeCategories;
                    NegativeButton.Content = "No";
                    NegativeButton.Click += Cancel;
                    AuxiliaryButton.Content = "Cancel";
                    AuxiliaryButton.Click += Cancel;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            this.Visibility = Visibility.Visible;
        }

        private void PlayAgain(object sender, RoutedEventArgs e)
        {
            loginViewModel.SaveActiveUserScore();
            this.Visibility = Visibility.Hidden;
            mainWindow.ToCategories();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            loginViewModel.SaveActiveUserScore();
            this.Visibility = Visibility.Hidden;
            mainWindow.ToLogin();
        }

        private void ExitGame(object sender, RoutedEventArgs e)
        {
            loginViewModel.SaveActiveUserScore();
            mainWindow.Close();
        }

        private void EndGameSave(object sender, RoutedEventArgs e)
        {
            loginViewModel.SaveActiveUserGame();
            this.Visibility = Visibility.Hidden;
            mainWindow.ToLogin();
        }

        private void EndGameNoSave(object sender, RoutedEventArgs e)
        {
            SessionSetUp.ActiveUser.Score.LostGames += 1;
            loginViewModel.SaveActiveUserScore();
            this.Visibility = Visibility.Hidden;
            mainWindow.ToLogin();
        }

        private void ChangeCategories(object sender, RoutedEventArgs e)
        {
            SessionSetUp.ActiveUser.Score.LostGames += 1;
            loginViewModel.SaveActiveUserScore();
            this.Visibility = Visibility.Hidden;
            mainWindow.ToCategories();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
