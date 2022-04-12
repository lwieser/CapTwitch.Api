namespace CapTwitch.Api.Services
{
    public class PasswordValdator
    {
        public static bool IsValid(string str)
        {
            str ??= String.Empty;
            str = str.Replace(" ", "");
            if (String.IsNullOrEmpty(str) || str.Length < 8)
            {
                return false;
            }

            return str.Any(c => !Char.IsLetterOrDigit(c)) && str.Any(c => int.TryParse(c.ToString(), out var _));
        }
    }
}
