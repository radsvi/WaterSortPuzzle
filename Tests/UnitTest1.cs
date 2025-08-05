using WaterSortPuzzle.Models;
using FluentAssertions;

namespace Tests
{
    public class TestLevelPreferences : ILevelPreferences
    {
        public int Score { get; set; } = 50;
        public int Level { get; set; } = 2;
    }
    public class UnitTest1
    {
        //[Fact]
        //public void Test1()
        //{
        //    if (Sum(1, 3) != 4)
        //        throw new Exception();
        //}
        //int Sum(int left, int right)
        //{
        //    return left + right;
        //}
        [Fact]
        public void Score_500_should_return_NumberOfColors_7()
        {
            var prefs = new TestLevelPreferences();
            var leveling = new Leveling(prefs)
            {
                Score = 500
            };

            leveling.CalculateNextLevelParameters();

            int resultLow = 7;
            int resultHigh = resultLow + Constants.MinColors;

            leveling.NumberOfColorsToGenerate.Should().BeGreaterThanOrEqualTo(resultLow);
            leveling.NumberOfColorsToGenerate.Should().BeLessThanOrEqualTo(resultHigh);
        }
    }
}
