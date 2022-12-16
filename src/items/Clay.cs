namespace ConsoleGame.Items
{
    class Clay : PlaceableItem
    {
        public override string DisplayName() => "Clay";
        public override Tile ToPlace(Map map, Position position) => new Tiles.Clay(map, position);
    }
}