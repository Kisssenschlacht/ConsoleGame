using System.Security.Cryptography;

namespace ConsoleGame
{
    partial class Program
    {

        const int MillisecondsInSec = 1000;
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
        readonly Dictionary<TileType, TileUpdate> tileUpdateForTileType = new Dictionary<TileType, TileUpdate>() {
            { TileType.Sapling, TileUpdateHelper.GenerateTileUpdate(TileType.SaplingStageTwo, 10 * MillisecondsInSec) },
            { TileType.SaplingStageTwo, TileUpdateHelper.GenerateTileUpdate(TileType.SaplingStageThree, 10 * MillisecondsInSec) },
            { TileType.SaplingStageThree, TileUpdateHelper.GenerateTileUpdate(TileType.Tree, 10 * MillisecondsInSec) },
            { TileType.Grass, TileUpdateHelper.GenerateTileUpdate(TileType.GrassStageTwo, 10 * MillisecondsInSec)},
            { TileType.GrassStageTwo, TileUpdateHelper.GenerateTileUpdate(TileType.GrassStageThree, 10 * MillisecondsInSec)}
        };
        readonly Dictionary<Item, TileType> tileTypeForItem = new Dictionary<Item, TileType>() {
            { Item.Stone, TileType.Stone },
            { Item.Wood, TileType.Wood },
            { Item.Sapling, TileType.Sapling },
            { Item.Clay, TileType.Clay},
            { Item.Grass, TileType.Grass}
        };
        readonly Dictionary<TileType, LootTable> itemForTileType = new Dictionary<TileType, LootTable> {
            { TileType.Stone, () => new List<ItemAmount>() { new ItemAmount(Item.Stone, 1) } },
            { TileType.Tree, () => {
                List<ItemAmount> list = new List<ItemAmount>();
                list.Add(new ItemAmount(Item.Wood, RandomNumberGenerator.GetInt32(3) + 1 ));
                if (RandomNumberGenerator.GetInt32(2) == 0)
                    list.Add(new ItemAmount(Item.Sapling, 1));
                return list;
            } },
            { TileType.Sapling, () => new List<ItemAmount>() { new ItemAmount(Item.Sapling, 1)}},
            { TileType.SaplingStageTwo, () => new List<ItemAmount>() { new ItemAmount(Item.Sapling, 1) } },
            { TileType.SaplingStageThree, () => new List<ItemAmount>() { new ItemAmount(Item.Sapling, 1) } },
            { TileType.Clay, () => new List<ItemAmount>() { new ItemAmount(Item.Clay, 1) } },
            { TileType.Wood, () => new List<ItemAmount>() { new ItemAmount(Item.Wood, 1) } },
            { TileType.Grass, () => new List<ItemAmount>() { new ItemAmount(Item.Grass, 1)}},
            { TileType.GrassStageTwo, () => new List<ItemAmount>() { new ItemAmount(Item.Grass, 1)}},
            { TileType.GrassStageThree, () => new List<ItemAmount>() { new ItemAmount(Item.Grass, 1)}}
        };
        readonly Dictionary<TileType, bool> isObstacle = new Dictionary<TileType, bool>() {
            { TileType.Empty, false },
            { TileType.Stone, true },
            { TileType.Tree, true },
            { TileType.Sapling, true },
            { TileType.Clay, true },
            { TileType.SaplingStageTwo, true},
            { TileType.SaplingStageThree, true},
            { TileType.Wood, true},
            { TileType.Grass, false},
            { TileType.GrassStageTwo, false},
            { TileType.GrassStageThree, false}
        };
        readonly Dictionary<TileType, char[,]> TileTypeChars = new Dictionary<TileType, char[,]>() {
            { TileType.Empty, fromString(new string[] {"   ", "   ", "   "}) },
            { TileType.Stone, fromString(new string[] {" # ", "## ", "###"}) },
            { TileType.Tree, fromString(new string[] {" * ", "/_\\", " | "}) },
            { TileType.Sapling, fromString(new string[] { "   ", " + ", "   " })},
            { TileType.Clay, fromString(new string[] { "++", "+++", "+++"})},
            { TileType.SaplingStageTwo, fromString(new string[] {" . ", "_", " , "})},
            { TileType.SaplingStageThree, fromString(new string[] {"   ", "/_\\", " | "})},
            { TileType.Wood, fromString(new string[] { "OOO", "OOO", "OOO" })},
            { TileType.Grass, fromString(new string[] {"   ", " \" ", "   "})},
            { TileType.GrassStageTwo, fromString(new string[] {" \" ", "\"\"\"", " \" "})},
            { TileType.GrassStageThree, fromString(new string[] {"\"\"\"", "\"\"\"", "\"\"\""})}
        };
        readonly Dictionary<EntityType, char[,]> EntityTypeChars = new Dictionary<EntityType, char[,]>() {
            { EntityType.Sheep, fromString(new string[] {"@  ", "|__", "| |"}) },
            { EntityType.Cow, fromString(new string[] {"   ", "@__", "| |"}) }
        };
        readonly Dictionary<EntityType, int> EntityHearts = new Dictionary<EntityType, int>(){
            {EntityType.Cow, 6},
            {EntityType.Sheep, 5}
        };
        readonly char[,] Player = fromString(new string[] { " @ ", "_|_", "| |" });
        const int width = 20;
        const int height = 10;
        const int EntityLimit = 10;
    }
}