namespace MusicInside.DataAccessLayer.Models
{
    public class MediaFile
    {
        #region Properties
        public int Id { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        #endregion

        #region Navigation Properties
        public Song Song { get; set; }
        #endregion
    }
}
