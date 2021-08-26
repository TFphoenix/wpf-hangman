using System;
using System.Windows;
using System.Windows.Media;
using Hangman.Models;


namespace Hangman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MediaPlayer MusicPlayer = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void ToMenu()
        {
            LoginView.Visibility = Visibility.Hidden;
            GameView.Visibility = Visibility.Hidden;
            StatisticsView.Visibility = Visibility.Hidden;
            MainMenuView.Visibility = Visibility.Visible;
        }

        public void ToLogin()
        {
            LoginView.DeserializeUsers();
            MainMenuView.Visibility = Visibility.Hidden;
            NewUserView.Visibility = Visibility.Hidden;
            SavedGameView.Visibility = Visibility.Hidden;
            CategoriesView.Visibility = Visibility.Hidden;
            GameView.Visibility = Visibility.Hidden;
            LoginView.Visibility = Visibility.Visible;
        }

        public void ToNewUser()
        {
            LoginView.Visibility = Visibility.Hidden;
            NewUserView.Visibility = Visibility.Visible;
        }

        public void ToSavedGame()
        {
            LoginView.Visibility = Visibility.Hidden;
            SavedGameView.Visibility = Visibility.Visible;
        }

        public void ToCategories()
        {
            LoginView.Visibility = Visibility.Hidden;
            SavedGameView.Visibility = Visibility.Hidden;
            GameView.Visibility = Visibility.Hidden;
            CategoriesView.Visibility = Visibility.Visible;
        }

        public void ToGame()
        {
            CategoriesView.Visibility = Visibility.Hidden;
            SavedGameView.Visibility = Visibility.Hidden;
            GameView.CustomMessageBox.Visibility = Visibility.Hidden;
            GameView.Visibility = Visibility.Visible;
            GameView.LoadOrInitializeGameSession();
        }

        public void ToStatistics()
        {
            StatisticsView.UpdateView();
            MainMenuView.Visibility = Visibility.Hidden;
            StatisticsView.Visibility = Visibility.Visible;
        }
    }
}
