using MusicInside.DataAccessLayer.Models;

namespace MusicInside.DataAccessLayer.AssociationClasses
{
    public class SongMoment
    {
        public Moment Moment { get; set; }
        public int MomentId { get; set; }

        public Song Song { get; set; }
        public int SongId { get; set; }
    }
}
