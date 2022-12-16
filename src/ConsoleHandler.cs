using System.Diagnostics;
using System.Text;

namespace ConsoleGame
{
    class ConsoleHandler
    {
        State state = State.StartMenu;
        enum State
        {
            StartMenu,
            QuickMenu,
            Inventory,
            Game
        }
        public Update Update;
        bool running = true;
        Map Map { get; init; }
        string InventoryCommand = string.Empty;
        public ConsoleHandler(Map map, Update update)
        {
            Map = map;
            Update = update;
        }
        public async void Run()
        {
            Thread inputHandlerThread = new(new ThreadStart(() =>
            {
                while (running) HandleKeyInput();
            }));
            inputHandlerThread.Start();
            Stopwatch stopwatch = new();
            stopwatch.Start();
            while (running)
            {
                Console.SetCursorPosition(0, 0);
                Display();
                Update(stopwatch.Elapsed);
                stopwatch.Restart();
                await Task.Delay(10);
            }
        }
        void HandleKeyInput()
        {
            ConsoleKeyInfo lastPressed = Console.ReadKey(true);
            switch (state)
            {
                #region StartMenu
                case State.StartMenu:
                    switch (lastPressed.Key)
                    {
                        case ConsoleKey.Enter:
                            state = State.Game;
                            break;
                    }
                    break;
                #endregion
                #region QuickMenu
                case State.QuickMenu:
                    switch (lastPressed.Key)
                    {
                        case ConsoleKey.Escape:
                            state = State.Game;
                            break;
                    }
                    break;
                #endregion
                #region Inventory
                case State.Inventory:
                    switch (lastPressed.Key)
                    {
                        case ConsoleKey.Enter:
                            if (InventoryCommand.StartsWith("swap"))
                            {
                                int[] parts = new string(InventoryCommand.Skip(4).ToArray()).Split(':').Select(x => int.Parse(x)).Take(2).ToArray();
                                int from;
                                int to;
                                if (parts.Length == 2)
                                {
                                    from = parts[0];
                                    to = parts[1];
                                }
                                else if (parts.Length == 1)
                                {
                                    from = Map.Player.Inventory.SelectedIndex;
                                    to = parts[0];
                                }
                                else break;
                                var tmp = Map.Player.Inventory[from];
                                Map.Player.Inventory[from] = Map.Player.Inventory[to];
                                Map.Player.Inventory[to] = tmp;
                                InventoryCommand = string.Empty;
                            }
                            break;
                        case ConsoleKey.Escape:
                            state = State.Game;
                            break;
                        case ConsoleKey.OemPlus:
                            ++Map.Player.Inventory.SelectedIndex;
                            break;
                        case ConsoleKey.OemMinus:
                            --Map.Player.Inventory.SelectedIndex;
                            break;
                        default:
                            InventoryCommand += lastPressed.KeyChar;
                            break;
                    }
                    break;
                #endregion
                #region Game
                case State.Game:
                    switch (lastPressed.Key)
                    {
                        case ConsoleKey.Q:
                            running = false;
                            break;
                        case ConsoleKey.Escape:
                            state = State.QuickMenu;
                            break;
                        case ConsoleKey.E:
                            state = State.Inventory;
                            break;
                        case ConsoleKey.A:
                        case ConsoleKey.W:
                        case ConsoleKey.S:
                        case ConsoleKey.D:
                            Map.Player.Move(lastPressed.Key);
                            break;
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.RightArrow:
                        case ConsoleKey.DownArrow:
                        case ConsoleKey.LeftArrow:
                            if ((lastPressed.Modifiers & ConsoleModifiers.Shift) == 0) Map.Player.BreakFromKey(lastPressed.Key);
                            else Map.Player.PlaceFromKey(lastPressed.Key);
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
                            Map.Player.Inventory.SelectedIndex = lastPressed.Key - ConsoleKey.D0;
                            break;
                        case ConsoleKey.OemPlus:
                            ++Map.Player.Inventory.SelectedIndex;
                            break;
                        case ConsoleKey.OemMinus:
                            --Map.Player.Inventory.SelectedIndex;
                            break;
                    }
                    break;
                    #endregion
            }
        }
        void Display()
        {
            Console.Clear();
            switch (state)
            {
                case State.StartMenu:
                    Console.Write(MenuToString(Constants.StartMenu));
                    break;
                case State.QuickMenu:
                    Console.Write(MenuToString(Constants.QuickMenu));
                    break;
                case State.Inventory:
                    int rows = (int)Math.Ceiling((decimal)Inventory.InventorySize / (decimal)Inventory.HotbarSize);
                    for (int i = 0; i < rows; ++i)
                        Console.WriteLine(InventoryToString(i * Inventory.HotbarSize, Math.Min(Inventory.InventorySize - i * Inventory.HotbarSize, 10)));
                    Console.Write(InventoryCommand);
                    break;
                case State.Game:
                    Console.WriteLine(MapToString());
                    Console.Write(HotbarToString());
                    break;
            }
        }
        string MapToString()
        {
            char[,] charArray = new char[Constants.height * 3, Constants.width * 3];
            for (int y = 0; y < Constants.height; ++y)
            {
                for (int x = 0; x < Constants.width; ++x)
                {
                    AddTo2DCharArray(ref charArray, GetCharsAtPosition(new Position { x = x, y = y }), y, x);
                }
            }
            return GenerateBorder(charArray);
        }
        char[,] GetCharsAtPosition(Position position) =>
            position == Map.Player.Position ? Map.Player.Texture :
            Map.Entities.Where(x => x.Position == position).FirstOrDefault()?.Texture ??
            Map.Tiles[position.x, position.y]?.Texture ??
            Constants.EmptyTexture;
        static void AddTo2DCharArray(ref char[,] charArray, char[,] toAdd, int xOffset, int yOffset)
        {
            int width = toAdd.GetLength(0);
            int height = toAdd.GetLength(1);
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    charArray[xOffset * 3 + x, yOffset * 3 + y] = toAdd[x, y];
                }
            }
        }
        static string MenuToString(char[,] menu)
        {
            return GenerateBorder(menu, 1, 1);
        }
        static string GenerateBorder(char[,] from, int topSpacing = 0, int bottomSpacing = 0, char spacing = ' ')
        {
            int fromHeight = from.GetLength(0);
            int fromWidth = from.GetLength(1);
            StringBuilder sb = new StringBuilder((fromHeight + 2) * (fromWidth + 3));
            sb.Append($"+{new string('-', fromWidth)}+\n");
            for (; topSpacing > 0; --topSpacing) sb.Append($"|{new string(spacing, fromWidth)}|\n");
            for (int y = 0; y < fromHeight; ++y)
            {
                sb.Append('|');
                for (int x = 0; x < fromWidth; ++x)
                {
                    sb.Append(from[y, x]);
                }
                sb.Append("|\n");
            }
            for (; bottomSpacing > 0; --bottomSpacing) sb.Append($"|{new string(spacing, fromWidth)}|\n");
            sb.Append($"+{new string('-', fromWidth)}+\n");
            return sb.ToString();
        }
        string HotbarToString() => InventoryToString(0, Inventory.HotbarSize);
        string InventoryToString(int from, int toExclusive) => string.Join(',', Map.Player.Inventory.GetItemStacks().Select((x, i) => new Tuple<ItemStack?, int>(x, i)).Skip(from).Take(toExclusive).Select(x =>
            {
                string separator = (x.Item2 == Map.Player.Inventory.SelectedIndex ? "|" : " ");
                string main = x.Item1.HasValue ? $"{x.Item1.Value.Item.DisplayName()}: {x.Item1.Value.Amount}" : " ";
                return separator + main + separator;
            }));
    }
}