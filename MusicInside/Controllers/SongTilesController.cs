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
    [Route(WebConstants.ROUTES.SONG_LIST_ROUTE)]
    public class SongTilesController : Controller
    {
        private MusicInsideDbContext _context;
        private readonly WebRepositoriesOptions _webOptions;

        public SongTilesController(MusicInsideDbContext context, IOptions<WebRepositoriesOptions> options)
        {
            _context = context;
            _webOptions = options.Value;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int limit = int.MaxValue, [FromQuery] int page = 1, [FromQuery] string title = "")
        {
            if(limit > 0 && page > 0)
            {
                // Ask for number of total songs
                int count = _context.Songs.Where(x => string.IsNullOrEmpty(title) || x.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) > 0).Count();
                // Retrieve page of songs
                IEnumerable<Song> songs = _context.Songs
                    .Where(x => string.IsNullOrEmpty(title) || x.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) > 0)
                    .OrderBy(s => s.Id)
                    .Skip((page - 1) * limit)
                    .Take(limit)
                    .ToList();

                // Instantiate temp list
                IList<SongTileEntity> parsedSongs = new List<SongTileEntity>();

                // Map into result entities
                foreach (var song in songs)
                {
                    SongTileEntity ste = new SongTileEntity
                    {
                        Id = song.Id,
                        Title = song.Title,
                        Artist = song.Artists.Where(x => x.IsPrincipalArtist.Value).FirstOrDefault().Artist.ArtName,
                        Album = song.Album.Title,
                        Genre = string.Join(", ", song.Genres.Select(x => x.Genre).ToList().Select(x => x.Description)),
                        CoverUrl = Path.Combine(_webOptions.Cover, song.Album.Cover.Path),
                        FileUrl = Path.Combine(_webOptions.File, song.Media.Path),
                        FileType = "audio/mpeg",
                        StatCount = song.Statistic.NumPlay,
                        StatWhen = song.Statistic.LastPlay.HasValue ? song.Statistic.LastPlay.Value.ToShortDateString() : "Never"
                    };
                    parsedSongs.Add(ste);
                }

                // Return Json Result
                return Json(new PagedSongTileEntity
                {
                    OverallCount = count,
                    Page = page,
                    PageSize = limit,
                    Songs = parsedSongs
                });
            }
            else
            {
                // Return status code 400
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Post([FromBody]SongTileEntity entity)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public IActionResult Put([FromBody]SongTileEntity entity)
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