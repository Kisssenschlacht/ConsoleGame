using System.Security.Cryptography;

namespace ConsoleGame
{
    class Constants
    {

        public const int MillisecondsInSec = 1000;
        public static char[,] FromString(string[] s)
        {
            int sHeight = s.Length;
            int[] lengthOfStrings = s.Select(x => x.Length).ToArray();
            int sWidth = lengthOfStrings.Max();
            char[,] result = new char[sHeight, sWidth];
            for (int i = 0; i < sHeight; ++i)
            {
                for (int j = 0; j < sWidth; ++j)
                    result[i, j] = j < lengthOfStrings[i] ? s[i][j] : ' ';
            }
            return result;
        }
        public static readonly char[,] EmptyTexture = FromString(new string[] { "   ", "   ", "   " });
        public static readonly char[,] StartMenu = FromString(new string[] { "Hit enter to start the game!" });
        public static readonly char[,] QuickMenu = FromString(new string[] { "", " Press S to save map and inventory ", "", " Press L to load previous map", "" });
        public const int width = 20;
        public const int height = 10;
        public const int EntityLimit = 10;
    }
}