namespace ConsoleGame.Items
{
    class Wood : PlaceableItem
    {
        public override string DisplayName() => "Wood";
        public override Tile ToPlace(Map map, Position position) => new Tiles.Wood(map, position);
    }
}