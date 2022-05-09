using CapTwitch.Model.Interfaces;

namespace CapTwitch.Model.Model;

public class StreamRequest : IStoredObject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime? ValidatedAt { get; set; }
}