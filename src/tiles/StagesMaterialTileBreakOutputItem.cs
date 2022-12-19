namespace ConsoleGame.Tiles
{
    abstract class StagedMaterialTileBreakOutputItem : MaterialTileBreakOutputItem
    {
        int _stage;
        protected int Stage
        {
            get => _stage;
            set
            {
                _stage = value;
                if (_stage >= TotalStages - 1)
                {
                    FinalStageReached();
                    _stage = TotalStages - 1;
                }

                else MillisecondsToWait = GrowthTimeInMilliseconds(_stage);
            }
        }
        protected abstract int TotalStages { get; }
        double MillisecondsToWait;
        protected virtual void FinalStageReached() { }
        protected abstract int GrowthTimeInMilliseconds(int stage);
        protected StagedMaterialTileBreakOutputItem(Map map, Position position) : base(map, position)
        {
            MillisecondsToWait = GrowthTimeInMilliseconds(0);
        }
        public override void Update(TimeSpan elapsedTime)
        {
            if (_stage != TotalStages)
            {
                MillisecondsToWait -= elapsedTime.TotalMilliseconds;
                if (MillisecondsToWait <= 0) ++Stage;
            }
        }
        public override char[,] Texture => Textures[Stage];
        public abstract char[][,] Textures { get; }
    }
}