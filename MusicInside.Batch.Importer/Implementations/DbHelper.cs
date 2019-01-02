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
        public bool ExistArtist(string artName)
        {
            return _context.Artists.Any(x => x.ArtName.Equals(artName));
        }

        public bool ExistArtist(string artName, bool isBand)
        {
            return _context.Artists.Any(x => x.ArtName.Equals(artName) && x.IsBand == isBand);
        }

        public bool ExistArtist(string artName, string name, string surname)
        {
            return _context.Artists.Any(x => x.ArtName.Equals(artName) && x.Name.Equals(name) && x.Surname.Equals(surname));
        }

        public bool ExistArtist(string artName, string name, string surname, bool isBand)
        {
            return _context.Artists.Any(x => x.ArtName.Equals(artName) && x.Name.Equals(name) && x.Surname.Equals(surname) && x.IsBand == isBand);
        }

        public bool ExistGenre(string genre)
        {
            return _context.Genres.Any(x => x.Description.Equals(genre));
        }

        public bool ExistSong(string title, string albumName, string artistName)
        {
            _logger.LogDebug("ExistSong | Attempt to search the existence of the song with [title={0}][album={1}][artist={2}]", title, albumName, artistName);
            // Retrieve all candidates
            List<Song> candidates = _context.Songs.Where(x => x.Title.Equals(title)).ToList();
            if (candidates.Count == 0) return false;

            // To proceed with the tests, at least one candidate must have an album with the specified name
            if (!candidates.Any(x => x.Album.Title.Equals(albumName))) return false;
            // Filter list
            candidates.RemoveAll(x => !x.Album.Title.Equals(albumName));

            // To proceed with the test, at least one candidate must have the specified artist
            if (!candidates.Any(x => x.Artists.Any(y => y.IsPrincipalArtist && y.Artist.Name.Equals(artistName)))) return false;

            // At this point some candidates survive to filtering and the song exist
            return true;
        }

        public void InsertSong(Tag tag)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateSong(Tag tag)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
