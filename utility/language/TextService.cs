using System.Globalization;
using System.Threading;

namespace utility.language
{
    public static class TextService
    {
        private static readonly TextInfo TextInfo;
        static TextService()
        {
            TextInfo = (Thread.CurrentThread.CurrentCulture).TextInfo;
        }
        public static string ToTitleCase(string v)
        {
            return TextInfo.ToTitleCase(v);
        }

    }
}
