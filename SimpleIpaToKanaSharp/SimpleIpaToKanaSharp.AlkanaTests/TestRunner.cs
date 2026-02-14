using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var outputPath = "alkana_analysis_legacy.csv";

            if (File.Exists(csvPath))
            {
                var lines = File.ReadAllLines(csvPath);
                var results = new List<TestResultRow>();

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
                            if (string.IsNullOrEmpty(ipa)) ipa = "N/A";

                            var actual = kanaConverter.ToKatakana(ipa, word);
                            var result = (actual == expected) ? "PASS" : "FAIL";

                            writer.WriteLine($"{word},{ipa},{expected},{actual},{result}");
                            results.Add(new TestResultRow { Word = word, Ipa = ipa, Expected = expected, Actual = actual, Result = result });

                            if (result == "FAIL")
                            {
                                Console.WriteLine($"FAIL: {word} -> IPA:{ipa} Exp:{expected} Act:{actual}");
                            }
                        }
                        catch (Exception ex)
                        {
                            writer.WriteLine($"{word},ERROR,{expected},ERROR,ERROR");
                            results.Add(new TestResultRow { Word = word, Ipa = "ERROR", Expected = expected, Actual = "ERROR", Result = "ERROR" });
                            Console.WriteLine($"ERROR: {word} - {ex.Message}");
                        }
                    }
                }
                Console.WriteLine($"Analysis complete. Saved to {outputPath}");

                // Generate Markdown report
                GenerateMarkdownReport(results);
            }
            else
            {
                Console.WriteLine("CSV file not found.");
            }
        }

        static void GenerateMarkdownReport(List<TestResultRow> results)
        {
            var total = results.Count;
            var pass = results.Count(r => r.Result == "PASS");
            var rate = total > 0 ? Math.Round((double)pass / total * 100, 1) : 0;

            Console.WriteLine($"Generating Markdown report for {total} results...");

            // Try to find project root by looking for solution file or .git directory
            var currentDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string rootPath = null;
            while (currentDir != null)
            {
                // Root is usually where .slnx or .gitignore is (not the sub-dir ones)
                if (currentDir.GetFiles("*.slnx").Length > 0 || currentDir.GetFiles(".gitignore").Length > 0)
                {
                    rootPath = currentDir.FullName;
                    // BUT, if this is inside "SimpleIpaToKanaSharp" subfolder, continue up
                    if (currentDir.Name != "SimpleIpaToKanaSharp" || currentDir.Parent?.GetFiles(".gitignore").Length == 0)
                    {
                        break;
                    }
                }
                currentDir = currentDir.Parent;
            }

            if (rootPath == null)
            {
                Console.WriteLine("Error: Could not find project root directory.");
                return;
            }

            var docPath = Path.Combine(rootPath, "docs", "ALKANA_RESULTS_LEGACY.md");

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("# Alkana Conversion Results (Legacy Version)");
            sb.AppendLine();
            sb.AppendLine("This document shows the results of a large-scale conversion test using the legacy Alkana algorithm (commit: e4b4f70, 1,000 words).");
            sb.AppendLine();
            sb.AppendLine("## Summary");
            sb.AppendLine($"- **Total Cases**: {total}");
            sb.AppendLine($"- **Pass Count**: {pass}");
            sb.AppendLine($"- **Pass Rate**: {rate}%");
            sb.AppendLine();
            sb.AppendLine("## Conversion Table");
            sb.AppendLine();
            sb.AppendLine("| Word | IPA | Expect | Actual | Result |");
            sb.AppendLine("|---|---|---|---|---|");

            foreach (var res in results)
            {
                var icon = res.Result == "PASS" ? "✅" : "❌";
                var word = res.Word?.Replace("|", "\\|");
                var ipa = res.Ipa?.Replace("|", "\\|");
                var expected = res.Expected?.Replace("|", "\\|");
                var actual = res.Actual?.Replace("|", "\\|");

                sb.AppendLine($"| {word} | {ipa} | {expected} | {actual} | {icon} |");
            }

            try
            {
                File.WriteAllText(docPath, sb.ToString(), System.Text.Encoding.UTF8);
                Console.WriteLine($"[DEBUG] Successfully wrote report to {docPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEBUG] Error writing Markdown report: {ex.Message}");
            }
        }
    }

    class TestResultRow
    {
        public string Word { get; set; }
        public string Ipa { get; set; }
        public string Expected { get; set; }
        public string Actual { get; set; }
        public string Result { get; set; }
    }
}
