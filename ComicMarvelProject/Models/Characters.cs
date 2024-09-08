namespace ComicMarvelProject.Models
{
    public class CharactersDataWrapper
    {
        public int Code { get; set; }
        public string Status { get; set; }

        public string CopyRight { get; set; }

        public string Attributiontext { get; set; }
        public string AttributionHTML { get; set; }
        public CharacterDataContainer Data { get; set; }
        public string Etag { get; set; }
    }

    public class CharacterDataContainer
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public List<Character> Results { get; set; }
    }

    public class Character
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime modifed { get; set; }
        public string resourceURI { get; set; }
        public List<Url> urls { get; set; }
        public Image Thumbnail { get; set; }
        public ComicList Comics { get; set; }
        public StoryList Stories { get; set; }
        public EventList Events { get; set; }
        public SeriesList Series { get; set; }
    }

    public class Url
    {
        public string Type { get; set; }
        public string url { get; set; }
    }

    public class Image
    {
        public string Path { get; set; }
        public string Extension { get; set; }
    }

    public class ComicList
    {
        public int Avaliable { get; set; }
        public int Returned { get; set; }
        public string CollectionUrl { get; set; }
        public ComicSummary Items { get; set; }
    }

    public class ComicSummary
    {
        public string ResourceUrl { get; set; }
        public string Name { get; set; }
    }

    public class StoryList
    {
        public int Avaliable { get; set; }
        public int Returned { get; set; }
        public string CollectionUrl { get; set; }
        public StorySummary Items { get; set; }
    }

    public class StorySummary
    {
        public string ResourceUrl { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class EventList
    {
        public int Avaliable { get; set; }
        public int Returned { get; set; }
        public string CollectionUrl { get; set; }
        public EventSummary Items { get; set; }
    }

    public class EventSummary
    {
        public string ResourceUrl { get; set; }
        public string Name { get; set; }
    }
    public class SeriesList
    {
        public int Avaliable { get; set; }
        public int Returned { get; set; }
        public string CollectionUrl { get; set; }
        public SeriesSummary Items { get; set; }
    }

    public class SeriesSummary
    {
        public string ResourceUrl { get; set; }
        public string Name { get; set; }
    }
}
