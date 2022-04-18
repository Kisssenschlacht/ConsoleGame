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
            
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            while(true){
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
                lastPressed = Console.ReadKey(true);
                if(await HandleKeyInput()) break;
                Update(stopwatch.Elapsed);
                stopwatch.Restart();
            }
        }
        private void Update(TimeSpan elapsedTime)
        {
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    if (Map[x, y] == TileType.Sapling && RandomNumberGenerator.GetInt32((int)((double)10/(double)elapsedTime.Milliseconds*(double)1000)) == 0)
                        Map[x, y] = TileType.Tree;
                }
            }
            return;
        }
        // returns whether the game loop should stop
    }
}
