using System.Collections.Generic;

namespace ConsoleGame
{
    struct GameState
    {
        public Dictionary<Item, int> Inventory;
        public TileType[,] Map;
        public GameState(int width, int height, Dictionary<Item, int>? inventory = null)
        {
            Inventory = inventory ?? new Dictionary<Item, int>();
            Map = new TileType[width, height];
        }
    }
}