using System.Collections.Generic;

namespace MusicInside.Entities
{
    public class AlbumTileEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public int NumSongs { get; set; }
        public string CoverUrl { get; set; }
    }

    public class PagedAlbumTileEntity : PagedEntity
    {
        public IEnumerable<AlbumTileEntity> Albums { get; set; }
    }
}
