# SimpleIpaToKanaSharp

IPA（国際音声記号）を日本語のカタカナ表記に変換するための .NET ライブラリおよび CLI ツールです。

## 概要

このプロジェクトは、英語の IPA 表記をルールベースのアルゴリズムによって適切な日本語カタカナに変換することを目的としています。
単なる文字置換ではなく、日本語の音韻規則に配慮した変換を目指しています。

__結論としては、このままルールベースで実装するよりも、HMMやG2P, ONNX(LLM) を使った方が良いと考えています。__   
ANTLRを用いたルールベースへの再実装も考えましたが、確率論によるHMMやG2Pの方が確率的に正確な結果を出せると考えています。   

[sci.ang.japan の English to katakana converter](https://www.sljfaq.org/cgi/e2k.cgi)のサイトにある変換器はどうなってるんでしょうね・・・？

当初はカタカナのIPAはある程度決まっているし何とかなるでしょと思ってましたが、英語発音ってそもそもカタカナ発音とは全然違うってのを忘れてました（トマトとか）

## 今後（この方向性だと）

IPA通さずに CMUから変換？
参考： https://a244.hateblo.jp/entry/2016/08/25/063000

英単語をローマ字読み → IPA/CM → カタカナ？

## 現状の変換結果

* [1000語の変換結果(bep-eng.dicからの抜き出し)](docs/ALKANA_RESULTS.md) 19.9% (199/1000)
* [AIに出してもらった英単語→カタカナ語の変換結果](docs/KATAKANA_RESULTS.md) 38.8% (40/103)


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
bep-eng.dic から抽出された英単語を用いた大規模な変換テストです。

- **総数**: 1,000 語
- **合格**: 199 語
- **合格率**: **19.9%**
- **詳細**: `SimpleIpaToKanaSharp.AlkanaTests` による検証結果を参照。詳細は [docs/ALKANA_RESULTS.md](docs/ALKANA_RESULTS.md) を参照。

### 2. 基本単語テスト (Standard Loanwords)
一般的なカタカナ語（外来語）との一致を確認するテストです。

- **総数**: 103 語
- **合格**: 40 語
- **合格率**: **38.8%**
- **詳細**: `SimpleIpaToKanaSharp.xUnitTests`（旧プロジェクト）による検証結果を参照。詳細は [docs/KATAKANA_RESULTS.md](file:///c:/workspace/workspace-win/SimpleIpaToKanaSharp/docs/KATAKANA_RESULTS.md) を参照。

## 技術仕様・依存関係

### 利用ライブラリ
- **[Qkmaxware.Phonetics](https://www.nuget.org/packages/Qkmaxware.Phonetics)**: IPA のパースおよび音素解析に使用。
- **Antigravity**: AIアシスタントとして利用

### 参考リファレンス
- [英語の発音をカタカナで表す方法](https://www.sljfaq.org/afaq/english-in-japanese.ja.html)
- [IPA表記 (Wikipedia)](https://en.wikipedia.org/wiki/International_Phonetic_Alphabet)
- [alkana.py の data.py](https://github.com/zomysan/alkana.py/blob/master/alkana/data.py)
- [English to Katakana transcription](https://github.com/yokolet/transcript)
- [WaniKani Japanese Phonoogy Chart](https://community.wanikani.com/t/japanese-phonology-chart-with-ipa/48255)
- [Katakana in IPA Notation](https://syllabary.sourceforge.net/Katakana/Katakana.html)
- [【python】機械学習ベースの英語-カタカナ変換ライブラリの比較](https://qiita.com/shimajiroxyz/items/a509acf5a188fc8002c2)
- [google-ime-user-dictionary-ja-en](https://github.com/KEINOS/google-ime-user-dictionary-ja-en?tab=readme-ov-file)
- [新語・固有表現に強い「mecab-ipadic-NEologd」の効果を調べてみた](https://engineering.linecorp.com/ja/blog/mecab-ipadic-neologd-new-words-and-expressions)
- [英和辞書データ『ejdict-hand』(配布版)](https://github.com/kujirahand/EJDict/blob/master/tools/README.md)
- [日本語WordNetを用いて、JSON形式で簡単に取り扱える辞書を作ってみました。](https://qiita.com/oyahiroki/items/973f5bb94cd3458983bd)

## ライセンス

alkana.py が GPL v2 なので、このプロジェクトも GPL v2
今後も更新するなら一度 akana.py 由来のコード削除、Apache License 2.0 にする予定
