# このプロジェクトは？

英単語をIPA表記からカタカナ表記に変換するツールです。


# 使い方

SimpleIpaToKanaSharpCli.exeを使用して、コマンドラインから変換を行うことができます。

```bash
SimpleIpaToKanaSharpCli.exe "Hello"
→ ハロー
```

# プロジェクト構成

- - SimpleIpaToKanaSharp  
  英単語をIPA表記からカタカナ表記に変換するライブラリです。
- SimpleIpaToKanaSharpCli  
  コマンドラインから使用するためのツールです。
- SimpleIpaToKanaSharp.Tests  
  単体テストを実行するためのプロジェクトです。

# 謝辞
このプロジェクトは、以下のリソースを参考にして作成されました。
- [System.Speech.Recognition](https://docs.microsoft.com/en-us/dotnet/api/system.speech.recognition?view=net-5.0)
- [英語の発音をカタカナで表す方法](https://www.sljfaq.org/afaq/english-in-japanese.ja.html)
- [IPA表記](https://en.wikipedia.org/wiki/International_Phonetic_Alphabet)
