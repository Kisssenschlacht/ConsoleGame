namespace ConsoleGame.Entities
{
    abstract class InventoryEntity : Entity
    {
        public Inventory Inventory { get; private set; }
        protected InventoryEntity(Map map, Position position) : base(map, position) { Inventory = new(); }
    }
}