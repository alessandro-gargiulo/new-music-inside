using MusicInside.DataAccessLayer.Models;

namespace MusicInside.DataAccessLayer.AssociationClasses
{
    public class SongArtist
    {
        #region Properties
        public int Id { get; set; }
        public bool? IsPrincipalArtist { get; set; }
        #endregion

        #region Navigation Properties
        public virtual Song Song { get; set; }
        public int SongId { get; set; }

        public virtual Artist Artist { get; set; }
        public int ArtistId { get; set; }
        #endregion
    }
}
