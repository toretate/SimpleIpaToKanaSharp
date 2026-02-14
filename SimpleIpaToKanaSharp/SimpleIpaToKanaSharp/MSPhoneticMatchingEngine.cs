using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.PhoneticMatching;

// Microsoft.PhoneticMatchingのソースコード
// https://github.com/microsoft/PhoneticMatching/tree/master/src/cs
// ドキュメント
// https://microsoft.github.io/PhoneticMatching/

namespace SimpleIpaToKanaSharp
{

    public class MSPhoneticMatchingEngine : IIpaConverter
    {
        // CMU音素→IPA変換テーブル
        private static readonly Dictionary<string, string> CmuToIpa = new()
        {
            {"AA", "ɑ"}, {"AE", "æ"}, {"AH", "ʌ"}, {"AO", "ɔ"}, {"AW", "aʊ"}, {"AY", "aɪ"},
            {"B", "b"}, {"CH", "tʃ"}, {"D", "d"}, {"DH", "ð"}, {"EH", "ɛ"}, {"ER", "ɝ"}, {"EY", "eɪ"},
            {"F", "f"}, {"G", "ɡ"}, {"HH", "h"}, {"IH", "ɪ"}, {"IY", "i"}, {"JH", "dʒ"}, {"K", "k"},
            {"L", "l"}, {"M", "m"}, {"N", "n"}, {"NG", "ŋ"}, {"OW", "oʊ"}, {"OY", "ɔɪ"}, {"P", "p"},
            {"R", "ɹ"}, {"S", "s"}, {"SH", "ʃ"}, {"T", "t"}, {"TH", "θ"}, {"UH", "ʊ"}, {"UW", "u"},
            {"V", "v"}, {"W", "w"}, {"Y", "j"}, {"Z", "z"}, {"ZH", "ʒ"}
        };

        // CMUストレス記号（0,1,2）は無視
        private static string RemoveStress(string phoneme)
        {
            if (phoneme.Length > 2 && char.IsDigit(phoneme[2]))
                return phoneme.Substring(0, 2);
            if (phoneme.Length > 1 && char.IsDigit(phoneme[1]))
                return phoneme.Substring(0, 1);
            return phoneme;
        }

        public string ToIpa(string word)
        {
            // 1. Pronouncerから結果オブジェクトを取得
            var pronouncer = EnPronouncer.Instance;
            var result = pronouncer.Pronounce(word); // 例: "HH AH0 L OW1"

            // 2. 結果自体がnull、または目的のArpabet形式の文字列が空でないかチェック
            // ※Microsoft.PhoneticMatchingでは .Arpabet プロパティに "HH AH0 L OW1" のような形式が入ります
            if (result == null || result.Phones == null || result.Phones.Count == 0)
            {
                return word;
            }

            var ipa = string.Join("", result.Phones.Select(p =>
            {
                string upperP = p.Phonation.ToString().ToUpperInvariant();
                var p2 = RemoveStress(upperP);
                return CmuToIpa.TryGetValue(p2, out var ipaVal) ? ipaVal : p2;
            }));
            return ipa;

            //if (string.IsNullOrWhiteSpace(cmu)) return word;
            //var ipa = string.Join("", cmu.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            //    .Select(p => {
            //        var p2 = RemoveStress(p.ToUpperInvariant());
            //        return CmuToIpa.TryGetValue(p2, out var ipaVal) ? ipaVal : p2;
            //    }));
            //return ipa;
        }
    }
}
