using CapTwitch.Model.Interfaces;

namespace CapTwitch.Model.Model
{
    public class StreamEvent : IStoredObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Background { get; set; }
        public string Tecs { get; set; }
    }
}
