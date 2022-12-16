using System.Security.Cryptography;

namespace ConsoleGame.Tiles
{
    class Tree : MaterialTile
    {
        public override char[,] Texture => Constants.FromString(new string[] { " * ", "/_\\", " | " });

        public override LootTable LootTable => (Entity _) =>
        {
            List<ItemStack> result = new() { new ItemStack() { Item = new Items.Wood(), Amount = (ulong)RandomNumberGenerator.GetInt32(3) + 1 } };
            if (RandomNumberGenerator.GetInt32(2) == 0)
                result.Add(new ItemStack() { Item = new Items.Sapling(), Amount = (ulong)RandomNumberGenerator.GetInt32(3) + 1 });
            return result;
        };

        public Tree(Map map, Position position) : base(map, position)
        {
        }
    }
}