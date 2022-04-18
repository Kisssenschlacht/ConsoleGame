using System.Security.Cryptography;

namespace ConsoleGame
{
    partial class Program
    {
        private static char[,] fromString(string[] s)
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
        readonly char[,] StartMenu = fromString(new string[] { "Hit enter to start the game!" });
        readonly char[,] QuickMenu = fromString(new string[] { "", " Press S to save map and inventory ", "", " Press L to load previous map", "" });
        public delegate List<ItemAmount> LootTable();
        public delegate TileType? TileUpdate(TimeSpan elapsedTime);
        readonly Dictionary<TileType, TileUpdate> tileUpdateForTileType = new Dictionary<TileType, TileUpdate>() {
            { TileType.Sapling, (TimeSpan elapsedTime) => RandomNumberGenerator.GetInt32((int)((double)10/(double)elapsedTime.Milliseconds*(double)1000)) == 0 ? TileType.Tree : null }
        };
        readonly Dictionary<Item, TileType> tileTypeForItem = new Dictionary<Item, TileType>() {
            { Item.Stone, TileType.Stone },
            { Item.Wood, TileType.Tree },
            { Item.Sapling, TileType.Sapling },
            { Item.Clay, TileType.Clay}
        };
        readonly Dictionary<TileType, LootTable> itemForTileType = new Dictionary<TileType, LootTable> {
            { TileType.Stone, () => new List<ItemAmount>() { new ItemAmount(Item.Stone, 1) } },
            { TileType.Tree, () => {
                List<ItemAmount> list = new List<ItemAmount>();
                list.Add(new ItemAmount(Item.Wood, 1));
                if (RandomNumberGenerator.GetInt32(2) == 0)
                    list.Add(new ItemAmount(Item.Sapling, 1));
                return list;
            } },
            { TileType.Sapling, () => new List<ItemAmount>() { new ItemAmount(Item.Sapling, 1)}},
            { TileType.Clay, () => new List<ItemAmount>() { new ItemAmount(Item.Clay, 1) } }
        };
        readonly Dictionary<TileType, bool> isObstacle = new Dictionary<TileType, bool>() {
            { TileType.Empty, false },
            { TileType.Stone, true },
            { TileType.Tree, true },
            {TileType.Sapling, true },
            { TileType.Clay, true }
        };
        readonly Dictionary<TileType, char[,]> getChars = new Dictionary<TileType, char[,]>() {
            { TileType.Empty, fromString(new string[] {"   ", "   ", "   "}) },
            { TileType.Stone, fromString(new string[] {" # ", "## ", "###"}) },
            { TileType.Tree, fromString(new string[] {" * ", "/_\\", " | "}) },
            { TileType.Sapling, fromString(new string[] { " . ", "...", " . " })},
            { TileType.Clay, fromString(new string[] { "++", "+++", "+++"})}
        };
        readonly char[,] Player = fromString(new string[] { " @ ", "_|_", "| |" });
        const int width = 20;
        const int height = 10;
        //:D
    }
}