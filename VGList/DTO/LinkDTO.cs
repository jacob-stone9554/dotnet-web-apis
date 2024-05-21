namespace VGList.DTO
{
    public class LinkDTO
    {
        //necessary fields for the Link DTO
        public string Href { get; private set; }
        public string Rel { get; private set; }
        public string Type { get; private set; }

        //constructor. Recieves href, rel, and type.
        public LinkDTO(string href, string rel, string type)
        {
            Href = href;
            Rel = rel;
            Type = type;
        }
    }
}
