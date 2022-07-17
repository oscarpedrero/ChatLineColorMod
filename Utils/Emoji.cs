using System.Text.RegularExpressions;

namespace ChatLineColorMod.Utils
{
    internal class Emoji
    {

        public const string Grin = "😁";
        public const string Joy = "😂";
        public const string Smiley = "😃";
        public const string Smile = "😄";
        public const string Sweat_Smile = "😅";
        public const string Laughing = "😆";
        public const string Wink = "😉";
        public const string Blush = "😊";
        public const string Yum = "😋";
        public const string Heart_Eyes = "😍";
        public const string Kissing_Heart = "😘";

        private static RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Multiline;


        public static string convertCharactersToEoji(string text)
        {

            var pattern = @"(😂)[D]+";
            
            foreach (Match m in Regex.Matches(text, pattern, options))
            {
                text = text.Replace("😂D", Joy);
                break;
            }


            return text.Replace("xE", Grin)
                .Replace("xD", Joy)
                .Replace("XD", Joy)
                .Replace(":)", Smiley)
                .Replace(":D", Smile)
                .Replace(";D", Sweat_Smile)
                .Replace("lol", Laughing)
                .Replace("Lol", Laughing)
                .Replace("LoL", Laughing)
                .Replace("LOL", Laughing)
                .Replace(";)", Wink)
                .Replace("x)", Blush)
                .Replace(":P", Yum)
                .Replace("<3)", Heart_Eyes)
                .Replace(":*", Kissing_Heart);
            
        }

    }
}
