namespace ConsoleGame{
    partial class Program{
        Dictionary<Item, int> inventory { get => _state.Inventory; set => _state.Inventory = value; }
        int _selectedIndex = 0;
        int SelectedIndex { get => _selectedIndex; set { if (value >= 0 && value < inventory.Count) _selectedIndex = value; } }
        ItemAmount SelectedItem { get {  KeyValuePair<Item, int> kvp = inventory.ElementAt(SelectedIndex); return new ItemAmount(kvp.Key, kvp.Value); } }
        void checkSelectedIndex(){
            if (lastPressed.Key >= ConsoleKey.D0 && lastPressed.Key <= ConsoleKey.D9)
            {
                SelectedIndex = (lastPressed.Key - ConsoleKey.D0 - 1) % 10;
            }
            switch (lastPressed.Key){
                case ConsoleKey.OemPlus:
                ++SelectedIndex;
                break;
                case ConsoleKey.OemMinus:
                --SelectedIndex;
                break;
            }
        }
        void AddToSelectedItem(int amount)
        {
            int newAmount = SelectedItem.Amount + amount;
            if (newAmount < 1)
            {
                inventory.Remove(SelectedItem.Item);
                if (SelectedIndex == inventory.Count)
                {
                    --SelectedIndex;
                }
                return;
            }
            inventory[SelectedItem.Item] = newAmount;
        }

        void AddToInventory(Item item, int amount)
        {
            if (inventory.ContainsKey(item))
            {
                int newAmount = inventory[item] + amount;
                if (newAmount < 1)
                {
                    inventory.Remove(item);
                    return;
                }
                inventory[item] = newAmount;
                return;
            }
            if (amount > 0) inventory.Add(item, amount);
            else throw new Exception("Tried to add a new item to the inventory with non positive amount");
        }
    }
}