﻿using ComicMarvelProject.Models.Shared;

namespace ComicMarvelProject.Models
{
    public class EventDataWrapper
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Copyright { get; set; }
        public string AttributionText { get; set; }
        public string AttributionHTML { get; set; }
        public EventDataContainer Data { get; set; }
        public string Etag { get; set; }
    }

    public class EventDataContainer
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public List<Event> Results { get; set; }
    }

    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ResourceURI { get; set; }
        public List<Url> Urls { get; set; }
        public string? Modified { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public Image Thumbnail { get; set; }
        public ComicList Comics { get; set; }
        public StoryList Stories { get; set; }
        public SeriesList Series { get; set; }
        public CharacterList Characters { get; set; }
        public CreatorList Creators { get; set; }
        public EventSummary Next { get; set; }
        public EventSummary Previous { get; set; }
    }

    public class EventList
    {
        public int Available { get; set; }
        public int Returned { get; set; }
        public string CollectionURI { get; set; }
        public List<EventSummary> Items { get; set; }
    }

    public class EventSummary
    {
        public string ResourceURI { get; set; }
        public string Name { get; set; }
    }
}
