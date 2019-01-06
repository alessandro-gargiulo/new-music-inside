using System;
using System.Collections.Generic;

namespace MusicInside.DataAccessLayer.Models
{
    public class Album
    {
        #region Properties
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
        #endregion

        #region Navigation Properties
        public virtual IList<Song> Songs { get; set; }

        public virtual CoverFile Cover { get; set; }
        public int? CoverId { get; set; }
        #endregion
    }
}
