using System.Security.Cryptography;

namespace ConsoleGame
{
    static class ArrayExtensions
    {
        public static Nullable<T> Random<T>(this T[] array) where T : struct
        {
            return array.Length != 0 ? array[RandomNumberGenerator.GetInt32(array.Length)] : null;
        }
        public static T? Random<T>(this T[] array, byte _ = 0) where T : class
        {
            return array.Length != 0 ? array[RandomNumberGenerator.GetInt32(array.Length)] : null;
        }
    }
}