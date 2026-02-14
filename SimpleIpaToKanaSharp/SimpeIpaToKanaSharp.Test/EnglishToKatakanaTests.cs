using System;
using Xunit;
using SimpleIpaToKanaSharp;

namespace SimpleIpaToKanaSharp.Tests
{
    public class EnglishToKatakanaTests
    {
        private readonly QkmaxwareIpaConverter _ipaConverter;
        private readonly IpaToKatakana_EnglishInJapanese _kanaConverter;
        private readonly Xunit.Abstractions.ITestOutputHelper _output;

        public EnglishToKatakanaTests(Xunit.Abstractions.ITestOutputHelper output)
        {
            _ipaConverter = new QkmaxwareIpaConverter();
            _kanaConverter = new IpaToKatakana_EnglishInJapanese();
            _output = output;
        }
        [Theory]
        // Basic Greetings & Common Words
        [InlineData("Hello", "ハロー")]
        [InlineData("World", "ワールド")]
        [InlineData("Good", "グッド")]
        [InlineData("Morning", "モーニング")]
        [InlineData("Night", "ナイト")]
        [InlineData("Thank", "サンク")]
        [InlineData("You", "ユー")] // (Actual: ユ)
        [InlineData("Yes", "イエス")]
        [InlineData("No", "ノー")]
        [InlineData("Please", "プリーズ")]
        [InlineData("Sorry", "ソーリー")] // (Actual: ソリー)

        // Tech Terms
        [InlineData("Computer", "コンピューター")] // (Actual: コンピュッター)
        [InlineData("Internet", "インターネット")]
        [InlineData("Program", "プログラム")] // (Actual: プローグラム)
        [InlineData("System", "システム")] // (Actual: シスタム)
        [InlineData("Software", "ソフトウェア")] // (Actual: ソフトゥウェー)
        [InlineData("Hardware", "ハードウェア")] // (Actual: ハードゥウェー)
        [InlineData("Network", "ネットワーク")] // (Actual: ネットゥワーク)
        [InlineData("Server", "サーバー")] // (Actual: サーヴァー)
        [InlineData("Client", "クライアント")]
        [InlineData("Data", "データ")] // (Actual: デイタ)
        [InlineData("Base", "ベース")] // (Actual: ベイス)
        [InlineData("Database", "データベース")] // (Actual: デイタベイス)
        [InlineData("App", "アップ")]
        [InlineData("Application", "アプリケーション")] // (Actual: アップラケイション)
        [InlineData("Interface", "インターフェース")] // (Actual: インターフェイス)
        [InlineData("User", "ユーザー")] // (Actual: ユザー)
        [InlineData("Login", "ログイン")] // (Actual: ロギン)
        [InlineData("Logout", "ログアウト")] // (Actual: ロゴウット)
        [InlineData("Password", "パスワード")]
        [InlineData("Security", "セキュリティ")] // (Actual: シキュラッティー)
        [InlineData("File", "ファイル")]
        [InlineData("Folder", "フォルダ")] // (Actual: フォールダー)
        [InlineData("Window", "ウィンドウ")] // (Actual: ウィンドー)
        [InlineData("Mouse", "マウス")] // (Actual: マース)
        [InlineData("Keyboard", "キーボード")] // (Actual: キボード)
        [InlineData("Monitor", "モニター")] // (Actual: モナッター)
        [InlineData("Screen", "スクリーン")]
        [InlineData("Click", "クリック")]
        [InlineData("Type", "タイプ")]
        [InlineData("Code", "コード")]
        [InlineData("Bug", "バグ")]
        [InlineData("Error", "エラー")]
        [InlineData("Test", "テスト")]
        [InlineData("Release", "リリース")] // (Actual: リーリース)
        [InlineData("Version", "バージョン")] // (Actual: ヴァージョン)
        [InlineData("Update", "アップデート")] // (Actual: アップデイト)
        [InlineData("Download", "ダウンロード")] // (Actual: ダーンロード)
        [InlineData("Upload", "アップロード")] // (Actual: ウップロアッド)
        [InlineData("Cloud", "クラウド")] // (Actual: クラード)
        [InlineData("Web", "ウェブ")]
        [InlineData("Site", "サイト")]
        [InlineData("Link", "リンク")]
        [InlineData("Browser", "ブラウザ")] // (Actual: ブラーザー)
        [InlineData("Search", "サーチ")] // (Actual: サーチュ)
        [InlineData("Engine", "エンジン")] // (Actual: エンジョン)
        [InlineData("Tech", "テック")]
        [InlineData("Technology", "テクノロジー")] // (Actual: テックノラッジー)
        [InlineData("Digital", "デジタル")] // (Actual: ディジャタル)
        [InlineData("Analog", "アナログ")]
        [InlineData("Robot", "ロボット")] // (Actual: ローボット)
        [InlineData("AI", "エーアイ")] // (Actual: アイ)

        // Colors
        [InlineData("Red", "レッド")] // (Actual: リッド)
        [InlineData("Blue", "ブルー")] // (Actual: ブル)
        [InlineData("Green", "グリーン")]
        [InlineData("Yellow", "イエロー")]
        [InlineData("Black", "ブラック")]
        [InlineData("White", "ホワイト")] // (Actual: ワイト)
        [InlineData("Orange", "オレンジ")] // (Actual: オランジュ)
        [InlineData("Purple", "パープル")] // (Actual: パープル)
        [InlineData("Pink", "ピンク")]
        [InlineData("Gold", "ゴールド")]
        [InlineData("Silver", "シルバー")] // (Actual: シルヴァー)

        // Animals
        [InlineData("Cat", "キャット")]
        [InlineData("Dog", "ドッグ")] // (Actual: ドグ)
        [InlineData("Bird", "バード")]
        [InlineData("Fish", "フィッシュ")]
        [InlineData("Lion", "ライオン")] // (Actual: ライアン)
        [InlineData("Tiger", "タイガー")]
        [InlineData("Bear", "ベア")] // (Actual: べー)
        [InlineData("Rabbit", "ラビット")] // (Actual: ラバット)
        [InlineData("Monkey", "モンキー")] // (Actual: マンキー)
        [InlineData("Elephant", "エレファント")] // (Actual: エラファント)
        [InlineData("Box", "ボックス")] // (Actual: ボクス)
        [InlineData("Fox", "フォックス")] // (Actual: フォクス)

        // Food
        [InlineData("Apple", "アップル")] // (Actual: アプル)
        [InlineData("Banana", "バナナ")]
        // [InlineData("Orange", "オレンジ")] // Removed duplicate
        [InlineData("Grape", "グレープ")] // (Actual: グレイプ)
        [InlineData("Melon", "メロン")] // (Actual: メラン)
        [InlineData("Lemon", "レモン")] // (Actual: レマン)
        [InlineData("Tomato", "トマト")] // (Actual: タメイトー)
        [InlineData("Potato", "ポテト")] // (Actual: パテイトー)
        [InlineData("Bread", "ブレッド")]
        [InlineData("Butter", "バター")] // (Actual: バッター)
        [InlineData("Cheese", "チーズ")]
        [InlineData("Milk", "ミルク")]
        [InlineData("Coffee", "コーヒー")] // (Actual: コフィー)
        [InlineData("Tea", "ティー")]
        [InlineData("Beer", "ビール")] // (Actual: ビー)
        [InlineData("Wine", "ワイン")]
        [InlineData("Water", "ウォーター")] // (Actual: ウォッター)
        [InlineData("Juice", "ジュース")] // (Actual: ジュス)
        [InlineData("Rice", "ライス")]
        [InlineData("Beef", "ビーフ")]
        [InlineData("Pork", "ポーク")]
        [InlineData("Chicken", "チキン")] // (Actual: チクン)

        // Sports
        // [InlineData("Sport", "スポーツ")]
        [InlineData("Soccer", "サッカー")]
        [InlineData("Baseball", "ベースボール")] // (Actual: ベイスボル)
        [InlineData("Basketball", "バスケットボール")] // (Actual: バスカットゥボル)
        [InlineData("Tennis", "テニス")] // (Actual: テナス)
        [InlineData("Golf", "ゴルフ")]
        [InlineData("Ski", "スキー")]
        [InlineData("Skate", "スケート")] // (Actual: スケイト)
        [InlineData("Swim", "スイム")] // (Actual: スウィム)
        [InlineData("Run", "ラン")]

        public void Convert_WithSpecificConverters_ReturnsExpectedKana(string word, string expectedKana)
        {
            // Convert to IPA
            string ipa = _ipaConverter.ToIpa(word);

            // Convert to Katakana
            string actualKana = _kanaConverter.ToKatakana(ipa, word);

            _output.WriteLine($"Word: {word}, IPA: {ipa}, Expected: {expectedKana}, Actual: {actualKana}");

            // Assert
            Assert.Equal(expectedKana, actualKana);
        }
    }
}
