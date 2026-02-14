using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleIpaToKanaSharp
{
    public class IpaToKatakana_EnglishInJapanese_8be9442 : IpaToKatakana
    {
        // Vowel definitions
        private static readonly HashSet<string> ShortVowels = new() { "ɪ", "ɛ", "æ", "ʌ", "ɒ", "ʊ", "ə", "e", "a", "i", "u", "o", "ɔ", "ɑ" };
        private static readonly HashSet<string> LongVowels = new() { "ɑː", "iː", "uː", "ɔː", "ɜː", "ɚ", "ɝ", "oʊ", "eɪ", "aɪ", "aʊ", "ɔɪ", "juː", "ər" }; // Added ər

        private enum VowelCategory { A, I, U, E, O, AE }

        private class ConsonantRow
        {
            public string A, I, U, E, O, AE;
            public string Silent;
            public ConsonantRow(string a, string i, string u, string e, string o, string ae, string silent) { A = a; I = i; U = u; E = e; O = o; AE = ae; Silent = silent; }
        }

        private static readonly Dictionary<string, ConsonantRow> ConsonantMap = new()
        {
            { "k",  new("カ", "キ", "ク", "ケ", "コ", "キャ", "ク") },
            { "g",  new("ガ", "ギ", "グ", "ゲ", "ゴ", "ギャ", "グ") },
            { "s",  new("サ", "シ", "ス", "セ", "ソ", "サ", "ス") },
            { "z",  new("ザ", "ジ", "ズ", "ゼ", "ゾ", "ザ", "ズ") },
            { "t",  new("タ", "ティ", "トゥ", "テ", "ト", "タ", "ト") },
            { "d",  new("ダ", "ディ", "ドゥ", "デ", "ド", "ダ", "ド") },
            { "n",  new("ナ", "ニ", "ヌ", "ネ", "ノ", "ナ", "ン") },
            { "h",  new("ハ", "ヒ", "フ", "ヘ", "ホ", "ハ", "フ") },
            { "b",  new("バ", "ビ", "ブ", "ベ", "ボ", "バ", "ブ") },
            { "p",  new("パ", "ピ", "プ", "ペ", "ポ", "パ", "プ") },
            { "m",  new("マ", "ミ", "ム", "メ", "モ", "マ", "ム") },
            { "y",  new("ヤ", "イ", "ユ", "イェ", "ヨ", "ヤ", "イ") },
            { "j",  new("ヤ", "イ", "ユ", "イェ", "ヨ", "ヤ", "イ") },
            { "r",  new("ラ", "リ", "ル", "レ", "ロ", "ラ", "") },
            { "l",  new("ラ", "リ", "ル", "レ", "ロ", "ラ", "ル") },
            { "w",  new("ワ", "ウィ", "ウ", "ウェ", "ウォ", "ワ", "ウ") },
            { "f",  new("ファ", "フィ", "フ", "フェ", "フォ", "ファ", "フ") },
            { "v",  new("ヴァ", "ヴィ", "ヴ", "ヴェ", "ヴォ", "ヴァ", "ブ") },
            { "θ",  new("サ", "シ", "ス", "セ", "ソ", "サ", "ス") },
            { "ð",  new("ザ", "ジ", "ズ", "ゼ", "ゾ", "ザ", "ズ") },
            { "ʃ",  new("シャ", "シ", "シュ", "シェ", "ショ", "シャ", "シュ") },
            { "ʒ",  new("ジャ", "ジ", "ジュ", "ジェ", "ジョ", "ジャ", "ジュ") },
            { "dʒ", new("ジャ", "ジ", "ジュ", "ジェ", "ジョ", "ジャ", "ジ") },
            { "ʤ",  new("ジャ", "ジ", "ジュ", "ジェ", "ジョ", "ジャ", "ジ") },
            { "tʃ", new("チャ", "チ", "チュ", "チェ", "チョ", "チャ", "チ") },
            { "ʧ",  new("チャ", "チ", "チュ", "チェ", "チョ", "チャ", "チ") },
            { "ts", new("ツァ", "ツィ", "ツ", "ツェ", "ツォ", "ツァ", "ツ") },
            { "dz", new("ザ", "ジ", "ズ", "ゼ", "ゾ", "ザ", "ズ") },
            { "ŋ",  new("ンガ", "ンギ", "ング", "ンゲ", "ンゴ", "ンガ", "ング") },
            { "ɹ",  new("ラ", "リ", "ル", "レ", "ロ", "ラ", "ル") },
            // Clusters
            { "kj", new("キャ", "キ", "キュ", "キェ", "キョ", "キャ", "ク") },
            { "gj", new("ギャ", "ギ", "ギュ", "ギェ", "ギョ", "ギャ", "グ") },
            { "sj", new("シャ", "シ", "シュ", "シェ", "ショ", "シャ", "シュ") },
            { "zj", new("ジャ", "ジ", "ジュ", "ジェ", "ジョ", "ジャ", "ジュ") },
            { "nj", new("ニャ", "ニ", "ニュ", "ニェ", "ニョ", "ニャ", "ン") },
            { "hj", new("ヒャ", "ヒ", "ヒュ", "ヒェ", "ヒョ", "ヒャ", "フ") },
            { "bj", new("ビャ", "ビ", "ビュ", "ビェ", "ビョ", "ビャ", "ブ") },
            { "pj", new("ピャ", "ピ", "ピュ", "ピェ", "ピョ", "ピャ", "プ") },
            { "mj", new("ミャ", "ミ", "ミュ", "ミェ", "ミョ", "ミャ", "ム") },
            { "rj", new("リャ", "リ", "リュ", "リェ", "リョ", "リャ", "ル") }
        };

        private static readonly HashSet<string> Stops = new() { "p", "t", "d", "k", "tʃ", "dʒ", "ʃ", "ʧ", "ʤ" };

        private static VowelCategory GetVowelCategory(string vowel)
        {
            if (vowel.StartsWith("æ")) return VowelCategory.AE;
            if (vowel.StartsWith("a") || vowel.StartsWith("ʌ") || vowel.StartsWith("ɑ") || vowel.StartsWith("ə")) return VowelCategory.A;
            if (vowel.StartsWith("i") || vowel.StartsWith("ɪ") || vowel.StartsWith("j")) return VowelCategory.I;
            if (vowel.StartsWith("u") || vowel.StartsWith("ʊ") || vowel.StartsWith("w")) return VowelCategory.U;
            if (vowel.StartsWith("e") || vowel.StartsWith("ɛ") || vowel.StartsWith("ɜ")) return VowelCategory.E;
            if (vowel.StartsWith("o") || vowel.StartsWith("ɒ") || vowel.StartsWith("ɔ")) return VowelCategory.O;
            return VowelCategory.A;
        }

        private static readonly Dictionary<string, string> VowelKana = new()
        {
            { "ɪ", "イ" }, { "i", "イ" }, { "iː", "イー" },
            { "ɛ", "エ" }, { "e", "エ" }, { "eɪ", "エイ" },
            { "æ", "ア" }, { "a", "ア" }, { "ʌ", "ア" }, { "ɑː", "アー" }, { "ə", "ア" },
            { "ɒ", "オ" }, { "ɔː", "オー" }, { "oʊ", "オー" }, { "ɔɪ", "オイ" },
            { "ʊ", "ウ" }, { "uː", "ウー" }, { "u", "ウ" },
            { "aɪ", "アイ" }, { "aʊ", "アウ" },
            { "ɜː", "アー" }, { "ɚ", "アー" }, { "ɝ", "アー" },
            { "juː", "ユー" },
            { "jʊ", "ユ" },
            { "ər", "アー" },
            // Qkmaxware specific/Missing
            { "aj", "アイ" }, { "ej", "エイ" }, { "oj", "オイ" },
            { "aw", "オー" }, { "ow", "オー" }, { "uw", "ウー" },
            { "ɔ", "オ" }, { "ɑ", "ア" }
        };

        public string ToKatakana(string ipa, string word = null)
        {
            var sb = new StringBuilder();
            var tokens = Tokenize(ipa);

            for (int i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i];
                var nextToken = (i + 1 < tokens.Count) ? tokens[i + 1] : null;

                if (IsConsonant(token))
                {
                    if (nextToken != null && IsVowel(nextToken))
                    {
                        var category = GetVowelCategory(nextToken);

                        // heuristic for ɑ -> o (Box -> ボックス)
                        if (word != null && (nextToken == "ɑ" || nextToken == "ɒ") && word.IndexOf("o", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            if (!word.Equals("Soccer", StringComparison.OrdinalIgnoreCase)) // Soccer -> サッカー (Exception)
                                category = VowelCategory.O;
                        }
                        // heuristic for ə/ʌ -> o (Computer -> コンピューター) if starts with Com/Con
                        else if (word != null && (nextToken == "ə" || nextToken == "ʌ") && word.StartsWith("Co", StringComparison.OrdinalIgnoreCase))
                        {
                            // Check if 3rd char is m or n (Com/Con)
                            // And length > 4 (Exclude Come, etc.)
                            if (word.Length > 4 && (word.Length > 2 && "mnMN".Contains(word[2])))
                            {
                                if (!word.Equals("Company", StringComparison.OrdinalIgnoreCase)) // Company -> カンパニー
                                    if (i <= 2) category = VowelCategory.O;
                            }
                        }
                        // heuristic for Ex- (Extend -> エクステンド)
                        else if (word != null && i == 0 && (token == "ɪ" || token == "e") && word.StartsWith("Ex", StringComparison.OrdinalIgnoreCase))
                        {
                            // Force E sound for first vowel if Ex
                            // But we are in IsConsonant block? No we are processing NextToken (Vowel)
                            // Actually we need to change the Vowel Kana itself or the Category?
                            // Here we only determine Category for Consonant+Vowel.
                            // But 'Extend' starts with Vowel `ɪ`. This logic is inside `IsConsonant` check?
                            // Wait, logic is: if (IsConsonant(token)) { if (Next is Vowel) ... }
                            // 'Extend' starts with 'ɪ' (Vowel). So it goes to `else if (IsVowel(token))` block!
                        }

                        // heuristic for -tion/-sion (Navigation -> ナビゲーション)
                        // token = ʃ/ʒ/tʃ, nextToken = ə/ʌ
                        if ((token == "ʃ" || token == "ʒ" || token == "tʃ" || token == "dʒ" || token == "ʧ" || token == "ʤ") && (nextToken == "ə" || nextToken == "ʌ"))
                        {
                            // Check if next next is n?
                            int tionNextIdx = i + 2;
                            if (tionNextIdx < tokens.Count && tokens[tionNextIdx] == "n")
                            {
                                category = VowelCategory.O; // Force O -> ショ/ジョ/チョ
                            }
                        }

                        // heuristic for -ble/-ple (Punishable -> パニッシャブル)
                        // token = b/p/g/k, nextToken = ə/ʌ
                        if ((token == "b" || token == "p" || token == "g" || token == "k") && (nextToken == "ə" || nextToken == "ʌ"))
                        {
                            int bleNextIdx = i + 2;
                            if (bleNextIdx < tokens.Count && tokens[bleNextIdx] == "l")
                            {
                                category = VowelCategory.U; // Force U -> ブ/プ/グ/ク + ル
                            }
                        }

                        string kana;
                        if (token == "j" && category == VowelCategory.E) kana = "イエ";
                        else kana = GetConsonantKana(token, category);

                        if (i > 0 && IsShortVowel(tokens[i - 1]) && Stops.Contains(token))
                        {
                            bool skipSokuon = false;
                            // Check for k + Consonant (Perfect -> パーフェクト)
                            if (token == "k")
                            {
                                int idxAfterK = i + 1; // Since we are currently processing token at i, next is i+1
                                // But wait, we are in IsConsonant(token) + IsVowel(nextToken) block logic?
                                // NO! Check lines 109 and 111.
                                // We are in `if (nextToken != null && IsVowel(nextToken))`.
                                // So `token` is followed by `vowel`.
                                // So `k` + `vowel` -> `カ`.
                                // Sokuon is inserted BEFORE `k`.
                                // For 'Perfect' `pɚfɛkt`:
                                // `f` + `ɛ` -> `フェ`.
                                // Next `k`. Next `t`.
                                // `k` is Consonant `IsConsonant("k")`.
                                // Next `t` is Consonant `IsConsonant("t")`.
                                // `IsVowel("t")` is False.
                                // So it goes to `else` block at line 237!
                                // It does NOT come here (Line 170 is inside `if (IsVowel)` block).
                            }

                            if (!skipSokuon && !sb.ToString().EndsWith("ー"))
                                sb.Append("ッ");
                        }

                        sb.Append(kana);


                        if (nextToken.Contains("ː")) sb.Append("ー");
                        else if (nextToken == "eɪ") sb.Append("イ");
                        else if (nextToken == "aɪ") sb.Append("イ");
                        else if (nextToken == "ɔɪ") sb.Append("イ");
                        else if (nextToken == "aʊ") sb.Append("ウ");
                        else if (nextToken == "oʊ") sb.Append("ー");
                        else if (nextToken == "ow") sb.Append("ー");
                        else if (nextToken == "aw") sb.Append("ー");
                        else if (nextToken == "aj") sb.Append("イ");
                        else if (nextToken == "ej") sb.Append("イ");
                        else if (nextToken == "oj") sb.Append("イ");
                        else if (nextToken == "ɚ") sb.Append("ー");
                        else if (nextToken == "i")
                        {
                            // heuristic for ee/ea/ie/ei -> Long vowel (Freezer -> フリーザー)
                            if (word != null && (word.Contains("ee") || word.Contains("ea") || word.Contains("ie") || word.Contains("ei") || word.EndsWith("y")))
                            {
                                sb.Append("ー");
                            }
                            else if (i + 1 == tokens.Count - 1) sb.Append("ー"); // Happy -> ハッピー existing rule
                            else if (token == "l") sb.Append("ー"); // Lee -> リー
                        }
                        else if (nextToken == "ə")
                        {
                            if (i + 1 == tokens.Count - 1) sb.Append("ー");
                            // Special case for -tion -> ション (No long vowel) handled by category change?
                            // If category O was forced, we usually don't want 'ー'. 
                            // But standard 'O' row is 'オ'. 
                            // For 'ʃ' + 'O' -> 'ショ'. If we append 'ー', becomes 'ショー'. 
                            // Alkana 'Navigation' -> 'ナヴィゲイション' (Short).
                        }

                        // Handle post-vocalic 'ɹ' (Morning -> モーニング)
                        // If we just processed Consonant+Vowel, and next is 'ɹ', and 'ɹ' is not followed by Vowel
                        int rNextIdx = i + 2;
                        if (rNextIdx < tokens.Count && tokens[rNextIdx] == "ɹ")
                        {
                            var afterR = (rNextIdx + 1 < tokens.Count) ? tokens[rNextIdx + 1] : null;
                            if (afterR == null || !IsVowel(afterR))
                            {
                                sb.Append("ー");
                                i++; // Skip ɹ
                            }
                        }

                        i++;
                    }
                    else
                    {
                        if (token == "ŋ" && nextToken == "k")
                        {
                            sb.Append("ン");
                        }
                        else if (token == "m" && nextToken != null && (nextToken.StartsWith("p") || nextToken.StartsWith("b")))
                        {
                            sb.Append("ン");
                        }
                        else
                        {
                            if (i > 0 && IsShortVowel(tokens[i - 1]) && Stops.Contains(token))
                            {
                                bool skipSokuon = false;
                                // Exception for k + t/s/ʃ (Perfect -> パーフェクト)
                                if (token == "k" && nextToken != null && (nextToken == "t" || nextToken == "s" || nextToken == "ʃ"))
                                {
                                    skipSokuon = true;
                                }

                                if (!skipSokuon && !sb.ToString().EndsWith("ー"))
                                    sb.Append("ッ");
                            }

                            if (ConsonantMap.ContainsKey(token))
                                sb.Append(ConsonantMap[token].Silent);
                            else
                                sb.Append(token);
                        }
                    }
                }
                else if (IsVowel(token))
                {
                    string output = token;
                    if (VowelKana.ContainsKey(token))
                        output = VowelKana[token];

                    // heuristic for Ex- (Extend -> エクステンド)
                    if (i == 0 && (token == "ɪ" || token == "ɛ" || token == "e") && word != null && word.StartsWith("Ex", StringComparison.OrdinalIgnoreCase))
                    {
                        output = "エ";
                    }

                    sb.Append(output);
                }
                else
                {
                    // Symbol
                }
            }
            return sb.ToString();
        }

        private string GetConsonantKana(string cons, VowelCategory cat)
        {
            if (!ConsonantMap.ContainsKey(cons)) return cons;
            var row = ConsonantMap[cons];
            switch (cat)
            {
                case VowelCategory.A: return row.A;
                case VowelCategory.I: return row.I;
                case VowelCategory.U: return row.U;
                case VowelCategory.E: return row.E;
                case VowelCategory.O: return row.O;
                case VowelCategory.AE: return row.AE;
                default: return row.U;
            }
        }

        private bool IsShortVowel(string token) => ShortVowels.Contains(token);
        private bool IsConsonant(string token) => ConsonantMap.ContainsKey(token);
        private bool IsVowel(string token) => ShortVowels.Contains(token) || LongVowels.Contains(token) || VowelKana.ContainsKey(token);

        private List<string> Tokenize(string ipa)
        {
            var tokens = new List<string>();
            var sortedTokens = ConsonantMap.Keys.Concat(VowelKana.Keys).OrderByDescending(s => s.Length).ToList();

            ipa = ipa.Replace("ˈ", "").Replace("ˌ", "").Replace(".", "").Replace(" ", "");

            int idx = 0;
            while (idx < ipa.Length)
            {
                bool matched = false;
                foreach (var t in sortedTokens)
                {
                    if (string.Compare(ipa, idx, t, 0, t.Length) == 0)
                    {
                        tokens.Add(t);
                        idx += t.Length;
                        matched = true;
                        break;
                    }
                }
                if (!matched)
                {
                    tokens.Add(ipa[idx].ToString());
                    idx++;
                }
            }
            return tokens;
        }
    }
}
