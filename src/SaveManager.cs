using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace ConsoleGame
{
    class SaveManager<T> where T : notnull
    {
        private JsonSerializerOptions? _jsonSerializerOptions;
        public SaveManager(JsonSerializerOptions? jsonSerializerOptions = null)
        {
            _jsonSerializerOptions = jsonSerializerOptions;
        }
        public bool Exists(string filepath)
        {
            return new FileInfo(filepath).Exists;
        }
        public async Task SaveAsJson(string filepath, T toSave)
        {
            using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                await JsonSerializer.SerializeAsync<T>(fs, toSave, _jsonSerializerOptions);
            }
        }
        public async ValueTask<T?> ReadFromJson(string filepath)
        {
            using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                return await JsonSerializer.DeserializeAsync<T>(fs, _jsonSerializerOptions);
            }
        }
    }
}