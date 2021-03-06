﻿using MusicInside.DataAccessLayer.Models;

namespace MusicInside.DataAccessLayer.AssociationClasses
{
    public class SongGenre
    {
        #region Properties
        public int Id { get; set; }
        #endregion

        #region Navigation Properties
        public virtual Song Song { get; set; }
        public int SongId { get; set; }

        public virtual Genre Genre { get; set; }
        public int GenreId { get; set; }
        #endregion
    }
}
