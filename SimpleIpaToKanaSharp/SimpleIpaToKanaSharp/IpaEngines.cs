using Qkmaxware.Phonetics;

namespace SimpleIpaToKanaSharp
{
    public enum IpaEngineType
    {
        Qkmaxware
    }

    public interface IIpaConverter
    {
        string ToIpa(string word);
    }
}

