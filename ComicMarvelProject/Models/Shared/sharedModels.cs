namespace ComicMarvelProject.Models.Shared
{
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
    public class TextObject
    {
        public string Type { get; set; }
        public string Language { get; set; }
        public string Text { get; set; }
    }
}
