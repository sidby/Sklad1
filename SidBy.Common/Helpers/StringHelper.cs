using System.Text;

namespace SidBy.Common.Helpers
{
    public static class StringHelper
    {
        public static string Transliterate(string source)
        {
            return Transliterate(source, true);
        }

        public static string Transliterate(string source, bool trimtolower)
        {
            const string chars_allowed = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-";

            const string chars_ru = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

            string[] chars_translit_ruen = {"a", "b","v","g","d","e","yo","zh","z","i","j","k","l","m","n","o","p","r","s","t","u","f","h","c","ch","sh","sch","","y","", "e","yu", "ya",
                  "A", "B","V","G","D","E","YO","ZH","Z","I","J","K","L","M","N","O","P","R","S","T","U","F","H","C","CH","SH","SCH","","Y","", "E","YU", "YA"};

            StringBuilder sb = new StringBuilder();
            if (trimtolower)
            {
                source = source.Trim().ToLower();
            }
            foreach (char c in source)
            {
                if (c == ' ')
                {
                    sb.Append('-');
                }
                else if (chars_allowed.IndexOf(c) >= 0)
                {
                    sb.Append(c);
                }
                else if (chars_ru.IndexOf(c) >= 0)
                {
                    int index = chars_ru.IndexOf(c);
                    sb.Append(chars_translit_ruen[index]);
                }
            }
            return sb.ToString();
        }
    }
}
