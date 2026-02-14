using SimpleIpaToKanaSharp;

string? engine = null;
string? word = null;

for (int i = 0; i < args.Length; i++)
{
    if (args[i] == "--engine" && i + 1 < args.Length)
    {
        engine = args[i + 1];
        i++;
    }
    else if (word == null)
    {
        word = args[i];
    }
}

if (string.IsNullOrEmpty(word))
{
    Console.WriteLine("Usage: SimpleIpaToKanaSharpCli [--engine qkmaxware|microsoft] <word>");
    return;
}

var engineType = engine?.ToLower() switch
{
    "microsoft" => IpaEngineType.Microsoft,
    _ => IpaEngineType.Qkmaxware
};

var converter = new IpaToKanaConverter(engineType);
string kana = converter.Convert(word);
Console.WriteLine(kana);
