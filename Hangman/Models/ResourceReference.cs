using System;

namespace Hangman.Models
{
    public static class ResourceReference
    {
        public static readonly Uri MenuMusic = new Uri($"{Environment.CurrentDirectory}/Resources/Music/menu_music.mp3");
        public static readonly Uri GameMusic = new Uri($"{Environment.CurrentDirectory}/Resources/Music/game_music.mp3");
        public static readonly Uri FinalMusic = new Uri($"{Environment.CurrentDirectory}/Resources/Music/final_music.mp3");
        public static readonly Uri HangmanAnimation = new Uri($"{Environment.CurrentDirectory}/Resources/HangmanAnimation.wmv");
        public static readonly Uri ProfilePictures = new Uri($"{Environment.CurrentDirectory}/Resources/ProfilePictures");
        public static readonly string ProfilePicturesStr = "../Resources/ProfilePictures";
        public static readonly string UsersXml = "./Resources/Data/Users.xml";
        public static readonly string WordsCsv = "./Resources/Words.csv";
        public static readonly string GameAnimation = "../Resources/GameAnimation";
        public static readonly string Icons = $"{Environment.CurrentDirectory}/Resources/Icons";

        public static Uri GetIconUri(string name)
        {
            return new Uri($"{Icons}/{name}.png");
        }
        public static string GetGameAnimationPath(int mistakes)
        {
            return $"{GameAnimation}/phase{mistakes}.png";
        }
    }
}
