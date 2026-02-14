using Xunit;
using SimpleIpaToKanaSharp;

namespace SimpleIpaToKanaSharp.Tests
{
    public class IpaToKanaConverterTests
    {
        [Theory]
        [InlineData("Hello", "ハァルオー")]
        public void Convert_QkmaxwareEngine_ReturnsExpectedKana(string input, string expected)
        {
            var converter = new IpaToKanaConverter(IpaEngineType.Qkmaxware);
            var result = converter.Convert(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Hello", "ハェルオー")]
        public void Convert_MicrosoftEngine_ReturnsExpectedKana(string input, string expected)
        {
            var converter = new IpaToKanaConverter(IpaEngineType.Microsoft);
            var result = converter.Convert(input);
            Assert.Equal(expected, result);
        }
    }
}
