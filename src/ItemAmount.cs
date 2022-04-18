namespace ConsoleGame
{
    struct ItemAmount
    {
        public Item Item;
        public int Amount;
        public ItemAmount(Item item, int amount)
        {
            Item = item;
            Amount = amount;
        }
    }
}