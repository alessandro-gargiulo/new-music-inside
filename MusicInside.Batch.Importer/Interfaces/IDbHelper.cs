using Microsoft.EntityFrameworkCore.Storage;
using TagLib;

namespace MusicInside.Batch.Importer.Interfaces
{
    interface IDbHelper
    {
        IDbContextTransaction BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
       
        int ExistAlbum(string title, string artistArtName);

        int CreateAlbum(Tag tag);
        int CreateEmptyStatistic();
        int CreateMediaFile(Tag tag, int albumId);

        int CreateSong(Tag tag, int statisticId, int albumId, int mediaFileId);

        int ExistArtist(string artName);
        int ExistArtist(string artName, bool isBand);
        int ExistArtistForAlbum(int albumId, string artName);
        int ExistArtistForAlbum(int albumId, string artName, bool isBand);

        int CreateArtist(string artName);
        void LinkArtist(int artistId, bool isPrincipalArtist, int songId);

        int CreateGenre(string genre);
        void LinkGenre(int genreId, int songId);
    }
}
