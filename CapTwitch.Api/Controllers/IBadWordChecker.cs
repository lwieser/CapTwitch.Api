namespace CapTwitch.Api.Controllers;

public interface IBadWordChecker
{
    bool IsBad(string word);
}

public class BadWordChecker : IBadWordChecker
{
    public bool IsBad(string word)
    {
        throw new NotImplementedException();
    }
}