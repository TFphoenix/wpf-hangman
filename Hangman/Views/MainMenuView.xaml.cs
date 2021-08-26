using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Hangman.Models;
using Hangman.ViewModels;

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for MainMenuView.xaml
    /// </summary>
    public partial class MainMenuView : UserControl
    {
        private readonly MainWindow mainWindow;

        public MainMenuView()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            InitializeComponent();

            // Background music
            MainWindow.MusicPlayer.Open(ResourceReference.MenuMusic);
            MainWindow.MusicPlayer.Volume = 0.7f;
            MainWindow.MusicPlayer.Play();
            MainWindow.MusicPlayer.MediaEnded += (s, e) =>
            {
                MainWindow.MusicPlayer.Position = TimeSpan.Zero;
                MainWindow.MusicPlayer.Play();
            };

            // Hangman Animation
            VideoPlayer.Source = ResourceReference.HangmanAnimation;
            VideoPlayer.Play();
            VideoPlayer.MediaEnded += (s, e) =>
            {
                VideoPlayer.Position = TimeSpan.Zero;
                VideoPlayer.Play();
            };
            this.IsVisibleChanged += (s, e) =>
            {
                if (!IsVisible)
                {
                    VideoPlayer.Stop();
                    VideoPlayer.Position = TimeSpan.Zero;
                }
                else
                {
                    VideoPlayer.Play();
                }
            };
        }

        private void NewGame(object sender, RoutedEventArgs e)
        {
            mainWindow.ToLogin();
        }

        private void Statistics(object sender, RoutedEventArgs e)
        {
            mainWindow.ToStatistics();
        }

        private void About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("©Mihăescu Teodor-Adrian\nInformatică-Aplicată\nUNITBV Brașov\nGrupa: 10LF382");
        }

        private void ExitGame(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }
    }
}
