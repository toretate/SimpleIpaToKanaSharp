using Qkmaxware.Phonetics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleIpaToKanaSharp
{
    // Qkmaxware.Phonetics 
    // https://github.com/qkmaxware/CsPhonetics
    public class QkmaxwareIpaConverter : IIpaConverter
    {
        private readonly IPA _ipa = new IPA();

        public string ToIpa(string word)
        {
            // Qkmaxware.PhoneticsのAPIでIPA取得
            var ipa = _ipa.EnglishToIPA(word); // 例: "həˈloʊ"
            return string.IsNullOrWhiteSpace(ipa) ? word : ipa;
        }
    }
}
