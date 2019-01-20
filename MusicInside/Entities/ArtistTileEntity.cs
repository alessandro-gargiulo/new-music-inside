using System.Collections.Generic;

namespace MusicInside.Entities
{
    public class ArtistTileEntity
    {
        public int Id { get; set; }
        public string ArtName { get; set; }
        public int NumSongs { get; set; }
    }

    public class PagedArtistTileEntity : PagedEntity
    {
        public IEnumerable<ArtistTileEntity> Artists { get; set; }
    }
}
