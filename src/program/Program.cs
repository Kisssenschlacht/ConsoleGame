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
        Game
    }
    partial class Program
    {
        // Change in production to false
        const bool DEBUG = true;
        private readonly object runningLock = new object();
        private readonly object entitiesLock = new object();
        bool running = true;
        ProgramState programState = ProgramState.StartMenu;
        public static readonly Program Instance = new Program();
        public GameState _state = new GameState();
        ConsoleKeyInfo lastPressed;
        Position playerPosition = new Position() { x = 4, y = 4 };
        public static async Task Main(string[] args) { await Instance.Run(args); }
        public event Update update;
        public Program()
        {
            update += UpdateTiles;
            update += UpdateEntities;
        }
        public async Task Run(string[] args)
        {
            Console.Clear();

            Console.CursorVisible = false;

            Thread inputHandlerThread = new Thread(new ThreadStart(async () =>
            {
                bool _running = false;
                lock (runningLock)
                {
                    _running = running;
                }
                while (_running)
                {
                    lastPressed = Console.ReadKey(true);
                    _running = !await HandleKeyInput();
                    lock (runningLock)
                    {
                        running = _running;
                    }
                    await Task.Delay(10);
                }
            }));
            inputHandlerThread.Start();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            bool _running = false;
            lock (runningLock)
            {
                _running = running;
            }
            while (_running)
            {
                Console.SetCursorPosition(0, 0);
                switch (programState)
                {
                    case ProgramState.StartMenu:
                        printMenu(Constants.StartMenu);
                        break;
                    case ProgramState.QuickMenu:
                        printMenu(Constants.QuickMenu);
                        break;
                    case ProgramState.Game:
                        print();
                        break;
                }
                update.Invoke(stopwatch.Elapsed);
                stopwatch.Restart();
                lock (runningLock)
                {
                    _running = running;
                }
                await Task.Delay(10);
            }
        }
        private void UpdateTiles(TimeSpan elapsedTime)
        {
            Parallel.ForEach(_state.Map.Tiles.Cast<Tile?>(), x => x?.Update(elapsedTime));
            return;
        }
        private void UpdateEntities(TimeSpan elapsedTime)
        {
            lock (entitiesLock)
            {
                Parallel.ForEach(_state.Map.Entities, x => { x?.Update(elapsedTime); });
                if (RandomNumberGenerator.GetInt32(10 * 1000 / elapsedTime.Milliseconds) == 0 && _state.Map.Entities.Count() < Constants.EntityLimit)
                    SpawnRandomEntity();
            }
        }
        // returns whether the game loop should stop
    }
}
