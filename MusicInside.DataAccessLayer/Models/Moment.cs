using MusicInside.DataAccessLayer.AssociationClasses;
using System.Collections.Generic;

namespace MusicInside.DataAccessLayer.Models
{
    public class Moment
    {
        #region Properties
        public int Id { get; set; }
        public string Description { get; set; }
        #endregion

        #region Navigation Properties
        public IList<SongMoment> Songs { get; set; }
        #endregion
    }
}
