using System.Security.Cryptography;

namespace ConsoleGame
{
    class Map
    {
        public Tile?[,] Tiles = new Tile?[Constants.width, Constants.height];
        Entities.Player? player = null;
        public Entities.Player Player { get => player ?? (player = new(this, new Position() { x = 4, y = 6 })); }
        public List<Entity> Entities = new();
        public Map()
        {
            for (int y = 0; y < Constants.height; ++y)
            {
                for (int x = 0; x < Constants.width; ++x)
                {
                    var position = new Position() { x = x, y = y };
                    var tile = PlaceTile(position);
                    if (tile != null) Tiles[x, y] = tile;
                }
            }
        }

        private Tile? PlaceTile(Position position)
        {
            int random = RandomNumberGenerator.GetInt32(100);
            if (random < 2) return new Tiles.Tree(this, position);
            if (random < 5) return new Tiles.Stone(this, position);
            if (random < 6) return new Tiles.Clay(this, position);
            if (random < 8) return new Tiles.Grass(this, position);
            return null;
        }
        public bool IsObstructed(Position position) =>
            IsIllegalPosition(position) || (Tiles[position.x, position.y]?.IsObstacle ?? false);
        public bool IsIllegalPosition(Position position) =>
            position.x >= Constants.width ||
            position.x < 0 ||
            position.y >= Constants.height
            || position.y < 0;
    }
}