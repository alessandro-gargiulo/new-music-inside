﻿using MusicInside.DataAccessLayer.AssociationClasses;
using System;
using System.Collections.Generic;

namespace MusicInside.DataAccessLayer.Models
{
    public class Song
    {
        #region Properties
        public int Id { get; set; }
        public string Title { get; set; }
        public int TrackNo { get; set; }
        public int? Year { get; set; }
        public DateTime CreatedOn { get; set; }
        #endregion

        #region Navigation Properties
        public virtual Statistic Statistic { get; set; }
        public int? StatisticId { get; set; }
        public virtual Album Album { get; set; }
        public int? AlbumId { get; set; }
        public virtual MediaFile Media { get; set; }
        public int? MediaId { get; set; }

        public virtual IList<SongGenre> Genres { get; set; }
        public virtual IList<SongArtist> Artists { get; set; }
        public virtual IList<SongMoment> Moments { get; set; }
        #endregion
    }
}
