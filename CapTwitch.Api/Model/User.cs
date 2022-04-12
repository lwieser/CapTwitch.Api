namespace CapTwitch.Api.Model;

public class User : IStoredObject
{
    public int Id { get; set; }
    public string Pseudo { get; set; }
}