namespace ConsoleGame.Entities
{
    abstract class PeacefulMob : Mob
    {
        protected PeacefulMob(Map map, Position position) : base(map, position) { }
        List<Position>? Path { get; set; }
        int saturation = 10;
        public override void Action()
        {
            if (Map.Tiles[Position.x, Position.y] is Tiles.Grass) return;
            if ((Path == null || Path.Count == 0)) Path = Search(x => Map.Tiles[x.x, x.y] is Tiles.Grass);
            if (Path.Count == 0) return;
            Position += Path[0];
            Path.RemoveAt(0);
        }
    }
}