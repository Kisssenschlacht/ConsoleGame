namespace ConsoleGame.Tiles
{
    class Grass : StagedMaterialTileBreakOutputItem
    {
        static readonly char[][,] textures = new char[][,] {
            Constants.FromString(new string[] { "   ", " \" ", "   " }),
            Constants.FromString(new string[] {" \" ", "\"\"\"", " \" " }),
            Constants.FromString(new string[] {"\"\"\"", "\"\"\"", "\"\"\"" })
        };
        protected override int TotalStages => 3;
        readonly int[] GrowthTimesInMilliseconds = new int[] { 10000, 10000, 10000 };
        protected override int GrowthTimeInMilliseconds(int stage) => GrowthTimesInMilliseconds[stage];
        public Grass(Map map, Position position) : base(map, position)
        {
        }
        public override char[][,] Textures => textures;
        public override IItem BreakOutputItem => new Items.Sapling();
    }
}