using System;
using System.Collections.Generic;
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
using Hangman.Models;
using Hangman.ViewModels;

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for SavedGameView.xaml
    /// </summary>
    public partial class SavedGameView : UserControl
    {
        // Links
        private readonly MainWindow mainWindow;
        private LoginVM LoginViewModel => mainWindow.LoginView.DataContext as LoginVM;

        // Ctor
        public SavedGameView()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            InitializeComponent();
        }

        private void ResumeGame_OnClick(object sender, RoutedEventArgs e)
        {
            SessionSetUp.ActiveCategories = SessionSetUp.ActiveUser.Saves.Categories;
            mainWindow.ToGame();
        }

        private void NewGame_OnClick(object sender, RoutedEventArgs e)
        {
            SessionSetUp.ActiveUser.Score.LostGames += 1;
            LoginViewModel.SaveActiveUserScore();
            mainWindow.ToCategories();
        }

        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            mainWindow.ToLogin();
        }
    }
}
