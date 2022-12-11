namespace Utility.Validation.General
{

    public static class DtoValidationTools
    {
        public static bool StringSpaceAndNullChecking(this string str)
        {
            return String.IsNullOrWhiteSpace(str) ||
                   str.Any(x => Char.IsWhiteSpace(x));
        }

        public static bool ListStringSpaceAndNullChecking(this List<string> strList)
        {
            return strList.Any(i =>
                String.IsNullOrWhiteSpace(i) ||
                i.Any(x => Char.IsWhiteSpace(x))
            );
        }

        public static bool CheckingPresenceNumberInString(this string str)
        {
            return str.Any(x => int.TryParse(x.ToString(), out _));
        }

        public static bool CheckingPresenceNumberInListString(this List<string> strList)
        {
            return strList.Any(i =>
            i.Any(x => int.TryParse(x.ToString(), out _))
            );
        }
    }
}
