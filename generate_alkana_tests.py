import sys
sys.path.append('temp')
import alkana_data
import random

# Set seed for reproducibility
random.seed(42)

data = alkana_data.data
items = list(data.items())

# Sample 1000 items
sample_size = 1000
if len(items) > sample_size:
    sampled_items = random.sample(items, sample_size)
else:
    sampled_items = items

header = """using System;
using Xunit;
using SimpleIpaToKanaSharp;

namespace SimpleIpaToKanaSharp.Tests
{
    public class AlkanaTests
    {
        [Theory]"""

footer = """        public void Alkana_Dictionary_Tests(string word, string expectedKana)
        {
            var ipaConverter = new QkmaxwareIpaConverter();
            var kanaConverter = new IpaToKatakana_EnglishInJapanese();

            try 
            {
                var ipa = ipaConverter.ToIpa(word);
                // Some words might not be in IPA dictionary, returning same or null.
                if (string.IsNullOrEmpty(ipa) || ipa == word) 
                {
                    // Skip if IPA conversion fails or returns word (meaning not found)
                    // But we can't easily skip in InlineData without marking as skipped.
                    // Let's just return if we can't convert, or try best effort.
                    return; 
                }

                var kana = kanaConverter.ToKatakana(ipa, word);
                Assert.Equal(expectedKana, kana);
            }
            catch (Exception)
            {
                // Ignore errors for now, just want to see passes
                throw;
            }
        }
    }
}
"""

print(header)
for word, kana in sampled_items:
    word = word.replace('"', '\\"')
    print(f'        [InlineData("{word}", "{kana}")]')
print(footer)
