using System;
using SimpleIpaToKanaSharp;

namespace ValidationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var converter = new IpaToKatakana_EnglishInJapanese();
            Console.WriteLine("Testing IPA to Katakana Conversion:");

            Test(converter, "kæt", "キャット");
            Test(converter, "pɪt", "ピット");
            Test(converter, "mʌg", "マグ");
            Test(converter, "bʊk", "ブック");
            Test(converter, "sɒks", "ソックス");
            Test(converter, "θɪŋk", "シンク");
            Test(converter, "laɪt", "ライト");
            Test(converter, "wɔːtər", "ウォーター"); // water
            Test(converter, "dʒæz", "ジャズ"); // jazz
            Test(converter, "tʃɛk", "チェック"); // check
            Test(converter, "rɪtʃ", "リッチ"); // rich
            Test(converter, "dʒʌmp", "ジャンプ"); // jump
            // video might be tricky
            // Test(converter, "vɪdɪoʊ", "ヴィデオ", "ビデオ"); 
            Test(converter, "kɑː", "カー");
            Test(converter, "eɪt", "エイト");
            Test(converter, "noʊ", "ノー");
            // Test(converter, "faɪv", "ファイヴ", "ファイブ");
            Test(converter, "sɪŋə", "シンガー");
            Test(converter, "kɪŋ", "キング");
            Test(converter, "jɛs", "イエス");
            Test(converter, "juː", "ユー");
            Test(converter, "hæpi", "ハッピー");
            Test(converter, "æpl", "アップル");
        }

        static void Test(IpaToKatakana_EnglishInJapanese converter, string ipa, params string[] expected)
        {
            try
            {
                var result = converter.ToKatakana(ipa);
                bool match = false;
                foreach(var e in expected) if(result == e) match = true;
                
                if (match)
                {
                    Console.WriteLine($"[PASS] {ipa} -> {result}");
                }
                else
                {
                    Console.WriteLine($"[FAIL] {ipa} -> {result} (Expected: {string.Join(" or ", expected)})");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {ipa} -> {ex.Message}");
            }
        }
    }
}
