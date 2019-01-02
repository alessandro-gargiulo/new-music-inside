namespace MusicInside.Batch.Importer.Infrastructure
{
    public class MusicFilesOptions
    {
        public string RootDirectory { get; set; }
        public string CoverSubFolder { get; set; }
        public string RegexSubFolder { get; set; }
        public string[] AvailableExtensions { get; set; }
    }
}
