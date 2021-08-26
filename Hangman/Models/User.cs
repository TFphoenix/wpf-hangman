using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hangman.Models
{
    [Serializable]
    public class User
    {
        [Serializable]
        public class ScoreT
        {
            [XmlAttribute]
            public int WonGames;
            [XmlAttribute]
            public int LostGames;
            [XmlIgnore]
            public int TotalGames => WonGames + LostGames;
            [XmlIgnore]
            public string GameStatistics => $"Won: {WonGames} / Lost: {LostGames} / Total: {TotalGames}";
            [XmlAttribute]
            public int[] CategoriesPoints = new int[categoriesCount];

            [XmlIgnore]
            public string CategoriesPointsStatistics
            {
                get
                {
                    if (CategoriesPoints == null)
                        return "No points recorded";

                    string statistics = "";
                    int index = 0;
                    foreach (Categories category in Enum.GetValues(typeof(Categories)))
                    {
                        statistics += $"{category.ToString()}: {CategoriesPoints[index]}, ";
                        ++index;
                    }

                    statistics = statistics.Remove(statistics.Length - 2, 2);
                    return statistics;
                }
            }
        }

        [Serializable]
        public class SavesT
        {
            [XmlAttribute]
            public int Level;
            [XmlAttribute]
            public int Mistakes;
            [XmlAttribute]
            public int Timer;
            [XmlAttribute]
            public bool[] Categories = new bool[categoriesCount];
            [XmlAttribute]
            public bool[] LettersState = new bool[26];
            [XmlAttribute]
            public string ActiveWord;
        }

        // Static Fields
        private static int nextId = Properties.Settings.Default.NextUserId;
        private static int categoriesCount = Enum.GetNames(typeof(Categories)).Length;

        // Fields
        [XmlAttribute]
        private int id;
        [XmlAttribute]
        private string name;
        [XmlAttribute]
        private int imageIndex;
        [XmlAttribute]
        private bool inGame;
        [XmlAttribute]
        private ScoreT score;
        [XmlAttribute]
        private SavesT saves;

        // Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public int ImageIndex { get; set; }
        public string ImageSource => $"{ResourceReference.ProfilePicturesStr}/{ImageIndex}.png";
        public bool InGame { get; set; }
        public ScoreT Score { get; set; }
        public SavesT Saves { get; set; }

        // Ctor
        public User(string name, int imageIndex, bool inGame = false)
        {
            Id = Properties.Settings.Default.NextUserId++;
            Properties.Settings.Default.Save();
            Name = name;
            ImageIndex = imageIndex;
            InGame = inGame;
            Score = new ScoreT();
        }

        // Default Ctor
        public User()
        {
            Score = new ScoreT();
        }

        #region Op. Overloads

        private sealed class IdEqualityComparer : IEqualityComparer<User>
        {
            public bool Equals(User x, User y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.id == y.id;
            }

            public int GetHashCode(User obj)
            {
                return obj.id;
            }
        }

        public bool Equals(User other)
        {
            return id == other.id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User)obj);
        }

        public override int GetHashCode()
        {
            return id;
        }

        #endregion
    }
}
