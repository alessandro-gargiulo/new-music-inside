using MusicInside.DataAccessLayer.AssociationClasses;
using System.Collections.Generic;

namespace MusicInside.DataAccessLayer.Models
{
    public class Genre
    {
        #region Properties
        public int Id { get; set; }
        public string Description { get; set; }
        #endregion

        #region Navigation Properties
        public virtual IList<SongGenre> Songs { get; set; }
        #endregion
    }
}
