namespace ConsoleGame
{
    record struct Position(int x, int y)
    {
        Position Add(Position other) => new Position() { x = this.x + other.x, y = this.y + other.y };
        Position Subtract(Position other) => new Position() { x = this.x - other.x, y = this.y - other.y };
        public static Position operator +(Position a, Position b) => a.Add(b);
        public static Position operator -(Position a, Position b) => a.Subtract(b);
        public IEnumerable<Position> GetDirectNeighbors(Map map) =>
            new Position[] {
                Add(new Position() { x =  1 }),
                Add(new Position() { x = -1 }),
                Add(new Position() { y =  1 }),
                Add(new Position() { y = -1 })
            }.Where(x => !map.IsObstructed(x));
        public double Length() => Math.Sqrt(this.x * this.x + this.y * this.y);
    }
}