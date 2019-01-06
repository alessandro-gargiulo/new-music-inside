using Microsoft.AspNetCore.Mvc;
using MusicInside.DataAccessLayer.Context;
using MusicInside.DataAccessLayer.Models;
using MusicInside.Entities;
using MusicInside.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicInside.Controllers
{
    [Route(WebConstants.ROUTES.SONG_LIST_ROUTE)]
    public class SongTilesController : Controller
    {
        private MusicInsideDbContext _context;

        public SongTilesController(MusicInsideDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int limit = int.MaxValue, [FromQuery] int page = 1)
        {
            if(limit > 0 && page > 0)
            {
                // Ask for numbero of total songs
                int count = _context.Songs.Count();
                // Retrieve page of songs
                IEnumerable<Song> songs = _context.Songs
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
                        CoverUrl = song.Album.Cover.Path,
                        FileUrl = song.Media.Path,
                        FileType = "audio/mpeg",
                        StatCount = song.Statistic.NumPlay,
                        StatWhen = song.Statistic.LastPlay.HasValue ? song.Statistic.LastPlay.Value.ToShortDateString() : "Never"
                    };
                    parsedSongs.Add(ste);
                }

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