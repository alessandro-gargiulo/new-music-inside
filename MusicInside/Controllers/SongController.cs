using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MusicInside.DataAccessLayer.Context;
using MusicInside.DataAccessLayer.Models;
using MusicInside.Entities;
using MusicInside.Infrastracture;
using MusicInside.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicInside.Controllers
{
    [Route(WebConstants.ROUTES.SONG_ROUTE)]
    public class SongController : Controller
    {
        private MusicInsideDbContext _context;
        private readonly WebRepositoriesOptions _webOptions;

        public SongController(MusicInsideDbContext context, IOptions<WebRepositoriesOptions> options)
        {
            _context = context;
            _webOptions = options.Value;
        }

        [HttpGet("fromAlbum")]
        public IActionResult GetSongsFromAlbum([FromQuery] int id = 0)
        {
            if(id > 0)
            {
                // Ask for Album
                Album album = _context.Albums.FirstOrDefault(x => x.Id == id);
                if(album != null)
                {
                    IList<SongEntity> songs = new List<SongEntity>();

                    foreach(Song song in album.Songs)
                    {
                        songs.Add(new SongEntity
                        {
                            Id = song.Id,
                            Title = song.Title,
                            Artist = song.Artists.FirstOrDefault(x => x.IsPrincipalArtist.Value).Artist.ArtName,
                            CoverUrl = Path.Combine(_webOptions.Cover, song.Album.Cover.Path),
                            FileType = "audio/mpeg",
                            FileUrl = Path.Combine(_webOptions.File, song.Media.Path)
                        });
                    }

                    return Json(songs);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("fromArtist")]
        public IActionResult GetSongsFromArtist([FromQuery] int id = 0)
        {
            if (id > 0)
            {
                // Ask for Artist
                Artist artist = _context.Artists.FirstOrDefault(x => x.Id == id);
                if (artist != null)
                {
                    IList<SongEntity> songs = new List<SongEntity>();

                    // Retrieve all song
                    var allSongs = artist.Songs.Select(x => x.Song).ToList();

                    foreach (Song song in allSongs)
                    {
                        songs.Add(new SongEntity
                        {
                            Id = song.Id,
                            Title = song.Title,
                            Artist = song.Artists.FirstOrDefault(x => x.IsPrincipalArtist.Value).Artist.ArtName,
                            CoverUrl = Path.Combine(_webOptions.Cover, song.Album.Cover.Path),
                            FileType = "audio/mpeg",
                            FileUrl = Path.Combine(_webOptions.File, song.Media.Path)
                        });
                    }

                    return Json(songs);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Post([FromBody]SongEntity entity)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public IActionResult Put([FromBody]SongEntity entity)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}