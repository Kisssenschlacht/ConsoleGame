using System;
using System.Text;

namespace ConsoleGame
{
    partial class Program
    {
        void pause(){
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        void print(){
            Console.Clear();
            Console.Write(MapToString() + '\n' + InventoryToString() + '\n');
        }
        static void printMenu(char[,] menu) {
            Console.Clear();
            Console.Write(MenuToString(menu));
        }
        string MapToString(){
            char[,] charArray = new char[height,width];
            for(int y = 0; y < height; ++y){
                for(int x = 0;x < width; ++x){
                    charArray[y, x] = x == playerPosition.x && y == playerPosition.y ? Player : getChar[Map[x, y]];
                }
            }
            return generateBorder(charArray);
        }
        static string MenuToString(char[,] menu) {
            return generateBorder(menu, 1, 1);
        }
        static string generateBorder(char[,] from, int topSpacing = 0, int bottomSpacing = 0, char spacing = ' ')
        {
            int fromHeight = from.GetLength(0);
            int fromWidth = from.GetLength(1);
            StringBuilder sb = new StringBuilder((fromHeight + 2) * (fromWidth + 3));
            sb.Append($"+{new string('-', width)}+\n");
            for (; topSpacing > 0; --topSpacing) sb.Append($"|{new string(spacing, width)}|\n");
            for (int y = 0; y < fromHeight; ++y)
            {
                sb.Append('|');
                for (int x = 0; x < fromWidth; ++x)
                {
                    sb.Append(from[y, x]);
                }
                sb.Append("|\n");
            }
            for (; bottomSpacing > 0; --bottomSpacing) sb.Append($"|{new string(spacing, width)}|\n");
            sb.Append($"+{new string('-', width)}+\n");
            return sb.ToString();
        }
        string InventoryToString()
        {
            return string.Join(',', inventory.Select(i => (SelectedItem.Item == i.Key ? '|' : ' ') + $"{Enum.GetName<Item>(i.Key)}: {i.Value}" + (SelectedItem.Item == i.Key ? '|' : ' ')));
        }
    }
}