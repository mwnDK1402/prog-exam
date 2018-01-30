namespace Utility
{
    using System.Linq;
    using System.Text;

    public static class StringUtility
    {
        public static bool EndsWithNumber(this string str)
        {
            WalkThroughLastDigits(str, out StringBuilder numBuilder, out int i);
            return numBuilder.Length != 0;
        }

        public static string GetIncremented(this string str)
        {
            WalkThroughLastDigits(str, out StringBuilder numBuilder, out int i);

            if (numBuilder.Length == 0)
            {
                return str + "1";
            }

            var nonNumStr = str.Substring(0, i + 1);

            int num = int.Parse(
                new string(numBuilder.ToString().ToCharArray().Reverse().ToArray()));

            return nonNumStr + (num + 1).ToString();
        }

        private static void WalkThroughLastDigits(string str, out StringBuilder numBuilder, out int i)
        {
            numBuilder = new StringBuilder();
            for (i = str.Length - 1; i >= 0; --i)
            {
                if (!char.IsDigit(str[i]))
                {
                    break;
                }

                numBuilder.Append(str[i]);
            }
        }
    }
}