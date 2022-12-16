namespace ConsoleGame.Items
{
    class Stone : PlaceableItem
    {
        public override string DisplayName() => "Stone";
        public override Tile ToPlace(Map map, Position position) => new Tiles.Stone(map, position);
    }
}