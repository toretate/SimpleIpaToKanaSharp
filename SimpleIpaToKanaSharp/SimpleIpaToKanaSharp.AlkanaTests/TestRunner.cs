using System;
using SimpleIpaToKanaSharp;
using Qkmaxware.Phonetics;

namespace SimpleIpaToKanaSharp.AlkanaTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var ipaConverter = new QkmaxwareIpaConverter();
            var kanaConverter = new IpaToKatakana_EnglishInJapanese();

            var csvPath = "alkana_samples.csv";
            var outputPath = "alkana_analysis.csv";

            if (File.Exists(csvPath))
            {
                var lines = File.ReadAllLines(csvPath);
                using (var writer = new StreamWriter(outputPath))
                {
                    writer.WriteLine("Word,IPA,Expected,Actual,Result");
                    foreach (var line in lines.Skip(1)) // Skip header
                    {
                        var parts = line.Split(',');
                        if (parts.Length < 2) continue;

                        var word = parts[0];
                        var expected = parts[1];

                        try
                        {
                            var ipa = ipaConverter.ToIpa(word);
                            // Some words fail or return null/empty
                            if (string.IsNullOrEmpty(ipa)) ipa = "N/A";

                            var actual = kanaConverter.ToKatakana(ipa, word);
                            var result = (actual == expected) ? "PASS" : "FAIL";

                            writer.WriteLine($"{word},{ipa},{expected},{actual},{result}");

                            if (result == "FAIL")
                            {
                                Console.WriteLine($"FAIL: {word} -> IPA:{ipa} Exp:{expected} Act:{actual}");
                            }
                        }
                        catch (Exception ex)
                        {
                            writer.WriteLine($"{word},ERROR,{expected},ERROR,ERROR");
                            Console.WriteLine($"ERROR: {word} - {ex.Message}");
                        }
                    }
                }
                Console.WriteLine($"Analysis complete. Saved to {outputPath}");
            }
            else
            {
                Console.WriteLine("CSV file not found.");
            }
        }
    }
}
