# Task List

- [x] Create a new test case for English to Katakana conversion <!-- id: 0 -->
    - [x] Create `EnglishToKatakanaTests.cs` in `SimpeIpaToKanaSharp.Test` with many test cases <!-- id: 1 -->
    - [x] Implement test logic using `QkmaxwareIpaConverter` and `IpaToKatakana_EnglishInJapanese` <!-- id: 2 -->
    - [x] Run and verify the test <!-- id: 3 -->

- [/] Fix IPA to Katakana Rules <!-- id: 4 -->
    - [x] Diagnose IPA output <!-- id: 5 -->
    - [x] Update `IpaToKatakana_EnglishInJapanese.cs` with new rules <!-- id: 6 -->
        - Define missing chars (`ɔ`, `ɑ`, `ɚ`, `ɹ`)
        - Define diphthongs (`ow`, `aj`, etc.)
        - Define consonant clusters (`kj`, `pj`, etc.)
        - Fix long vowel logic
    - [x] Verify with tests <!-- id: 7 -->

- [x] Reference 'transcript' Repo to Improve Rules <!-- id: 8 -->
    - [x] Clone repo <!-- id: 9 -->
    - [x] Analyze python code for mapping rules <!-- id: 10 -->
    - [x] Update `IpaToKatakana_EnglishInJapanese.cs` <!-- id: 11 -->
    - [x] Update tests to match Standard readings <!-- id: 12 -->

- [x] Import Alkana Test Cases <!-- id: 13 -->
    - [x] Fetch `alkana/data.py` <!-- id: 14 -->
    - [x] Create `AlkanaTests.cs` (Sampled 1000 words) <!-- id: 15 -->
    - [x] Run and Verification (Pass Rate: 49.8%) <!-- id: 16 -->

- [x] Refine Rules based on Alkana Tests <!-- id: 17 -->
    - [x] Analyze `alkana` test failures
    - [x] Identify common failure patterns (Suffixes, Prefixes, Sokuon)
    - [x] Implement targeted rule refinements
        - [x] Suffixes: `-tion` (Consolidated), `-em`, `-et`, `-en`
        - [x] Prefixes: `Re-`
        - [x] Sokuon: Stop + Vowel constraints (Skip unless `-er`/`-y`)
    - [x] Verify improvements with `TestRunner`
    - [x] Fix regressions (`n` mapping, compilation errors)
- [x] Debug and Fix runtime error in `EnglishToKatakanaTests`
    - [x] Create implementation plan
    - [x] Add null checks to `ToKatakana`
    - [x] Optimize test initialization
    - [x] Verify test execution
    - [x] Add 'Actual' values to test cases where conversion fails
- [x] Migrate Standard to Expect and Generate Report
    - [x] Create implementation plan
    - [x] Transform `EnglishToKatakanaTests.cs` (Standard -> Expect)
    - [x] Re-run tests and update `Actual` comments
    - [x] Generate `KATAKANA_RESULTS.md` for GitHub Pages
    - [x] Update `walkthrough.md`
