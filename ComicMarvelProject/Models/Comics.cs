﻿using ComicMarvelProject.Models.Shared;

namespace ComicMarvelProject.Models
{
    public class ComicDataWrapper
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Copyright { get; set; }
        public string AttributionText { get; set; }
        public string AttributionHTML { get; set; }
        public ComicDataContainer Data { get; set; }
        public string Etag { get; set; }
    }

    public class ComicDataContainer
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public List<Comic> Results { get; set; }
    }

    public class Comic
    {
        public int Id { get; set; }
        public int DigitalId { get; set; }
        public string Title { get; set; }
        public double IssueNumber { get; set; }
        public string VariantDescription { get; set; }
        public string Description { get; set; }
        public string? Modified { get; set; }
        public string Isbn { get; set; }
        public string Upc { get; set; }
        public string DiamondCode { get; set; }
        public string Ean { get; set; }
        public string Issn { get; set; }
        public string Format { get; set; }
        public int PageCount { get; set; }
        public List<TextObject> TextObjects { get; set; }
        public string ResourceURI { get; set; }
        public List<Url> Urls { get; set; }
        public SeriesSummary Series { get; set; }
        public List<ComicSummary> Variants { get; set; }
        public List<ComicSummary> Collections { get; set; }
        public List<ComicSummary> CollectedIssues { get; set; }
        public List<ComicDate> Dates { get; set; }
        public List<ComicPrice> Prices { get; set; }
        public Image Thumbnail { get; set; }
        public List<Image> Images { get; set; }
        public CreatorList Creators { get; set; }
        public CharacterList Characters { get; set; }
        public StoryList Stories { get; set; }
        public EventList Events { get; set; }
    }

    public class ComicDate
    {
        public string Type { get; set; }
        public string? Date { get; set; }
    }

    public class ComicPrice
    {
        public string Type { get; set; }
        public float Price { get; set; }
    }
    public class ComicList
    {
        public int Available { get; set; }
        public int Returned { get; set; }
        public string CollectionURI { get; set; }
        public List<ComicSummary> Items { get; set; }
    }

    public class ComicSummary
    {
        public string ResourceURI { get; set; }
        public string Name { get; set; }
    }
}
