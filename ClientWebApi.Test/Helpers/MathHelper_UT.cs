using ClientWebApi.Helpers;
using Xunit;

namespace ClientWebApi.Test.Helpers
{
    public class MathHelper_UT
    {
        [Theory]
        [InlineData(1, 2, 3, 1)]
        [InlineData(9, 5, 8, 5)]
        [InlineData(8, 9, 4, 4)]
        [InlineData(9, 9, 1, 1)]
        [InlineData(1, 1, 1, 1)]
        public void FindMinValue_OK(int x, int y, int z, int expectedResult)
        {
            var result = MathHelper.Min(x, y, z);
            Assert.Equal(result, expectedResult);
        }
    }
}
