namespace ConsoleGame
{
    partial class Program
    {
        void getMovement(){
            Positions lastPosition = playerPosition;
            
            switch (lastPressed.Key)
            {
                case ConsoleKey.A:
                --playerPosition.x;
                break;

                case ConsoleKey.D:
                ++playerPosition.x;
                break;

                case ConsoleKey.W:
                --playerPosition.y;
                break;

                case ConsoleKey.S:
                ++playerPosition.y;
                break;
            }
            if(isIllegalPosition()) playerPosition = lastPosition;
        }
    }
}