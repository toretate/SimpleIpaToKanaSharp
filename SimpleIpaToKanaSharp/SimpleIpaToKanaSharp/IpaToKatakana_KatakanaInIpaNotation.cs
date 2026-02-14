using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleIpaToKanaSharp
{
    // IPA表記をカタカナ表記に変換するためのクラス
    // https://syllabary.sourceforge.net/Katakana/Katakana.html

    internal class IpaToKatakana_KatakanaInIpaNotation
    {
        public string ToKatakana( string ipa )
        {
            if (string.IsNullOrWhiteSpace(ipa)) return string.Empty;

            // IPA記号とカタカナの簡易マッピング（必要に応じて追加・修正可能）
            var ipaToKana = new Dictionary<string, string>
            {
                // 母音
                {"a", "ア"}, {"i", "イ"}, {"u", "ウ"}, {"e", "エ"}, {"o", "オ"},
                // 子音＋母音
                {"ka", "カ"}, {"ki", "キ"}, {"ku", "ク"}, {"ke", "ケ"}, {"ko", "コ"},
                {"sa", "サ"}, {"si", "シ"}, {"su", "ス"}, {"se", "セ"}, {"so", "ソ"},
                {"ta", "タ"}, {"ti", "チ"}, {"tu", "ツ"}, {"te", "テ"}, {"to", "ト"},
                {"na", "ナ"}, {"ni", "ニ"}, {"nu", "ヌ"}, {"ne", "ネ"}, {"no", "ノ"},
                {"ha", "ハ"}, {"hi", "ヒ"}, {"hu", "フ"}, {"he", "ヘ"}, {"ho", "ホ"},
                {"ma", "マ"}, {"mi", "ミ"}, {"mu", "ム"}, {"me", "メ"}, {"mo", "モ"},
                {"ya", "ヤ"}, {"yu", "ユ"}, {"yo", "ヨ"},
                {"ra", "ラ"}, {"ri", "リ"}, {"ru", "ル"}, {"re", "レ"}, {"ro", "ロ"},
                {"wa", "ワ"}, {"wo", "ヲ"},
                // その他
                {"n", "ン"},
                {"ɯ", "ウ"}, {"ɪ", "イ"}, {"ʊ", "ウ"}, {"ɛ", "エ"}, {"ɔ", "オ"},
                {"ʃ", "シ"}, {"ʒ", "ジ"}, {"tʃ", "チ"}, {"dʒ", "ヂ"},
                {"ts", "ツ"}, {"ɲ", "ニ"}, {"ɾ", "ラ"}, {"ŋ", "ン"},
                {"ɡ", "ガ"}, {"b", "バ"}, {"d", "ダ"}, {"g", "ガ"}, {"p", "パ"}, {"t", "タ"}, {"k", "カ"},
                {"s", "ス"}, {"z", "ズ"}, {"h", "ハ"}, {"f", "フ"}, {"v", "ヴ"}, {"m", "ム"}, {"j", "イ"}, {"w", "ウ"},
            };

            // 長い記号から順に置換（例: tʃ → チ, ʃ → シ）
            var orderedKeys = ipaToKana.Keys.OrderByDescending(k => k.Length);
            var result = ipa;
            foreach (var key in orderedKeys)
            {
                result = result.Replace(key, ipaToKana[key]);
            }
            return result;
        }
    }
}
