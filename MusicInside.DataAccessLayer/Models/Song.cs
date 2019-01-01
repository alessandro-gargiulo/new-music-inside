using MusicInside.DataAccessLayer.AssociationClasses;
using System.Collections.Generic;

namespace MusicInside.DataAccessLayer.Models
{
    public class Song
    {
        #region Properties
        public int Id { get; set; }
        public string Title { get; set; }
        public int TrackNo { get; set; }
        public int Year { get; set; }
        #endregion

        #region Navigation Properties
        public Statistic Statistic { get; set; }
        public int StatisticId { get; set; }
        public Album Album { get; set; }
        public int AlbumId { get; set; }
        public MediaFile Media { get; set; }
        public int MediaId { get; set; }

        public IList<SongGenre> Genres { get; set; }
        public IList<SongArtist> Artists { get; set; }
        public IList<SongMoment> Moments { get; set; }
        #endregion
    }
}
