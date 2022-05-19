namespace CapTwitch.Services;

public class BadWordChecker : IBadWordChecker
{
    public bool IsBad(string word)
    {
        return false;
    }
}