using System.Collections.Generic;

namespace ConsoleGame
{
    struct GameState
    {
        public Dictionary<Item, int> Inventory;
        public TileType[,] Map;
        public List<Entity> Entities;
        public GameState(int width, int height, Dictionary<Item, int>? inventory = null, List<Entity> entities = null)
        {
            Inventory = inventory ?? new Dictionary<Item, int>();
            Map = new TileType[width, height];
            Entities = entities ?? new List<Entity>();
        }
    }
}