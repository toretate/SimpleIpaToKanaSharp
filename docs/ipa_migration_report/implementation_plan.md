# Standard to Expect Migration and Report Generation

The goal is to align the `Expect` values in `EnglishToKatakanaTests.cs` with the `Standard` values found in comments, and then generate a comprehensive report of the engine's performance for GitHub Pages.

## Proposed Changes

### [SimpleIpaToKanaSharp.Tests]

#### [MODIFY] [EnglishToKatakanaTests.cs](../../SimpleIpaToKanaSharp/SimpeIpaToKanaSharp.Test/EnglishToKatakanaTests.cs)
- Replace the second argument of `InlineData` with the value from the `Standard:` comment if present.
- Format: `[InlineData("Word", "StandardValue")] // (Actual: ...)`
- Re-run tests to populate the `Actual` value in comments if it differs from the new `Expect`.

### [Documentation]

#### [NEW] [KATAKANA_RESULTS.md](../KATAKANA_RESULTS.md)
- Create a Markdown file containing a table with: **Word**, **Expect (Standard)**, **Actual**.
- This will be used for GitHub Pages to showcase current engine accuracy.

## Verification Plan

### Automated Tests
- Run `dotnet test` on `EnglishToKatakanaTests`.
- Ensure `Actual` values are correctly captured and documented in the source file.
- Verify the generated Markdown table reflects the test results.
