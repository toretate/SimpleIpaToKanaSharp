# このプロジェクトは？

英単語をIPA表記からカタカナ表記に変換するツールです。


# 使い方

このプロジェクト自体はライブラリのため、SimpleIpaToKanaSharp.dllをプロジェクトに追加して、以下のように使用してください。
```csharp
using SimpleIpaToKanaSharp;

class Program
{
    static void Main(string[] args)
    {
        var converter = new IpaToKanaConverter();
        string word = "Hello";
        string kana = converter.Convert(word);
        Console.WriteLine(kana);
    }
}
この例では、Helloをカタカナ表記に変換しています。
```

# このライブラリの流れ

1. 英単語をIPA表記に変換する  
IPA表記への変換は、System.Speech.Recognitionを使用して行っています。

1. 2. IPA表記をカタカナ表記に変換する  
変換処理は https://www.sljfaq.org/afaq/english-in-japanese.ja.html の変換ルールを参考にしています。


# 謝辞
このプロジェクトは、以下のリソースを参考にして作成されました。
- [System.Speech.Recognition](https://docs.microsoft.com/en-us/dotnet/api/system.speech.recognition?view=net-5.0)
- [英語の発音をカタカナで表す方法](https://www.sljfaq.org/afaq/english-in-japanese.ja.html)
- [IPA表記](https://en.wikipedia.org/wiki/International_Phonetic_Alphabet)
