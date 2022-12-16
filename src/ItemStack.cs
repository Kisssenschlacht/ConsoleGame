namespace ConsoleGame
{
    struct ItemStack
    {
        public IItem Item { get; set; }
        public ulong Amount { get; set; }
        public ItemStack Split(ulong amount)
        {
            Amount = checked(Amount - amount);
            return new ItemStack() { Item = this.Item, Amount = amount };
        }
    }
}