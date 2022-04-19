using System.Security.Cryptography;

namespace ConsoleGame
{
    static class ArrayExtensions
    {
        public static T Random<T>(this T[] array)
        {
            return array[RandomNumberGenerator.GetInt32(array.Length)];
        }
    }
}