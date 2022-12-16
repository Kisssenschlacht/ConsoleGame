namespace ConsoleGame.Entities
{
    abstract class Mob : Entity
    {
        protected TimeSpan timer;
        TimeSpan idleTime = TimeSpan.FromSeconds(2);

        protected Mob(Map map, Position position) : base(map, position) { }

        public abstract void Action();
        public override void Update(TimeSpan elapsedTime)
        {
            timer += elapsedTime;
            if (timer >= idleTime)
            {
                timer -= idleTime;
                Action();
            }
        }
    }
}