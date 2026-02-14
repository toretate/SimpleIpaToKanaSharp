using System;
using SimpleIpaToKanaSharp;
using Qkmaxware.Phonetics;

namespace ValidationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var ipaConverter = new QkmaxwareIpaConverter();
            var kanaConverter = new IpaToKatakana_EnglishInJapanese();
            
            // List of words causing issues
            var words = new[] { 
                "Computer", "Soccer", "Robot", "Box", "Fox", "Money", "Monkey", "Company", "Color", "Cover"
            };

            foreach (var word in words)
            {
                var ipa = ipaConverter.ToIpa(word);
                var kana = kanaConverter.ToKatakana(ipa, word);
                Console.WriteLine($"Word: {word}");
                Console.WriteLine($"IPA: {ipa}");
                Console.Write("IPA Hex: ");
                foreach (var c in ipa) Console.Write($"{(int)c:X4} ");
                Console.WriteLine();
                Console.WriteLine($"Kana: {kana}");
                Console.WriteLine(new string('-', 20));
            }
        }
    }
}
