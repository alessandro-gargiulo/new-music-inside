using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MusicInside.Batch.Importer.Exceptions;
using MusicInside.Batch.Importer.Infrastructure;
using MusicInside.Batch.Importer.Interfaces;
using MusicInside.DataAccessLayer.Context;
using MusicInside.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            _logger.LogInformation("ExistAlbum | Attempt to search the existence of the album with [title={0}][artist={1}]", title, artistArtName);
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
            _logger.LogInformation("ExistArtistForAlbum | Attempt to search the existence of the album with [albumId={0}][artist={1}]", albumId, artName);
            int foundedId = -1;
            // Retrieve single candidate
            Album album = _context.Albums.Where(x => x.Id == albumId).FirstOrDefault();
            if(album != null)
            {
                bool condition = album.Songs.Any(s => s.Artists.Any(a => a.IsPrincipalArtist && a.Artist.ArtName.Equals(artName)));
                _logger.LogDebug("ExistArtistForAlbum | Artist found? {0}", condition);
                if (condition)
                {
                    var candidateSong = album.Songs.Where(s => s.Artists.Any(a => a.IsPrincipalArtist && a.Artist.ArtName.Equals(artName))).FirstOrDefault();
                    foundedId = candidateSong.Artists.FirstOrDefault(x => x.IsPrincipalArtist && x.Artist.ArtName.Equals(artName)).Id;
                }
                return foundedId;
            }
            else
            {
                throw new InexistentAlbumException(albumId);
            }
        }

        public int ExistArtistForAlbum(int albumId, string artName, bool isBand)
        {
            _logger.LogInformation("ExistArtistForAlbum | Attempt to search the existence of the album with [albumId={0}][artist={1}][isBand={2}", albumId, artName, isBand);
            int foundedId = -1;
            // Retrieve single candidate
            Album album = _context.Albums.Where(x => x.Id == albumId).FirstOrDefault();
            if (album != null)
            {
                bool condition = album.Songs.Any(s => s.Artists.Any(a => a.IsPrincipalArtist && a.Artist.ArtName.Equals(artName) && a.Artist.IsBand == isBand));
                _logger.LogDebug("ExistArtistForAlbum | Artist found? {0}", condition);
                if (condition)
                {
                    var candidateSong = album.Songs.Where(s => s.Artists.Any(a => a.IsPrincipalArtist && a.Artist.ArtName.Equals(artName) && a.Artist.IsBand == isBand)).FirstOrDefault();
                    foundedId = candidateSong.Artists.FirstOrDefault(x => x.IsPrincipalArtist && x.Artist.ArtName.Equals(artName) && x.Artist.IsBand == isBand).Id;
                }
                return foundedId;
            }
            else
            {
                throw new InexistentAlbumException(albumId);
            }
        }
        #endregion

        #region Create Entry In Database
        public int CreateAlbum(Tag tag)
        {
            _logger.LogInformation("CreateAlbum | Attempt to create an album for tag [album={0}][artist={1}]", tag.Album, tag.FirstAlbumArtist);
            // Create an album in-memory object
            Album dbAlbum = new Album
            {
                Title = tag.Album,
                CreatedOn = DateTime.Now
            };
            _context.Albums.Add(dbAlbum);
            _context.SaveChanges();
            // Keep cover file
            if(tag.Pictures[0] != null)
            {
                // Insert a blank cover file
                CoverFile dbCoverFile = new CoverFile
                {
                    Album = dbAlbum,
                    FileName = string.Empty,
                    Path = string.Empty,
                    Extension = string.Empty
                };
                _context.Covers.Add(dbCoverFile);
                _context.SaveChanges();
                try
                {
                    string newCoverFileName = $"{dbCoverFile.Id}_{dbAlbum.Id}";
                    // Check if file does not exist
                    if (!System.IO.File.Exists(Path.Combine(_options.RootDirectory, _options.CoverSubFolder, newCoverFileName)))
                    {
                        // File does not exist on file system
                        MemoryStream ms = new MemoryStream(tag.Pictures[0].Data.Data);
                        using (FileStream fs = new FileStream(Path.Combine(_options.RootDirectory, _options.CoverSubFolder, newCoverFileName) + ".png", FileMode.Create, FileAccess.Write))
                        {
                            byte[] bytes = new byte[ms.Length];
                            ms.Read(bytes, 0, (int)ms.Length);
                            fs.Write(bytes, 0, bytes.Length);
                            ms.Dispose();
                        }
                        _logger.LogInformation("CreateAlbum | Created new file on file system under directory {0}\\{1} using name {2}.png", _options.RootDirectory, _options.CoverSubFolder, newCoverFileName);
                        dbCoverFile.FileName = newCoverFileName;
                        dbCoverFile.Extension = "png";
                        dbCoverFile.Path = Path.Combine(_options.CoverSubFolder, newCoverFileName);
                        _context.Covers.Update(dbCoverFile);
                        _context.SaveChanges();
                        _logger.LogInformation("CreateAlbum | Field 'FileName' of fileId={0} was correctly updated using value={1}", dbCoverFile.Id, newCoverFileName);
                    }
                    else
                    {
                        _logger.LogWarning("CreateAlbum | File cover with name {0} already exist!", newCoverFileName);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("CreateAlbum | Something goes wrong while exporting cover of album={0} due to exception: {1}", dbAlbum.Title, ex.Message);
                }
            }
            else
            {
                _logger.LogWarning("CreateAlbum | Album with title {0} seems not to have a cover file!", dbAlbum.Title);
            }
            return dbAlbum.Id;
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
