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
        bool running = true;
        ProgramState programState = ProgramState.StartMenu;
        static readonly Program Instance = new Program();
        private GameState _state = new GameState(width, height);
        ConsoleKeyInfo lastPressed;
        Positions playerPosition =  new Positions(){x = 4, y = 4};
        public static async Task Main(string[] args) { await Instance.Run(args); }
        public async Task Run(string[] args)
        {
            Console.Clear();
            generateMap();

            Console.CursorVisible = false;
            
            Thread inputHandlerThread = new Thread(new ThreadStart(async () => {
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
            while(_running){
                Console.SetCursorPosition(0, 0);
                switch (programState)
                {
                    case ProgramState.StartMenu:
                        printMenu(StartMenu);
                        break;
                    case ProgramState.QuickMenu:
                        printMenu(QuickMenu);
                        break;
                    case ProgramState.Game:
                        print();
                        break;
                }
                Update(stopwatch.Elapsed);
                stopwatch.Restart();
                lock (runningLock)
                {
                    _running = running;
                }
                await Task.Delay(10);
            }
        }
        private void Update(TimeSpan elapsedTime)
        {
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    TileType? newTileType = tileUpdateForTileType.ContainsKey(Map[x, y]) ? tileUpdateForTileType[Map[x, y]](elapsedTime) : null;
                    if (newTileType == null) continue;
                    Map[x, y] = (TileType)newTileType;
                }
            }
            return;
        }
        // returns whether the game loop should stop
    }
}
