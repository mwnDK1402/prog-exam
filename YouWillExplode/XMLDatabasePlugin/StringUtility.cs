using System.Linq;
using System.Text;

namespace XMLDatabasePlugin
{
    internal static class StringUtility
    {
        public static string GetIncremented(this string str)
        {
            StringBuilder numBuilder = new StringBuilder();
            int i;
            for (i = str.Length - 1; i >= 0; --i)
            {
                if (!char.IsDigit(str[i]))
                {
                    break;
                }

                numBuilder.Append(str[i]);
            }

            if (numBuilder.Length == 0)
            {
                return str + "1";
            }

            var nonNumStr = str.Substring(0, i + 1);

            int num = int.Parse(
                new string(numBuilder.ToString().ToCharArray().Reverse().ToArray()));

            return nonNumStr + (num + 1).ToString();
        }
    }
}