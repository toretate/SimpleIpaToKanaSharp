using Xunit;
using SimpleIpaToKanaSharp;

namespace SimpleIpaToKanaSharp.xUnitTests
{
    public class IpaToKanaConverterTests
    {
        [Theory]
        [InlineData("Hello", "ハロー")]
        [InlineData("World", "ワールド")]
        public void Convert_QkmaxwareEngine_ReturnsExpectedKana(string input, string expected)
        {
            var converter = new IpaToKanaConverter(IpaEngineType.Qkmaxware);
            var result = converter.Convert(input);
            Assert.Equal(expected, result);
        }


    }
}
