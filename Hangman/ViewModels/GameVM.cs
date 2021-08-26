using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Hangman.Commands;
using Hangman.Models;
using Hangman.Views;

namespace Hangman.ViewModels
{
    class GameVM : ViewModel
    {
        //View Link
        private readonly GameView View;

        //Words
        private WordsCollection activeWordsCollection;
        private string activeWordUncensored;
        private string activeWordCensored;

        //Game State
        private User activeUser;
        private int mistakes;
        private const int maxMistakes = 6;
        private int level;
        private const int maxLevel = 5;
        private int timer;
        private string gameAnimation;
        private bool[] lettersState;

        //Timer
        private readonly DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private const int timerSeconds = 60;
        private int timerDelay;
        private DateTime timerDeadline;

        // Properties
        public string ActiveWord
        {
            get => activeWordCensored;
            set
            {
                if (activeWordCensored != value)
                {
                    activeWordCensored = value;
                    OnPropertyChanged();
                }
            }
        }
        public User ActiveUser
        {
            get => activeUser;
            set
            {
                activeUser = value;
                OnPropertyChanged();
            }
        }
        public int Mistakes
        {
            get => mistakes;
            set
            {
                if (mistakes != value)
                {
                    mistakes = value;
                    GameAnimation = ResourceReference.GetGameAnimationPath(mistakes);
                    OnPropertyChanged();
                }
            }
        }
        public int MaxMistakes => maxMistakes;
        public int Level
        {
            get => level;
            set
            {
                if (level != value)
                {
                    level = value;
                    OnPropertyChanged();
                }
            }
        }
        public int MaxLevel => maxLevel;
        public int Timer
        {
            get => timer;
            set
            {
                if (timer != value)
                {
                    timer = value;
                    OnPropertyChanged();
                }
            }
        }
        public string GameAnimation
        {
            get => gameAnimation;
            set
            {
                if (gameAnimation != value)
                {
                    gameAnimation = value;
                    OnPropertyChanged();
                }
            }
        }

        // Commands
        public RelayCommand ChooseLetterCommand { get; }
        public RelayCommand ChangeCategoriesCommand { get; }
        public RelayCommand EndGameCommand { get; }

        // Ctor
        public GameVM(GameView view)
        {
            // View Link
            View = view;

            // Timer
            dispatcherTimer.Tick += dispatcherTimer_Tick;

            // Commands
            ChooseLetterCommand = new RelayCommand(ChooseLetter, CanInteractLetter);
            ChangeCategoriesCommand = new RelayCommand(ChangeCategories, CanInteractButton);
            EndGameCommand = new RelayCommand(EndGame, CanInteractButton);
        }

        public void InitializeGameSession()
        {
            // Session Set Up
            ActiveUser = SessionSetUp.ActiveUser;
            activeWordsCollection = new WordsCollection(SessionSetUp.ActiveCategories);

            // Level Initialization
            Level = 0;
            InitializeLevel();
        }

        private void InitializeLevel()
        {
            // Word Initialization
            InitializeActiveWord();

            // Mistakes Initialization
            Mistakes = 0;

            // Word State Initialization
            lettersState = new bool[26];

            // Game Animation Initialization
            GameAnimation = ResourceReference.GetGameAnimationPath(Mistakes);

            // Start Timer
            StartTimer();
        }

        public void LoadGameSession()
        {
            // Session Set Up
            ActiveUser = SessionSetUp.ActiveUser;
            activeWordsCollection = new WordsCollection(SessionSetUp.ActiveCategories);

            // Level Loading
            LoadActiveWord();
            Mistakes = SessionSetUp.ActiveUser.Saves.Mistakes;
            Level = SessionSetUp.ActiveUser.Saves.Level;
            lettersState = SessionSetUp.ActiveUser.Saves.LettersState;
            GameAnimation = ResourceReference.GetGameAnimationPath(Mistakes);

            // Timer Loading
            StartTimer(SessionSetUp.ActiveUser.Saves.Timer);
        }

        private void InitializeActiveWord()
        {
            activeWordUncensored = activeWordsCollection.GetRandomWord().ToUpper();
            Regex censoringRegex = new Regex("[A-Z]");
            ActiveWord = censoringRegex.Replace(activeWordUncensored, "*");
        }

        private void LoadActiveWord()
        {
            activeWordUncensored = SessionSetUp.ActiveUser.Saves.ActiveWord;
            Regex censoringRegex = new Regex("[A-Z]");
            ActiveWord = censoringRegex.Replace(activeWordUncensored, "*");

            char letter = 'A';
            foreach (bool letterState in SessionSetUp.ActiveUser.Saves.LettersState)
            {
                if (letterState) AddLetterToActiveWord(letter);
                ++letter;
            }
        }

        private void AddLetterToActiveWord(char chosenLetter)
        {
            string oldActiveWordCensored = ActiveWord;
            ActiveWord = "";
            for (int index = 0; index < activeWordUncensored.Length; ++index)
            {
                ActiveWord += activeWordUncensored[index] == chosenLetter ? activeWordUncensored[index] : oldActiveWordCensored[index];
            }
        }

        private bool IsActiveWordCompleted()
        {
            return !ActiveWord.Contains('*');
        }

        private bool WinCondition()
        {
            return Level > MaxLevel;
        }

        private bool LoseCondition()
        {
            return (Mistakes >= MaxMistakes || Timer == 0);
        }

        private void WonGame()
        {
            Level = MaxLevel;
            StopTimer();

            // Score
            SessionSetUp.ActiveUser.Score.WonGames += 1;
            for (int index = 0; index < SessionSetUp.ActiveCategories.Length; ++index)
                if (SessionSetUp.ActiveCategories[index])
                    SessionSetUp.ActiveUser.Score.CategoriesPoints[index] += 1;

            // Message Box
            View.CustomMessageBox.Show(InGameMessageBox.Type.WonGame);
        }

        private void LostGame()
        {
            StopTimer();

            // Score
            SessionSetUp.ActiveUser.Score.LostGames += 1;

            // Message Box
            View.CustomMessageBox.Show(InGameMessageBox.Type.LostGame);
        }

        private bool CanInteractLetter(object param)
        {
            if (LoseCondition() || WinCondition()) return false;
            if (param != null && lettersState[((string)((Button)param).Content)[0] - 65]) return false;
            return true;
        }

        private bool CanInteractButton(object param)
        {
            return !(LoseCondition() || WinCondition());
        }

        private void ChooseLetter(object param)
        {
            char chosenLetter = ((string)((Button)param).Content)[0];
            lettersState[chosenLetter - 65] = true;

            if (activeWordUncensored.Contains(chosenLetter))
            {
                AddLetterToActiveWord(chosenLetter);
                if (IsActiveWordCompleted())
                {
                    ++Level;
                    if (WinCondition())
                    {
                        // Won game
                        WonGame();
                    }
                    else
                    {
                        // Next level
                        InitializeLevel();
                    }
                }
            }
            else
            {
                ++Mistakes;
                if (LoseCondition())
                {
                    // Lost game
                    LostGame();
                }
            }
        }

        private void ChangeCategories(object param)
        {
            View.CustomMessageBox.Show(InGameMessageBox.Type.ChangeCategories);
        }

        private void EndGame(object param)
        {
            SessionSetUp.ActiveUser.Saves = new User.SavesT
            {
                Categories = SessionSetUp.ActiveCategories,
                LettersState = lettersState,
                Level = level,
                Mistakes = mistakes,
                Timer = timer,
                ActiveWord = activeWordUncensored
            };
            View.CustomMessageBox.Show(InGameMessageBox.Type.EndGame);
        }

        // Timer Implementation
        #region Timer

        private void StartTimer(int delay = timerSeconds)
        {
            timerDelay = delay;
            timerDeadline = DateTime.Now.AddSeconds(timerDelay);
            dispatcherTimer.Start();
        }

        private void StopTimer()
        {
            dispatcherTimer.Stop();
            timerDelay = (timerDeadline - DateTime.Now).Seconds;
            timerDeadline = DateTime.Now.AddSeconds(timerDelay);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            int secondsRemaining = (timerDeadline - DateTime.Now).Seconds;
            if (secondsRemaining == 0)
            {
                dispatcherTimer.Stop();
                dispatcherTimer.IsEnabled = false;
                timerDelay = timerSeconds;
                Timer = secondsRemaining;

                // Lost game
                LostGame();
            }
            else
            {
                Timer = secondsRemaining;
            }
        }

        #endregion
    }
}