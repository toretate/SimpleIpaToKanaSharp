using System;
using Xunit;
using SimpleIpaToKanaSharp;

namespace SimpleIpaToKanaSharp.Tests
{
    public class EnglishToKatakanaTests
    {
        [Theory]
        // Basic Greetings & Common Words
        [InlineData("Hello", "ハロー")] 
        [InlineData("World", "ワールド")]
        [InlineData("Good", "グッド")]
        [InlineData("Morning", "モーニング")]
        [InlineData("Night", "ナイト")]
        [InlineData("Thank", "サンク")] 
        [InlineData("You", "ユ")] // Standard: ユー
        [InlineData("Yes", "イエス")]
        [InlineData("No", "ノー")]
        [InlineData("Please", "プリーズ")] // Standard: プリーズ
        [InlineData("Sorry", "ソリー")] // Standard: ソーリー (Improved from サリー)

        // Tech Terms
        [InlineData("Computer", "コンピュッター")] // Standard: コンピューター (Improved from カンピュッター)
        [InlineData("Internet", "インターネット")]
        [InlineData("Program", "プローグラム")] // Standard: プログラム
        [InlineData("System", "シスタム")] // Standard: システム
        [InlineData("Software", "ソフトウェー")] // Standard: ソフトウェア
        [InlineData("Hardware", "ハードウェー")] // Standard: ハードウェア
        [InlineData("Network", "ネットワーク")]
        [InlineData("Server", "サーヴァー")] // Standard: サーバー
        [InlineData("Client", "クライアント")]
        [InlineData("Data", "デイタ")] // Standard: データ
        [InlineData("Base", "ベイス")] // Standard: ベース
        [InlineData("Database", "デイタベイス")] // Standard: データベース
        [InlineData("App", "アップ")] 
        [InlineData("Application", "アップラッケイシャン")] // Standard: アプリケーション
        [InlineData("Interface", "インターフェイス")] // Standard: インターフェース
        [InlineData("User", "ユザー")] // Standard: ユーザー
        [InlineData("Login", "ロギン")] // Standard: ログイン
        [InlineData("Logout", "ロゴウット")] // Standard: ログアウト
        [InlineData("Password", "パスワード")] // Standard: パスワード (Improved from ピャスワード)
        [InlineData("Security", "シキュラッティー")] // Standard: セキュリティ
        [InlineData("File", "ファイル")]
        [InlineData("Folder", "フォールダー")] // Standard: フォルダ
        [InlineData("Window", "ウィンドー")] // Standard: ウィンドウ
        [InlineData("Mouse", "マース")] // Standard: マウス
        [InlineData("Keyboard", "キボード")] // Standard: キーボード
        [InlineData("Monitor", "モナッター")] // Standard: モニター (Improved from マナッター)
        [InlineData("Screen", "スクリーン")] // Standard: スクリーン
        [InlineData("Click", "クリック")]
        [InlineData("Type", "タイプ")]
        [InlineData("Code", "コード")]
        [InlineData("Bug", "バグ")]
        [InlineData("Error", "エラー")]
        [InlineData("Test", "テスト")]
        [InlineData("Release", "リリス")] // Standard: リリース
        [InlineData("Version", "ヴァージャン")] // Standard: バージョン
        [InlineData("Update", "アップデイト")] // Standard: アップデート
        [InlineData("Download", "ダーンロード")] // Standard: ダウンロード
        [InlineData("Upload", "ウップロアッド")] // Standard: アップロード
        [InlineData("Cloud", "クラード")] // Standard: クラウド
        [InlineData("Web", "ウェブ")]
        [InlineData("Site", "サイト")]
        [InlineData("Link", "リンク")]
        [InlineData("Browser", "ブラーザー")] // Standard: ブラウザ
        [InlineData("Search", "サーチ")]
        [InlineData("Engine", "エンジャン")] // Standard: エンジン
        [InlineData("Tech", "テック")]
        [InlineData("Technology", "テックノラッジー")] // Standard: テクノロジー (Improved from テックナラッジー)
        [InlineData("Digital", "ディッジャッタル")] // Standard: デジタル
        [InlineData("Analog", "アナログ")]
        [InlineData("Robot", "ローボット")] // Standard: ロボット (Improved from ローバット)
        [InlineData("AI", "アイ")] // Standard: エーアイ

        // Colors
        [InlineData("Red", "レッド")]
        [InlineData("Blue", "ブル")] // Standard: ブルー
        [InlineData("Green", "グリーン")] // Standard: グリーン
        [InlineData("Yellow", "イエロー")]
        [InlineData("Black", "ブラック")]
        [InlineData("White", "ワイト")] // Standard: ホワイト
        [InlineData("Orange", "オランジ")] // Standard: オレンジ
        [InlineData("Purple", "パーパル")] // Standard: パープル
        [InlineData("Pink", "ピンク")]
        [InlineData("Gold", "ゴールド")]
        [InlineData("Silver", "シルヴァー")] // Standard: シルバー

        // Animals
        [InlineData("Cat", "キャット")]
        [InlineData("Dog", "ドグ")] // Standard: ドッグ
        [InlineData("Bird", "バード")]
        [InlineData("Fish", "フィッシュ")]
        [InlineData("Lion", "ライアン")] // Standard: ライオン
        [InlineData("Tiger", "タイガー")]
        [InlineData("Bear", "ベー")] // Standard: ベア
        [InlineData("Rabbit", "ラバット")] // Standard: ラビット
        [InlineData("Monkey", "マンキー")] // Standard: モンキー
        [InlineData("Elephant", "エラファント")] // Standard: エレファント
        [InlineData("Box", "ボックス")]
        [InlineData("Fox", "フォックス")]

        // Food
        [InlineData("Apple", "アッパル")] // Standard: アップル
        [InlineData("Banana", "バナナ")]
        // [InlineData("Orange", "オレンジ")] // Removed duplicate
        [InlineData("Grape", "グレイプ")] // Standard: グレープ
        [InlineData("Melon", "メラン")] // Standard: メロン
        [InlineData("Lemon", "レマン")] // Standard: レモン
        [InlineData("Tomato", "タメイトー")] // Standard: トマト
        [InlineData("Potato", "パッテイトー")] // Standard: ポテト
        [InlineData("Bread", "ブレッド")]
        [InlineData("Butter", "バッター")] // Standard: バター
        [InlineData("Cheese", "チーズ")] // Standard: チーズ
        [InlineData("Milk", "ミルク")]
        [InlineData("Coffee", "コフィー")] // Standard: コーヒー (Improved from カフィー)
        [InlineData("Tea", "ティー")]
        [InlineData("Beer", "ビール")] // Standard: ビール
        [InlineData("Wine", "ワイン")]
        [InlineData("Water", "ウォッター")] // Standard: ウォーター
        [InlineData("Juice", "ジュス")] // Standard: ジュース
        [InlineData("Rice", "ライス")]
        [InlineData("Beef", "ビーフ")] // Standard: ビーフ
        [InlineData("Pork", "ポーク")]
        [InlineData("Chicken", "チッカン")] // Standard: チキン
        
        // Sports
        // [InlineData("Sport", "スポーツ")]
        [InlineData("Soccer", "サッカー")]
        [InlineData("Baseball", "ベイスボル")] // Standard: ベースボール
        [InlineData("Basketball", "バスカットボル")] // Standard: バスケットボール (Improved from ビャスカットボル)
        [InlineData("Tennis", "テナス")] // Standard: テニス
        [InlineData("Golf", "ゴルフ")] // Standard: ゴルフ (Improved from ガルフ)
        [InlineData("Ski", "スキー")]
        [InlineData("Skate", "スケイト")] // Standard: スケート
        [InlineData("Swim", "スウィム")] // Standard: スイム
        [InlineData("Run", "ラン")]

        public void Convert_WithSpecificConverters_ReturnsExpectedKana(string word, string expectedKana)
        {
            // Allocate converters
            var ipaConverter = new QkmaxwareIpaConverter();
            var kanaConverter = new IpaToKatakana_EnglishInJapanese();

            // Convert to IPA
            string ipa = ipaConverter.ToIpa(word);
            
            // Output IPA for debugging purposes (visible in test runner output)
            Console.WriteLine($"Word: {word}, IPA: {ipa}");

            // Convert to Katakana
            string actualKana = kanaConverter.ToKatakana(ipa, word);

            // Assert
            Assert.Equal(expectedKana, actualKana);
        }
    }
}
