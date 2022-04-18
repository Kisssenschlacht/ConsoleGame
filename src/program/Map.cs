using System.Security.Cryptography;

namespace ConsoleGame
{
    partial class Program
    {
        TileType[,] Map { get => _state.Map; set => _state.Map = value; }
        void generateMap()
        {
            for(int i = 0; i < height; ++i){
                for(int k = 0; k < width; ++k){
                    Map[k, i] = placeTile();
                }
            }
        }
        TileType placeTile(){
            int random = RandomNumberGenerator.GetInt32(100);

            if(random < 2) return TileType.Tree;
            if(random < 5) return TileType.Stone;
            return TileType.Empty;
        }
        bool isIllegalPosition(){
            return isIllegalPosition(playerPosition) || isObstacle[Map[playerPosition.x, playerPosition.y]] ;
        }
        bool isIllegalPosition(Positions position){
            return position.x >= width || position.x < 0 || position.y >= height || position.y < 0;
        }
    }
}