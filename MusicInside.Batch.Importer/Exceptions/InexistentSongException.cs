using System;

namespace MusicInside.Batch.Importer.Exceptions
{
    class InexistentSongException : Exception
    {
        public InexistentSongException(int songId) : base($"Unable to find the song with id={songId}") { }
    }
}
