using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MusicInside.Batch.Importer.Infrastructure;
using MusicInside.Batch.Importer.Interfaces;
using MusicInside.DataAccessLayer.Context;
using MusicInside.DataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;
using TagLib;

namespace MusicInside.Batch.Importer.Implementations
{
    public class DbHelper : IDbHelper
    {
        private readonly ILogger<DbHelper> _logger;
        private readonly MusicFilesOptions _options;
        private MusicInsideDbContext _context;

        #region Constructor
        public DbHelper(ILogger<DbHelper> log, IOptions<MusicFilesOptions> options, MusicInsideDbContext context)
        {
            _logger = log;
            _options = options.Value;
            _context = context;
        }
        #endregion

        #region Database Transaction Handlers
        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }
        #endregion

        #region Database Existing Entities Check
        public int ExistArtist(string artName)
        {
            _logger.LogInformation("ExistArtist | Attempt to search the existence of the artist with [artist={0}]", artName);
            int foundedId = -1;
            bool condition = _context.Artists.Any(x => x.ArtName.Equals(artName));
            if (condition)
                foundedId = _context.Artists.Where(x => x.ArtName.Equals(artName)).FirstOrDefault().Id;
            return foundedId;
        }

        public int ExistArtist(string artName, bool isBand)
        {
            _logger.LogInformation("ExistArtist | Attempt to search the existence of the artist with [artist={0}][isBand={1}]", artName, isBand);
            int foundedId = -1;
            bool condition = _context.Artists.Any(x => x.ArtName.Equals(artName) && x.IsBand == isBand);
            if (condition)
                foundedId = _context.Artists.Where(x => x.ArtName.Equals(artName) && x.IsBand == isBand).FirstOrDefault().Id;
            return foundedId;
        }

        public int ExistAlbum(string title, string artistArtName)
        {
            _logger.LogInformation("ExistAlbum | Attempt to search the existence of the album with [title={0}][artist={2}]", title, artistArtName);
            int foundedId = -1;
            // Retrieve all candidates
            List<Album> candidates = _context.Albums.Where(x => x.Title.Equals(title)).ToList();
            _logger.LogDebug("ExistAlbum | Found {0} candidates", candidates.Count);
            if (candidates.Count == 0) return foundedId;
            bool condition = candidates.Any(c => c.Songs.Any(s => s.Artists.Any(a => a.IsPrincipalArtist && a.Artist.ArtName.Equals(artistArtName))));
            _logger.LogDebug("ExistAlbum | Album found? {0}", condition);
            if(condition)
                foundedId = candidates.Where(c => c.Songs.Any(s => s.Artists.Any(a => a.IsPrincipalArtist && a.Artist.ArtName.Equals(artistArtName)))).FirstOrDefault().Id;
            return foundedId;
        }

        public int ExistArtistForAlbum(int albumId, string artName)
        {
            throw new System.NotImplementedException();
        }

        public int ExistArtistForAlbum(int albumId, string artName, bool isBand)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Create Entry In Database
        public int CreateAlbum(Tag tag)
        {
            throw new System.NotImplementedException();
        }

        public int CreateEmptyStatistic()
        {
            throw new System.NotImplementedException();
        }

        public int CreateMediaFile(Tag tag, int albumId)
        {
            throw new System.NotImplementedException();
        }

        public int CreateSong(Tag tag, int statisticId, int albumId, int mediaFileId)
        {
            throw new System.NotImplementedException();
        }

        public int CreateArtist(string artName)
        {
            throw new System.NotImplementedException();
        }

        public int CreateGenre(string genre)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Link Entities
        public void LinkArtist(int artistId, bool isPrincipalArtist, int songId)
        {
            throw new System.NotImplementedException();
        }

        public void LinkGenre(int genreId, int songId)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
