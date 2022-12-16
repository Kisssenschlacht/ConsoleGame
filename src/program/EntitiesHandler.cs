using System;
using System.Security.Cryptography;
using ConsoleGame.Entities;
namespace ConsoleGame
{
    partial class Program
    {
        void SpawnRandomEntity()
        {
            Position[] possiblePositionsArray =
                Enumerable.Range(0, Constants.width * Constants.height)
                .Select(z => new Position() { x = z % Constants.width, y = z / Constants.width })
                .Where(x => !_state.Map.Entities.Select(y => y.Position).Contains(x)).ToArray();
            var chosenPosition = possiblePositionsArray.Random();
            if (chosenPosition == null) return;
            Position nonnullChosenPosition = (Position)chosenPosition;
            Func<Entity>? constructor = new Func<Entity>[] {
                () => new Entities.Cow(_state.Map, nonnullChosenPosition),
                () => new Entities.Sheep(_state.Map, nonnullChosenPosition)
            }.Random();
            Entity entity;
            if (constructor == null || (entity = constructor()) == null) return;
            _state.Map.Entities.Add(entity);
        }
    }
}