namespace CapTwitch.Api.Controllers;

public interface IBadWordChecker
{
    bool IsBad(string word);
}