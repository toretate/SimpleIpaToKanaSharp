# SimpleIpaToKanaSharp

IPA（国際音声記号）を日本語のカタカナ表記に変換するための .NET ライブラリおよび CLI ツールです。

## 概要

このプロジェクトは、英語の IPA 表記をルールベースのアルゴリズムによって適切な日本語カタカナに変換することを目的としています。
単なる文字置換ではなく、日本語の音韻規則に配慮した変換を目指しています。

## 特徴

- **ルールベース変換**: 英語の発音特性を考慮した変換ロジック。
- **.NET 8 対応**: モダンな .NET 環境で動作。
- **CLI ツール**: コマンドラインから即座に変換をテスト可能。
- **拡張性**: 独自の変換ルールを追加可能なアーキテクチャ。

## クイックスタート

### ビルド

```bash
dotnet build
```

### CLI の使用例

`SimpleIpaToKanaSharpCli` プロジェクトを使用して、英単語を直接変換できます。

```powershell
dotnet run --project SimpleIpaToKanaSharpCli "Apple"
# Result: アップル
```

## 品質指標 (Test Metrics)

プロジェクトの品質を維持するため、2つの異なるデータセットを用いて自動テストを実施しています。

### 1. 大規模辞書テスト (alkana)
英和辞書データを用いた大規模な変換テストです。

- **総数**: 1,117 語
- **合格**: 628 語
- **合格率**: **56.2%**
- **詳細**: `SimpleIpaToKanaSharp.AlkanaTests`（旧プロジェクト）の実行結果を参照。

### 2. 基本単語テスト (Standard Loanwords)
一般的なカタカナ語（外来語）との一致を確認するテストです。

- **総数**: 103 語
- **合格**: 40 語
- **合格率**: **38.8%**
- **詳細**: `SimpleIpaToKanaSharp.xUnitTests`（旧プロジェクト）による検証結果を参照。詳細は [docs/KATAKANA_RESULTS.md](file:///c:/workspace/workspace-win/SimpleIpaToKanaSharp/docs/KATAKANA_RESULTS.md) を参照。

## 技術仕様・依存関係

### 利用ライブラリ
- **[Qkmaxware.Phonetics](https://www.nuget.org/packages/Qkmaxware.Phonetics)**: IPA のパースおよび音素解析に使用。

### 参考リファレンス
- [英語の発音をカタカナで表す方法](https://www.sljfaq.org/afaq/english-in-japanese.ja.html)
- [IPA表記 (Wikipedia)](https://en.wikipedia.org/wiki/International_Phonetic_Alphabet)

## ライセンス

[LICENSE](LICENSE) ファイルを参照してください。
