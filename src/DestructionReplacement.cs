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
        DestroyBlockReturnType destroyBlock()
        {
            Position destructionPosition = playerPosition;
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
            if (_state.Map.IsIllegalPosition(destructionPosition)) return DestroyBlockReturnType.IllegalPosition;
            if (!(_state.Map.Tiles[destructionPosition.x, destructionPosition.y]?.IsObstacle ?? false)) return DestroyBlockReturnType.NoObstacle;
            _state.Map.Tiles[destructionPosition.x, destructionPosition.y]?.OnBreak(_state.Map.Player);
            return DestroyBlockReturnType.Ok;
        }
        PlaceBlockReturnType placeBlock()
        {
            Position placementPosition = playerPosition;
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
            if (_state.Map.IsIllegalPosition(placementPosition)) return PlaceBlockReturnType.IllegalPosition;
            if (_state.Map.Tiles[placementPosition.x, placementPosition.y] != null) return PlaceBlockReturnType.NotEmpty;
            if (!_state.Map.Player.Inventory.SelectedItem.HasValue)
            {
                return PlaceBlockReturnType.NoAction;
            }
            var item = _state.Map.Player.Inventory.SelectedItem?.Item as IPlaceable;
            if (item == null) return PlaceBlockReturnType.NoAction;
            item.Place(_state.Map, placementPosition);
            _state.Map.Player.Inventory.RemoveItem(_state.Map.Player.Inventory.SelectedIndex, 1);
            return PlaceBlockReturnType.Ok;
        }
    }
}