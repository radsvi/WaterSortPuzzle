using WaterSortPuzzle.Models;
using FluentAssertions;

namespace Tests
{
    public class TestLevelPreferences : ILevelPreferences
    {
        public int Score { get; set; } = 50;
        public int Level { get; set; } = 2;
    }
    public class LevelingTests
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
        public void Score_500_should_have_NumberOfColors_between_8_and_10()
        {
            var prefs = new TestLevelPreferences();
            var leveling = new Leveling(prefs)
            {
                Score = 500
            };

            leveling.CalculateNextLevelParameters();

            //int resultHigh = 7 + Constants.MinColors;
            int resultHigh = 10;
            int resultLow = resultHigh - 2;

            leveling.NumberOfColorsToGenerate.Should().BeGreaterThanOrEqualTo(resultLow);
            leveling.NumberOfColorsToGenerate.Should().BeLessThanOrEqualTo(resultHigh);
        }
        [Fact]
        public void Score_2892_should_have_NumberOfColors_between_11_and_12()
        {
            var prefs = new TestLevelPreferences();
            var leveling = new Leveling(prefs)
            {
                Score = 2892
            };

            //int resultHigh = Constants.ColorCount;
            int resultHigh = 12;
            int resultLow = resultHigh - 1;

            leveling.CalculateNextLevelParameters();

            leveling.NumberOfColorsToGenerate.Should().BeGreaterThanOrEqualTo(resultLow);
            leveling.NumberOfColorsToGenerate.Should().BeLessThanOrEqualTo(resultHigh);
        }
        [Fact]
        public void Score_2082_should_have_NumberOfColors_between_11_and_12()
        {
            var prefs = new TestLevelPreferences();
            var leveling = new Leveling(prefs)
            {
                Score = 2082
            };

            //int resultHigh = Constants.ColorCount;
            int resultHigh = 12;
            int resultLow = resultHigh - 1;

            leveling.CalculateNextLevelParameters();

            leveling.NumberOfColorsToGenerate.Should().BeGreaterThanOrEqualTo(resultLow);
            leveling.NumberOfColorsToGenerate.Should().BeLessThanOrEqualTo(resultHigh);
        }
        [Fact]
        public void Score_2892_should_have_NumberOfColors_12_75_percent_of_the_time()
        {
            var prefs = new TestLevelPreferences();
            var leveling = new Leveling(prefs)
            {
                Score = 2892
            };

            //int resultHigh = Constants.ColorCount;
            int resultHigh = 12;
            //int resultLow = resultHigh - 1;
            int iterations = 10000;
            int counter = 0;

            for (int i = 0; i < iterations; i++)
            {
                leveling.CalculateNextLevelParameters();

                if (leveling.NumberOfColorsToGenerate == resultHigh)
                    counter++;
            }
            counter.Should().BeGreaterThan(7400);
            counter.Should().BeLessThan(7600);
        }
    }
}
