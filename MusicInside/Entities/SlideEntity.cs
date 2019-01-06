using MusicInside.DataAccessLayer.Models;
using System.Collections.Generic;
using System.IO;

namespace MusicInside.Entities
{
    public class SlideEntity
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public string Alt { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
    }

    public class PagedSlideEntity
    {
        public int OverallCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<SlideEntity> Slides { get; set; }
    }

    public static class Extension
    {
        public static IEnumerable<SlideEntity> MapToEntityList(this IEnumerable<Slide> source, string sourceRoot)
        {
            // Instantiate temp list
            IList<SlideEntity> parsedSlides = new List<SlideEntity>();

            // Map entities
            foreach (Slide slide in source)
            {
                parsedSlides.Add(slide.MapToEntity(sourceRoot));
            }

            return parsedSlides;
        }

        public static SlideEntity MapToEntity(this Slide source, string sourceRoot)
        {
            return new SlideEntity
            {
                Id = source.Id,
                Alt = source.AltText,
                Header = source.Header,
                Source = Path.Combine(sourceRoot, source.Source),
                Text = source.Text
            };
        }
    }
}
