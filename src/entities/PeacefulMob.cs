namespace ConsoleGame.Entities
{
    abstract class PeacefulMob : Mob
    {
        protected PeacefulMob(Map map, Position position) : base(map, position) { }
        public override void Action()
        {
            Position[] options = new Position[] { new Position() { x = 1 }, new Position() { x = -1 }, new Position() { y = 1 }, new Position() { y = -1 } }.Select(x => Position + x).Where(x => !Program.Instance._state.Map.IsObstructed(x)).ToArray();
            Position = options.Random() ?? new Position() { x = 0, y = 0 };
        }
    }
}