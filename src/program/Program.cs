using System;
using System.Diagnostics;
using System.Text;

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
                await Update(stopwatch.Elapsed);
                stopwatch.Restart();
            }
        }
        private async Task Update(TimeSpan elapsedTime)
        {
            
            return;
        }
        // returns whether the game loop should stop
    }
}
