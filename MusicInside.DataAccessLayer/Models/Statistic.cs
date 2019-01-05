using System;

namespace MusicInside.DataAccessLayer.Models
{
    public class Statistic
    {
        #region Properties
        public int Id { get; set; }
        public int NumPlay { get; set; }
        public DateTime? LastPlay { get; set; }
        #endregion

        #region Navigation Properties
        public Song Song { get; set; }
        #endregion
    }
}
