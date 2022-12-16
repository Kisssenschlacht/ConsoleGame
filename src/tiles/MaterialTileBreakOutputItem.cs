namespace ConsoleGame.Tiles
{
    abstract class MaterialTileBreakOutputItem : MaterialTileBreakOutputStack
    {
        protected MaterialTileBreakOutputItem(Map map, Position position) : base(map, position) { }

        public abstract IItem BreakOutputItem { get; }
        public override ItemStack BreakOutputStack { get => new ItemStack() { Item = BreakOutputItem, Amount = 1 }; }
    }
}