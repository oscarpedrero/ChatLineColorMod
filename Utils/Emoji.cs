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

        public static string convertCharactersToEoji(string text)
        {
            return text.Replace("xE", Grin)
                .Replace("xD", Joy)
                .Replace(":)", Smiley)
                .Replace(":D", Smile)
                .Replace(";D", Sweat_Smile)
                .Replace("lol", Laughing)
                .Replace("Lol", Laughing)
                .Replace(";)", Wink)
                .Replace("x)", Blush)
                .Replace(":P", Yum)
                .Replace("<3)", Heart_Eyes)
                .Replace(":*", Kissing_Heart);
            
        }

    }
}
