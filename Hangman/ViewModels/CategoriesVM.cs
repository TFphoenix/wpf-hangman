using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Hangman.Commands;
using Hangman.Models;

namespace Hangman.ViewModels
{
    public class CategoriesVM : ViewModel
    {
        private readonly MainWindow mainWindow;

        public class CategorySelection : ViewModel
        {
            private bool isSelected;

            public Categories Category { get; set; }
            public bool IsSelected
            {
                get => isSelected;
                set
                {
                    isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        private ObservableCollection<CategorySelection> categoriesList;
        private CategorySelection _selectedCategory;

        public ObservableCollection<CategorySelection> CategoriesList
        {
            get => categoriesList;
            set
            {
                if (categoriesList != value)
                {
                    categoriesList = value;
                    OnPropertyChanged();
                }
            }
        }
        public CategorySelection SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand StartGameCommand { get; }
        public RelayCommand BackCommand { get; }

        public CategoriesVM()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            InitializeCategoriesList();

            StartGameCommand = new RelayCommand(StartGame, CanStartGame);
            BackCommand = new RelayCommand(BackToLogin);
        }

        private void InitializeCategoriesList()
        {
            categoriesList = new ObservableCollection<CategorySelection>();
            foreach (Categories category in Enum.GetValues(typeof(Categories)))
            {
                categoriesList.Add(new CategorySelection { Category = category });
            }
        }

        private void StartGame(object param)
        {
            int index = 0;
            foreach (CategorySelection categorySelection in CategoriesList)
            {
                SessionSetUp.ActiveCategories[index] = categorySelection.IsSelected;
                ++index;
            }
            mainWindow.ToGame();
        }

        private bool CanStartGame(object param)
        {
            return SelectedCategory != null;
        }

        private void BackToLogin(object param)
        {
            mainWindow.ToLogin();
        }
    }
}
