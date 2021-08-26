﻿using System;
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
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        public GameView()
        {
            DataContext = new GameVM(this);
            InitializeComponent();
        }

        public void LoadOrInitializeGameSession()
        {
            if (SessionSetUp.ActiveUser.InGame)
                (DataContext as GameVM).LoadGameSession();
            else
                (DataContext as GameVM).InitializeGameSession();
        }
    }
}
