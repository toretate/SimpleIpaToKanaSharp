using Qkmaxware.Phonetics;

namespace SimpleIpaToKanaSharp
{
    public enum IpaEngineType
    {
        Qkmaxware,
        Microsoft
    }

    public interface IIpaConverter
    {
        string ToIpa(string word);
    }


    // MicrosoftIpaConverterとしてMSPhoneticMatchingEngineを利用
    public class MicrosoftIpaConverter : IIpaConverter
    {
        private readonly MSPhoneticMatchingEngine _engine = new MSPhoneticMatchingEngine();
        public string ToIpa(string word) => _engine.ToIpa(word);
    }
}
