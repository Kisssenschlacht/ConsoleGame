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
                Console.SetCursorPosition(0, 0);
                await Update(stopwatch.Elapsed);
                stopwatch.Restart();
            }
        }
        private async Task Update(TimeSpan elapsedTime)
        {
            
            return;
        }
        // returns whether the game loop should stop
        private async Task<bool> HandleKeyInput()
        {
            switch (programState)
            {
                case ProgramState.StartMenu:
                    return HandleKeyInputStartMenu();
                case ProgramState.QuickMenu:
                    return await HandleKeyInputQuickMenu();
                case ProgramState.Game:
                    return HandleKeyInputGame();
            }
            return false;
        }
        private bool HandleKeyInputStartMenu()
        {
            switch (lastPressed.Key)
            {
                case ConsoleKey.Enter:
                    programState = ProgramState.Game;
                    break;
            }
            return false;
        } 
        private async Task<bool> HandleKeyInputQuickMenu()
        {
            switch (lastPressed.Key)
            {
                case ConsoleKey.S:
                case ConsoleKey.L:
                    if ((lastPressed.Modifiers & ConsoleModifiers.Control) == 0) return false;
                    switch (lastPressed.Key)
                    {
                        case ConsoleKey.S:
                            await Save();
                            break;
                        case ConsoleKey.L:
                            await Load();
                            break;
                    }
                    break;
                case ConsoleKey.Escape:
                    programState = ProgramState.Game;
                    break;
            }
            return false;
        }
        private bool HandleKeyInputGame()
        {
            switch (lastPressed.Key)
            {
                case ConsoleKey.Q:
                    return true;
                case ConsoleKey.Escape:
                    programState = ProgramState.QuickMenu;
                    break;
                case ConsoleKey.A:
                case ConsoleKey.W:
                case ConsoleKey.S:
                case ConsoleKey.D:
                    getMovement();
                    break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                case ConsoleKey.RightArrow:
                case ConsoleKey.LeftArrow:
                    if ((lastPressed.Modifiers & ConsoleModifiers.Shift) == 0) destroyBlock();
                    else placeBlock();
                    break;
                case ConsoleKey.D0:
                case ConsoleKey.D1:
                case ConsoleKey.D2:
                case ConsoleKey.D3:
                case ConsoleKey.D4:
                case ConsoleKey.D5:
                case ConsoleKey.D6:
                case ConsoleKey.D7:
                case ConsoleKey.D8:
                case ConsoleKey.D9:
                case ConsoleKey.OemPlus:
                case ConsoleKey.OemMinus:
                    checkSelectedIndex();
                    break;
            }
            return false;
        }
        void checkSelectedIndex(){
            if (lastPressed.Key >= ConsoleKey.D0 && lastPressed.Key <= ConsoleKey.D9)
            {
                SelectedIndex = (lastPressed.Key - ConsoleKey.D0 - 1) % 10;
            }
            switch (lastPressed.Key){
                case ConsoleKey.OemPlus:
                ++SelectedIndex;
                break;
                case ConsoleKey.OemMinus:
                --SelectedIndex;
                break;
            }
        }
    }
}
