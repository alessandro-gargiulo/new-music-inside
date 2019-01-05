using System;

namespace MusicInside.Batch.Importer.Exceptions
{
    class InexistentAlbumException: Exception
    {
        public InexistentAlbumException(int albumId) : base($"Unable to find the album with id={albumId}") { }
    }
}
