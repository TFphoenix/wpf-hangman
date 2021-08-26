using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Models
{
    public class WordsCollection
    {
        #region STATIC FUNCTIONALITY

        private static readonly Dictionary<Categories, string[]> wordsDictionary = new Dictionary<Categories, string[]>();
        private static readonly int categoriesCount = Enum.GetNames(typeof(Categories)).Length;

        static WordsCollection()
        {
            string[] wordsCsvContent = File.ReadAllLines(ResourceReference.WordsCsv);
            foreach (Categories category in Enum.GetValues(typeof(Categories)))
            {
                var categoryWords = wordsCsvContent[(int)category].Split(',');
                wordsDictionary[category] = categoryWords;
            }
        }

        #endregion

        private readonly bool[] activeCategories;

        public WordsCollection(bool[] activeCategories)
        {
            this.activeCategories = activeCategories;
        }

        public string GetRandomWord()
        {
            Random random = new Random();

            bool foundActiveCategory = false;
            Categories categoryFound = Categories.Programming;
            while (!foundActiveCategory)
            {
                int randomCategory = random.Next(0, categoriesCount);
                if (activeCategories[randomCategory] == true)
                {
                    categoryFound = (Categories)randomCategory;
                    foundActiveCategory = true;
                }
            }

            int randomIndex = random.Next(0, wordsDictionary[categoryFound].Length);
            return wordsDictionary[categoryFound][randomIndex];
        }
    }
}
