namespace ConsoleGame.Items
{
    class Sapling : PlaceableItem
    {
        public override string DisplayName() => "Sapling";
        public override Tile ToPlace(Map map, Position position) => new Tiles.Sapling(map, position);
    }
}