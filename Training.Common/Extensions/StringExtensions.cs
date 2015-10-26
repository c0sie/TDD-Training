namespace Training.Common.Extensions
{
    public static class StringExtensions
    {
        public static int ToInt(this string value)
        {
            int result;

            return !int.TryParse(value, out result) ? result : result;
        }
    }
}
