namespace CapTwitch.Services;

public interface IBadWordChecker
{
    bool IsBad(string word);
}