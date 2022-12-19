namespace ConsoleGame
{
    abstract class Entity : IUpdatable, IHealth, IPosition, ITexture
    {
        public Map Map { get; init; }
        public abstract int Health { get; set; }
        public Position Position { get; set; }
        public abstract char[,] Texture { get; }
        public virtual void Update(TimeSpan elapsedTime) { }
        protected Entity(Map map, Position position)
        {
            Map = map;
            Position = position;
        }
        protected List<Position> Search(Predicate<Position> predicate)
        {
            var start = Position;

            HashSet<Position> closedSet = new HashSet<Position>();

            // The set of currently discovered nodes that are not evaluated yet.
            // Initially, only the start node is known.
            HashSet<Position> openSet = new HashSet<Position>();
            openSet.Add(start);

            // For each node, which node it can most efficiently be reached from.
            // If a node can be reached from many nodes, cameFrom will eventually contain the
            // most efficient previous step.
            Dictionary<Position, Position> cameFrom = new Dictionary<Position, Position>();

            // For each node, the cost of getting from the start node to that node.
            Dictionary<Position, double> gScore = new Dictionary<Position, double>();

            // The cost of going from start to start is zero.
            gScore[start] = 0;

            // For each node, the total cost of getting from the start node to the goal
            // by passing by that node. That value is partly known, partly heuristic.
            Dictionary<Position, double> fScore = new Dictionary<Position, double>();

            // For the first node, that value is completely heuristic.
            fScore[start] = 0;

            while (openSet.Count > 0)
            {
                // The node in openSet having the lowest fScore[] value
                Position? currentNullable = null;
                double lowestFScore = double.MaxValue;

                foreach (Position p in openSet)
                {
                    if (fScore[p] < lowestFScore)
                    {
                        currentNullable = p;
                        lowestFScore = fScore[p];
                    }
                }
                if (currentNullable == null) throw new Exception();
                Position current = (Position)currentNullable;

                if (predicate(current))
                {
                    return ReconstructMoves(cameFrom, current);
                }

                openSet.Remove(current);
                closedSet.Add(current);

                foreach (Position neighbor in current.GetDirectNeighbors(Map))
                {
                    // Ignore the neighbor which is already evaluated.
                    if (closedSet.Contains(neighbor))
                    {
                        continue;
                    }

                    // The distance from start to a neighbor
                    double distance = (neighbor - start).Length();

                    // Discover a new node
                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                    // This is not a better path.
                    else if (distance >= gScore[neighbor])
                    {
                        continue;
                    }

                    // This path is the best until now. Record it!
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = distance;
                    fScore[neighbor] = gScore[neighbor] + distance;
                }
            }

            // If we get here, then the search was unsuccessful and we didn't find a path to the goal.
            return new List<Position>();
        }
        private static List<Position> ReconstructMoves(Dictionary<Position, Position> cameFrom, Position current)
        {
            List<Position> moves = new List<Position>();
            while (cameFrom.ContainsKey(current))
            {
                var previous = current;
                current = cameFrom[current];
                moves.Add(previous - current);
            }
            moves.Reverse();
            return moves;
        }
    }
}