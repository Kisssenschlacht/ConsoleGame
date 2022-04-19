using System;
using System.Security.Cryptography;

namespace ConsoleGame
{
    partial class Program
    {
        public List<Entity> Entities { get => _state.Entities; set => _state.Entities = value; }
        void SpawnRandomEntity()
        {
            Positions[] notPossiblePositions = Entities.Select(i => i.Pos).ToArray();
            List<Positions> possiblePositionsList = new List<Positions>(width*height-notPossiblePositions.Length);
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    Positions currentPosition = new Positions { x = x, y = y };
                    if (notPossiblePositions.Contains(currentPosition)) continue;
                    possiblePositionsList.Add(currentPosition);
                }
            }

            Positions[] possiblePositionsArray = possiblePositionsList.ToArray();
            EntityType entityType = Enum.GetValues<EntityType>().Random();
            Entities.Add(new Entity(
                entityType,
                EntityHearts[entityType],
                possiblePositionsArray.Random()
            ));
        }
    }
}