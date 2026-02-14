using System;
using System.Linq;

namespace SimpleIpaToKanaSharp
{
    public class IpaToKanaConverter
    {
        private readonly IIpaConverter _ipaConverter;

        public IpaToKanaConverter(IpaEngineType engineType = IpaEngineType.Qkmaxware)
        {
            _ipaConverter = engineType switch
            {
                IpaEngineType.Qkmaxware => new QkmaxwareIpaConverter(),
                IpaEngineType.Microsoft => new MicrosoftIpaConverter(),
                _ => throw new ArgumentException($"Unknown engine: {engineType}")
            };
        }


        // 英単語をIPA表記に変換
        public string ToIpa(string word) => _ipaConverter.ToIpa(word);


        // 英単語→カタカナ
        public string Convert(string word)
        {
            var ipa = ToIpa(word);
            Console.WriteLine($"IPA: {ipa}");

            IpaToKatakana_KatakanaInIpaNotation ipaToKana = new();
            IpaToKatakana_EnglishInJapanese ipaToKanaEnglish = new();
            return ipaToKanaEnglish.ToKatakana(ipa, word);

            //return ipaToKana.ToKatakana(ipa);
        }
    }
}
