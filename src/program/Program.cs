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
        bool running = true;
        ProgramState programState = ProgramState.StartMenu;
        public static readonly Program Instance = new Program();
        public GameState _state = new GameState();
        ConsoleKeyInfo lastPressed;
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
            if (elapsedTime.Milliseconds != 0 && RandomNumberGenerator.GetInt32(10 * 1000 / elapsedTime.Milliseconds) == 0 && _state.Map.Entities.Count() < Constants.EntityLimit)
                SpawnRandomEntity();
        }
        // returns whether the game loop should stop
    }
}
