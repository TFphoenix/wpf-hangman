using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Models
{
    public static class SessionSetUp
    {
        public static User ActiveUser { get; set; }
        public static bool[] ActiveCategories { get; set; }

        static SessionSetUp()
        {
            ActiveCategories = new bool[Enum.GetNames(typeof(Categories)).Length];
        }
    }
}
