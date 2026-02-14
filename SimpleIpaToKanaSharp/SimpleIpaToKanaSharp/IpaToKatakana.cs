using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleIpaToKanaSharp
{
    public interface IpaToKatakana
    {
        public string ToKatakana(string ipa, string word = null);
    }
}
