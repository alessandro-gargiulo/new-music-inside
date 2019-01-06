using System.Collections.Generic;

namespace MusicInside.Entities
{
    public class SongTileEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public string CoverUrl { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
        public int StatCount { get; set; }
        public string StatWhen { get; set; }
    }

    public class PagedSongTileEntity
    {
        public int OverallCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<SongTileEntity> Songs { get; set; }
    }
}
