namespace ConsoleGame
{
    abstract class DefaultItem : IItem
    {
        public void Update(TimeSpan elapsedTime) { }

        public abstract string DisplayName();

        public virtual ulong MaxStackSize() => 64;
    }
}