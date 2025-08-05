using WaterSortPuzzle.Models;

namespace Tests
{
    public class TestLevelPreferences : ILevelPreferences
    {
        public int Score { get; set; } = 50;
        public int Level { get; set; } = 2;
    }
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            if (Sum(1, 3) != 4)
                throw new Exception();
        }
        int Sum(int left, int right)
        {
            return left + right;
        }
        [Fact]
        public void Test2()
        {
            var prefs = new TestLevelPreferences();
            var leveling = new Leveling(prefs);

            var qwer = leveling.NumberOfColorsToGenerate;
            leveling.Score = 500;
            leveling.CalculateNextLevelParameters();

            var asdf = leveling.NumberOfColorsToGenerate;
            Console.WriteLine(asdf);

            //throw new Exception();


        }
    }
}
