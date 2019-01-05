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
        public Song Song { get; set; }
        public int SongId { get; set; }

        public Artist Artist { get; set; }
        public int ArtistId { get; set; }
        #endregion
    }
}
