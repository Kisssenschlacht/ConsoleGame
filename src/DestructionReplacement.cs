namespace ConsoleGame
{
    enum DestroyBlockReturnType : byte
    {
        Ok = 0b0,
        NoAction = 0b01,
        NoObstacle = 0b10,
        IllegalPosition = 0b11
    }
    enum PlaceBlockReturnType : byte
    {
        Ok = 0b0,
        NoAction = 0b01,
        NotEmpty = 0b10,
        IllegalPosition = 0b11
    }
    partial class Program
    {
        DestroyBlockReturnType destroyBlock(){
            Positions destructionPosition = playerPosition;
            switch (lastPressed.Key)
            {
                case ConsoleKey.RightArrow:
                ++destructionPosition.x;
                break;
                case ConsoleKey.LeftArrow:
                --destructionPosition.x;
                break;
                case ConsoleKey.UpArrow:
                --destructionPosition.y;
                break;
                case ConsoleKey.DownArrow:
                ++destructionPosition.y;
                break;
                default: return DestroyBlockReturnType.NoAction;
            }
            if (isIllegalPosition(destructionPosition)) return DestroyBlockReturnType.IllegalPosition;
            if (!isObstacle[Map[destructionPosition.x, destructionPosition.y]]) return DestroyBlockReturnType.NoObstacle;
            List<ItemAmount> list = itemForTileType[Map[destructionPosition.x, destructionPosition.y]]();
            foreach (ItemAmount itemAmount in list)
            {
                AddToInventory(itemAmount.Item, itemAmount.Amount);
            }
            Map[destructionPosition.x, destructionPosition.y] = TileType.Empty;
            return DestroyBlockReturnType.Ok;
        }
        PlaceBlockReturnType placeBlock(){
            Positions placementPosition = playerPosition;
            switch (lastPressed.Key)
            {
                case ConsoleKey.RightArrow:
                ++placementPosition.x;
                break;
                case ConsoleKey.LeftArrow:
                --placementPosition.x;
                break;
                case ConsoleKey.UpArrow:
                --placementPosition.y;
                break;
                case ConsoleKey.DownArrow:
                ++placementPosition.y;
                break;
                default: return PlaceBlockReturnType.NoAction;
            }
            if (isIllegalPosition(placementPosition)) return PlaceBlockReturnType.IllegalPosition;
            if (Map[placementPosition.x, placementPosition.y] != TileType.Empty) return PlaceBlockReturnType.NotEmpty;
            if (inventory.Count == 0)
            {
                return PlaceBlockReturnType.NoAction;
            }
            Map[placementPosition.x, placementPosition.y] = tileTypeForItem[SelectedItem.Item];
            AddToSelectedItem(-1);
            return PlaceBlockReturnType.Ok;
        }
    }
}