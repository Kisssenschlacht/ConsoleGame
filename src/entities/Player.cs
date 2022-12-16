namespace ConsoleGame.Entities
{
    class Player : InventoryEntity
    {
        static readonly char[,] texture = Constants.FromString(new string[] { " @ ", "_|_", "| |" });
        public Player(Map map, Position position) : base(map, position) { }
        public override char[,] Texture => texture;
        int _health = 10;
        public override int Health { get => _health; set => _health = value; }
        public void Move(ConsoleKey lastPressed)
        {
            Position newPosition = Position;
            switch (lastPressed)
            {
                case ConsoleKey.A:
                    --newPosition.x;
                    break;

                case ConsoleKey.D:
                    ++newPosition.x;
                    break;

                case ConsoleKey.W:
                    --newPosition.y;
                    break;

                case ConsoleKey.S:
                    ++newPosition.y;
                    break;
            }
            if (!Map.IsObstructed(newPosition)) Position = newPosition;
        }
        public void PlaceFromKey(ConsoleKey lastPressed) => ActionFromKey(lastPressed, Place);
        public void BreakFromKey(ConsoleKey lastPressed) => ActionFromKey(lastPressed, Break);
        void ActionFromKey(ConsoleKey lastPressed, Action<Position> action)
        {
            Position? actionPosition = GetActionPosition(lastPressed);
            if (actionPosition != null) action((Position)actionPosition);
        }
        Position? GetActionPosition(ConsoleKey lastPressed)
        {
            Position actionPosition = Position;
            switch (lastPressed)
            {
                case ConsoleKey.RightArrow:
                    ++actionPosition.x;
                    break;
                case ConsoleKey.LeftArrow:
                    --actionPosition.x;
                    break;
                case ConsoleKey.UpArrow:
                    --actionPosition.y;
                    break;
                case ConsoleKey.DownArrow:
                    ++actionPosition.y;
                    break;
                default: return null;
            }
            return Map.IsIllegalPosition(actionPosition) ? null : actionPosition;
        }
        void Place(Position actionPosition)
        {
            if (Map.Tiles[actionPosition.x, actionPosition.y] != null) return;
            var item = Inventory.SelectedItem?.Item as IPlaceable;
            if (item == null) return;
            item.Place(Map, actionPosition);
            Inventory.RemoveItem(Inventory.SelectedIndex, 1);
        }
        void Break(Position actionPosition)
        {
            if (!(Map.Tiles[actionPosition.x, actionPosition.y]?.IsObstacle ?? false)) return;
            Map.Tiles[actionPosition.x, actionPosition.y]?.OnBreak(this);
        }
    }
}