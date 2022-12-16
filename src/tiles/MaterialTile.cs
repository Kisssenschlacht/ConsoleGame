using ConsoleGame.Entities;

namespace ConsoleGame.Tiles
{
    abstract class MaterialTile : Tile
    {
        protected MaterialTile(Map map, Position position) : base(map, position) { }
        public override bool IsObstacle => true;
        public abstract override char[,] Texture { get; }
        public abstract LootTable LootTable { get; }
        public override void Break(Entity entity)
        {
            Break(entity, ((entity as InventoryEntity) ?? throw new Exception()).Inventory.PlaceItemInInventory);
            Map.Tiles[Position.x, Position.y] = null;
        }
        public virtual void Break(Entity entity, Func<ItemStack, ItemStack?> addToInventory) => LootTable(entity).ForEach(x => addToInventory(x));
    }
}