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
            char[,] charArray = new char[height*3,width*3];
            for(int y = 0; y < height; ++y){
                for(int x = 0;x < width; ++x){
                    addTo2DCharArray(ref charArray, GetCharsAtPosition(new Positions { x = x, y = y }), y, x);
                }
            }
            return generateBorder(charArray);
        }
        char[,] GetCharsAtPosition(Positions position)
        {
            if (position == playerPosition) return Player;
            Entity[]? entityAtPosition = null;
            lock (entitiesLock)
            {
                entityAtPosition = Entities.Where(x => x.Pos == position).ToArray();
            }
            if (entityAtPosition.Length > 0) return EntityTypeChars[entityAtPosition[0].Type];
            return TileTypeChars[Map[position.x, position.y]];
        }
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
        static string MenuToString(char[,] menu) {
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
        string InventoryToString()
        {
            return string.Join(',', inventory.Select(i => (SelectedItem.Item == i.Key ? '|' : ' ') + $"{Enum.GetName<Item>(i.Key)}: {i.Value}" + (SelectedItem.Item == i.Key ? '|' : ' ')));
        }
    }
}