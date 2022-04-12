namespace CapTwitch.Api.Controllers;

public class BadWordChecker : IBadWordChecker
{
    public bool IsBad(string word)
    {
        return false;
    }
}