using System;
using System.Text;

namespace ConsoleGame
{
    partial class Program
    {
        void pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        void print()
        {
            Console.Clear();
            Console.Write(MapToString() + '\n' + InventoryToString() + '\n');
        }
        static void printMenu(char[,] menu)
        {
            Console.Clear();
            Console.Write(MenuToString(menu));
        }
        string MapToString()
        {
            char[,] charArray = new char[Constants.height * 3, Constants.width * 3];
            for (int y = 0; y < Constants.height; ++y)
            {
                for (int x = 0; x < Constants.width; ++x)
                {
                    addTo2DCharArray(ref charArray, GetCharsAtPosition(new Position { x = x, y = y }), y, x);
                }
            }
            return generateBorder(charArray);
        }
        char[,] GetCharsAtPosition(Position position) =>
            position == playerPosition ? _state.Map.Player.Texture :
            _state.Map.Entities.Where(x => x.Position == position).FirstOrDefault()?.Texture ??
            _state.Map.Tiles[position.x, position.y]?.Texture ??
            Constants.EmptyTexture;
        static void addTo2DCharArray(ref char[,] charArray, char[,] toAdd, int xOffset, int yOffset)
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
            return generateBorder(menu, 1, 1);
        }
        static string generateBorder(char[,] from, int topSpacing = 0, int bottomSpacing = 0, char spacing = ' ')
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
        string InventoryToString() => string.Join(',', _state.Map.Player.Inventory.GetItemStacks().Take(Inventory.HotbarSize).Select((x, i) =>
            {
                string separator = (i == _state.Map.Player.Inventory.SelectedIndex ? "|" : "");
                string main = x.HasValue ? $"{x.Value.Item.DisplayName()}: {x.Value.Amount}" : " ";
                return separator + main + separator;
            }));
    }
}