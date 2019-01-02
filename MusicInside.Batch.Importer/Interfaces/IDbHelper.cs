using Microsoft.EntityFrameworkCore.Storage;
using TagLib;

namespace MusicInside.Batch.Importer.Interfaces
{
    interface IDbHelper
    {
        IDbContextTransaction BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

        bool ExistGenre(string genre);

        bool ExistArtist(string artName);
        bool ExistArtist(string artName, bool isBand);
        bool ExistArtist(string artName, string name, string surname);
        bool ExistArtist(string artName, string name, string surname, bool isBand);

        bool ExistSong(string title, string albumName, string artist);

        void InsertSong(Tag tag);
        void UpdateSong(Tag tag);
    }
}
