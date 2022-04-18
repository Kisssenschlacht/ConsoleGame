using System.Security.Cryptography;

namespace ConsoleGame
{
    public class TileUpdateHelper
    {
        public static TileUpdate GenerateTileUpdate(TileType to, int avgTimeInMilliseconds)
        {
            return (TimeSpan elapsedTime) => RandomNumberGenerator.GetInt32((int)((double)avgTimeInMilliseconds/(double)elapsedTime.Milliseconds)) == 0 ? to : null;
        }
    }
}