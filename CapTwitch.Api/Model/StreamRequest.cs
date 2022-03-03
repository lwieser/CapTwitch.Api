namespace CapTwitch.Api.Model;

public class StreamRequest : IStoredObject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}