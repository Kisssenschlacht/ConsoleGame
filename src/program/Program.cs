using System;
using System.Diagnostics;
using System.Text;
using System.Security.Cryptography;

namespace ConsoleGame
{
    enum ProgramState
    {
        StartMenu,
        QuickMenu,
        Inventory,
        Game
    }
    partial class Program
    {
        // Change in production to false
        const bool DEBUG = true;
        private readonly object runningLock = new object();
        public static readonly Program Instance = new Program();
        public GameState _state = new GameState();
        Position playerPosition = new Position() { x = 4, y = 4 };
        public static void Main(string[] args) { Instance.Run(args); }
        public event Update update;
        public Program()
        {
            update += UpdateTiles;
            update += UpdateEntities;
        }
        public void Run(string[] args)
        {
            Console.Clear();
            Console.CursorVisible = false;
            ConsoleHandler handler = new(_state.Map, update.Invoke);
            handler.Run();
        }
        private void UpdateTiles(TimeSpan elapsedTime) => Parallel.ForEach(_state.Map.Tiles.Cast<Tile?>(), x => x?.Update(elapsedTime));
        private void UpdateEntities(TimeSpan elapsedTime) => Parallel.ForEach(_state.Map.Entities, x => { x?.Update(elapsedTime); });
        private void SpawnRandomEntity(TimeSpan elapsedTime)
        {
            if (elapsedTime.Milliseconds == 0 || RandomNumberGenerator.GetInt32(10 * 1000 / elapsedTime.Milliseconds) != 0 || _state.Map.Entities.Count() >= Constants.EntityLimit) return;
            Position[] possiblePositionsArray =
                Enumerable.Range(0, Constants.width * Constants.height)
                .Select(z => new Position() { x = z % Constants.width, y = z / Constants.width })
                .Where(x => !_state.Map.Entities.Select(y => y.Position).Contains(x)).ToArray();
            var chosenPosition = possiblePositionsArray.Random();
            if (chosenPosition == null) return;
            Position nonnullChosenPosition = (Position)chosenPosition;
            Func<Entity>? constructor = new Func<Entity>[] {
                () => new Entities.Cow(_state.Map, nonnullChosenPosition),
                () => new Entities.Sheep(_state.Map, nonnullChosenPosition)
            }.Random();
            Entity entity;
            if (constructor == null || (entity = constructor()) == null) return;
            _state.Map.Entities.Add(entity);
        }
        // returns whether the game loop should stop
    }
}
