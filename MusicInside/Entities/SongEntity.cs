namespace MusicInside.Entities
{
    public class MinimalSongEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }

    public class SongEntity : MinimalSongEntity
    {
    }
}
