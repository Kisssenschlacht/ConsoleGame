namespace ConsoleGame
{
    partial class Program
    {
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
                case ConsoleKey.E:
                    lock (entitiesLock)
                    {
                        SpawnRandomEntity();
                    }
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
    }
}