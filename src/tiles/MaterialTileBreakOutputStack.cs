namespace ConsoleGame.Tiles
{
    abstract class MaterialTileBreakOutputStack : MaterialTile
    {
        protected MaterialTileBreakOutputStack(Map map, Position position) : base(map, position) { }

        public abstract ItemStack BreakOutputStack { get; }
        public override LootTable LootTable => throw new NotImplementedException();
        public override void OnBreak(Entity entity, Func<ItemStack, ItemStack?> addToInventory) => addToInventory(BreakOutputStack);
    }
}