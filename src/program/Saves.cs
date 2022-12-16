using System.Text.Json;

namespace ConsoleGame
{
    partial class Program
    {
        private SaveManager<GameState> _saveManager = new SaveManager<GameState>(/* new JsonSerializerOptions { AllowTrailingCommas = Program.DEBUG, IncludeFields = true, PropertyNameCaseInsensitive = false, PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = false, Converters = { new TileTypeJsonConverter(), new TileType2DArrayJsonConverter() } }  */);
        private async Task Save()
        {
            Console.Clear();
            Console.WriteLine("Please enter the path to the file to be saved:");
            string? filepath = Console.ReadLine();
            if (filepath == null) throw new Exception("Filepath was null when saving");
            if (_saveManager.Exists(filepath))
            {
                bool invalidAnswer = false;
                do
                {
                    Console.WriteLine($"The file ${filepath} is already existing. Do you want it to be overwritten? [Y/N]");
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.N:
                            Console.WriteLine("Saving canceld");
                            Console.WriteLine("Press any key to continue");
                            Console.ReadKey();
                            return;
                        case ConsoleKey.Y:
                            invalidAnswer = false;
                            break;
                        default:
                            invalidAnswer = true;
                            break;
                    }
                } while (invalidAnswer);
            }
            await _saveManager.SaveAsJson(filepath, _state);
            Console.WriteLine("Saved!");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
        private async Task Load()
        {
            Console.Clear();
            Console.WriteLine("Please enter the path to the file to be loaded:");
            string? filepath = Console.ReadLine();
            if (filepath == null) throw new Exception("Filepath was nul when loading");
            if (!_saveManager.Exists(filepath))
            {
                Console.WriteLine("The file doesn't exist");
                pause();
                return;
            }
            _state = await _saveManager.ReadFromJson(filepath);
            Console.WriteLine("The file was loaded successfully");
            pause();
            return;
        }
    }
}