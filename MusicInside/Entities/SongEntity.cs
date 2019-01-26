namespace MusicInside.Entities
{
    public class MinimalSongEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FileUrl { get; set; }
    }

    public class SongEntity : MinimalSongEntity
    {
        public string Artist { get; set; }
        public string CoverUrl { get; set; }
        public string FileType { get; set; }
    }
}
