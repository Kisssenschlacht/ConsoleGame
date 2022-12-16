namespace ConsoleGame.Items
{
    abstract class PlaceableItem : DefaultItem, IPlaceable
    {
        public abstract Tile ToPlace(Map map, Position position);
        public void Place(Map map, Position position) => map.Tiles[position.x, position.y] = ToPlace(map, position);
    }
}