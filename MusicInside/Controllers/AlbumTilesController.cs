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
    [Route(WebConstants.ROUTES.ALBUM_LIST_ROUTE)]
    public class AlbumTilesController : Controller
    {
        private MusicInsideDbContext _context;
        private readonly WebRepositoriesOptions _webOptions;

        public AlbumTilesController(MusicInsideDbContext context, IOptions<WebRepositoriesOptions> options)
        {
            _context = context;
            _webOptions = options.Value;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int limit = int.MaxValue, [FromQuery] int page = 1, [FromQuery] string title = "")
        {
            if(limit > 0 && page > 0)
            {
                // Ask for number of total albums
                int count = _context.Albums.Where(x => string.IsNullOrEmpty(title) || x.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) >= 0).Count();
                // Retrieve page of albums
                IEnumerable<Album> albums = _context.Albums
                    .Where(x => string.IsNullOrEmpty(title) || x.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) >= 0)
                    .OrderBy(s => s.Id)
                    .Skip((page - 1) * limit)
                    .Take(limit)
                    .ToList();

                // Instantiate temp list
                IList<AlbumTileEntity> parsedAlbums = new List<AlbumTileEntity>();

                // Map into result entitis
                foreach(var album in albums)
                {
                    AlbumTileEntity ate = new AlbumTileEntity
                    {
                        Id = album.Id,
                        Title = album.Title,
                        CoverUrl = Path.Combine(_webOptions.Cover, album.Cover.Path),
                        NumSongs = album.Songs.Count(),
                        Artist = album.Songs.FirstOrDefault().Artists.FirstOrDefault(x => x.IsPrincipalArtist == true).Artist.ArtName
                    };
                    parsedAlbums.Add(ate);
                }

                // Return Json Result
                return Json(new PagedAlbumTileEntity
                {
                    OverallCount = count,
                    Page = page,
                    PageSize = limit,
                    Albums = parsedAlbums
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
        public IActionResult Post([FromBody]AlbumTileEntity entity)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public IActionResult Put([FromBody]AlbumTileEntity entity)
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