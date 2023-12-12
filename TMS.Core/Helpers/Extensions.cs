namespace TMS.Core;

public static class Extensions
{
    public static string Remove(this string s, char[] chars)
        => string.Concat(s.Split(chars));

    public static string Clean(this DateTime d)
        => d.ToString().Remove([.. Path.GetInvalidFileNameChars(), ' ']);
}