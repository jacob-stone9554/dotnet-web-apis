namespace VGList.Models
{
    public class VideoGame
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Year { get; set; }
        public int? MinPlayers { get; set; }
        public int? MaxPlayers { get; set;}
    }
}
