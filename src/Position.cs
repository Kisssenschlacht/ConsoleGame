namespace ConsoleGame
{
    public record struct Position(int x, int y)
    {
        public static Position operator +(Position a, Position b) => new Position() { x = a.x + b.x, y = a.y + b.y };
        public static Position operator -(Position a, Position b) => new Position() { x = a.x + b.x, y = a.y + b.y };
    }
}