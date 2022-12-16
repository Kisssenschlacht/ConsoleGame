namespace ConsoleGame
{
    class Inventory : Container
    {
        public const int HotbarSize = 10;
        public const int InventorySize = 30;
        public override ulong MaxStackSize => 64;
        public List<ItemStack?> Items = new(Enumerable.Range(0, HotbarSize + InventorySize).Select(x => (ItemStack?)null));
        public override ItemStack? this[int index] { get => Items[index]; set => Items[index] = value; }
        int _selectedIndex = 0;
        public int SelectedIndex { get => _selectedIndex; set { if (value >= 0 && value < HotbarSize) _selectedIndex = value; } }
        public ItemStack? SelectedItem => this[SelectedIndex];
        public override int GetSize() => Items.Count();
        public override List<ItemStack?> GetItemStacks() => Items;
        public override void RemoveItem(int index) => Items.RemoveAt(index);
        public override void RemoveItem(int index, ulong count)
        {
            var result = Items[index];
            ItemStack stack = Items[index] ?? throw new NullReferenceException("Tried to remove an empty slot");
            if (stack.Amount == count)
                RemoveItem(index);
            else
            {
                stack.Amount = checked(stack.Amount - count);
                Items[index] = stack;
            }
        }
        bool IsHotbarSlot(int index) => index >= 0 && index < HotbarSize;
        // a has remaining space for b if it isn't empty, they are the same and a isn't already at the maximum amount
        bool HasRemainingSpaceForItem(ItemStack? a, ItemStack b) => a.HasValue && a.Value.Item.GetItemComparer() == b.Item.GetItemComparer() && a.Value.Amount < a.Value.Item.MaxStackSize() && a.Value.Amount < MaxStackSize;
        // tries the currently selected slot first, then the other slots
        int GetSlotWithRemainingSpace(ItemStack stack) => HasRemainingSpaceForItem(this[SelectedIndex], stack) ? SelectedIndex : GetItemStacks().FindIndex(x => HasRemainingSpaceForItem(x, stack));
        int GetFreeSlot() => Items.FindIndex(x => !x.HasValue);
        void AddItem(int index, ItemStack toAdd) => this[index] = new ItemStack() { Item = toAdd.Item, Amount = (this[index]?.Amount ?? 0) + toAdd.Amount };
        public ItemStack? PlaceItemInInventory(ItemStack stack)
        {
            int size = GetSize();
            for (int i = 0; i < size && stack.Amount > 0; ++size)
            {
                int possibleSlot = GetSlotWithRemainingSpace(stack);
                if (possibleSlot == -1) possibleSlot = GetFreeSlot();
                if (possibleSlot != -1)
                {
                    ulong amountToMove = Math.Min(stack.Amount, (this[possibleSlot]?.Item.MaxStackSize() ?? 64));
                    AddItem(possibleSlot, stack.Split(amountToMove));
                    continue;
                }
            }
            return stack.Amount > 0 ? stack : null;
        }
    }
}