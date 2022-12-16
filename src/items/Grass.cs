namespace ConsoleGame.Items
{
    class Grass : PlaceableItem
    {
        public override string DisplayName() => "Grass";
        public override Tile ToPlace(Map map, Position position) => new Tiles.Grass(map, position);
    }
}