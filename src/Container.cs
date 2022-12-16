namespace ConsoleGame
{
    abstract class Container
    {
        public abstract int GetSize();
        public abstract ItemStack? this[int index] { get; set; }
        public virtual IEnumerable<ItemStack?> GetItemStacks() => Enumerable.Range(0, GetSize()).Select(x => this[x]);
        public abstract void RemoveItem(int index);
        public abstract void RemoveItem(int index, ulong count);
        public abstract ulong MaxStackSize { get; }
        public bool CanInteractWith(Entity entity) => true;
        public virtual void Open(Entity entity) { }
        public virtual void Close(Entity entity) { }
        public bool CanPlaceItem(int index, ItemStack? stack) => true;
        public ulong CountItem(IItem item) => CountItem(item.GetItemComparer());
        public ulong CountItem(ItemComparer itemComparer) =>
            GetItemStacks().Aggregate<ItemStack?, ulong>(0, (i, x) =>
                x.HasValue && x.Value.Item.GetItemComparer() == itemComparer ?
                (i + x.Value.Amount) : i);
        public bool HasAnyOf(IItem item) => HasAnyOf(item.GetItemComparer());
        public bool HasAnyOf(ItemComparer itemComparer) =>
            GetItemStacks().Any(x => x.HasValue && x.Value.Item.GetItemComparer() == itemComparer);
    }
}