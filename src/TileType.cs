using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleGame
{
    public enum TileType : byte
    {
        Empty,
        Tree,
        Stone,
        Sapling,
        Clay,
        SaplingStageTwo,
        SaplingStageThree,
        Wood,
        Grass,
        GrassStageTwo,
        GrassStageThree
    }

    public class TileTypeJsonConverter : JsonConverter<TileType>
    {
        public override TileType Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
                (TileType)byte.Parse(reader.GetString());

        public override void Write(
            Utf8JsonWriter writer,
            TileType tileTypeValue,
            JsonSerializerOptions options) =>
                writer.WriteNumberValue((byte)tileTypeValue);
    }
    public class TileType2DArrayJsonConverter : JsonConverter<TileType[,]>
    {
        public override TileType[,] Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            TileType[][] oneDimensionalArrays = JsonSerializer.Deserialize<TileType[][]>(ref reader);
            int height = oneDimensionalArrays.Length;
            int width = oneDimensionalArrays.Select(x => x.Length).Max();
            TileType[,] result = new TileType[height, width];
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    result[i, j] = oneDimensionalArrays[i][j];
                }
            }
            return result;
        }

        public override void Write(
            Utf8JsonWriter writer,
            TileType[,] tileType2DValue,
            JsonSerializerOptions options)
        {
            TileType[][] oneDimensionalArrays = new TileType[tileType2DValue.GetLength(0)][];
            for (int i = 0; i < tileType2DValue.GetLength(0); ++i)
            {
                TileType[] currentArray = new TileType[tileType2DValue.GetLength(1)];
                for (int j = 0; j < tileType2DValue.GetLength(1); ++j)
                    currentArray[j] = tileType2DValue[i, j];
                oneDimensionalArrays[i] = currentArray;
            }
            JsonSerializer.Serialize(writer, oneDimensionalArrays.ToArray());
        }
    }
}