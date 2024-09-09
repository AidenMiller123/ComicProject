using ComicMarvelProject.Models.Shared;

namespace ComicMarvelProject.Models
{
    public class StoryDataWrapper
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Copyright { get; set; }
        public string AttributionText { get; set; }
        public string AttributionHTML { get; set; }
        public StoryDataContainer Data { get; set; }
        public string Etag { get; set; }
    }

    public class StoryDataContainer
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public List<Story> Results { get; set; }
    }

    public class Story
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ResourceURI { get; set; }
        public string Type { get; set; }
        public DateTime? Modified { get; set; }
        public Image Thumbnail { get; set; }
        public ComicList Comics { get; set; }
        public SeriesList Series { get; set; }
        public EventList Events { get; set; }
        public CharacterList Characters { get; set; }
        public CreatorList Creators { get; set; }
        public ComicSummary OriginalIssue { get; set; }
    }
    public class StoryList
    {
        public int Avaliable { get; set; }
        public int Returned { get; set; }
        public string CollectionUrl { get; set; }
        public List<StorySummary> Items { get; set; }
    }

    public class StorySummary
    {
        public string ResourceUrl { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
