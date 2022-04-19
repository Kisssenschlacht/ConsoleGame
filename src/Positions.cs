namespace ConsoleGame
{
    public struct Positions{
        public int x;
        public int y;
        public static bool operator==(Positions a, Positions b) => a.x == b.x && a.y == b.y;
        public static bool operator!=(Positions a, Positions b) => a.x != b.x || a.y != b.y;
    }
}