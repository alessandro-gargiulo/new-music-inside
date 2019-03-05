using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MusicInside.DataAccessLayer.Context;
using MusicInside.DataAccessLayer.Models;
using MusicInside.Entities;
using MusicInside.Infrastracture;
using MusicInside.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicInside.Controllers
{
    [Route(WebConstants.ROUTES.ARTIST_LIST_ROUTE)]
    public class ArtistTilesController : Controller
    {
        private MusicInsideDbContext _context;
        private readonly WebRepositoriesOptions _webOptions;

        public ArtistTilesController(MusicInsideDbContext context, IOptions<WebRepositoriesOptions> options)
        {
            _context = context;
            _webOptions = options.Value;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int limit = int.MaxValue, [FromQuery] int page = 1, [FromQuery] string name = "")
        {
            if (limit > 0 && page > 0)
            {
                // Ask or number of total artist
                int count = _context.Artists.Where(x => string.IsNullOrEmpty(name) || x.ArtName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0).Count();
                // Retrieve page of artist
                IEnumerable<Artist> artists = _context.Artists
                    .Where(x => string.IsNullOrEmpty(name) || x.ArtName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                    .OrderBy(s => s.Id)
                    .Skip((page - 1) * limit)
                    .Take(limit)
                    .ToList();

                // Instantiate temp list
                IList<ArtistTileEntity> parsedArtist = new List<ArtistTileEntity>();

                // Map into result entitis
                foreach (var artist in artists)
                {
                    ArtistTileEntity ate = new ArtistTileEntity
                    {
                        Id = artist.Id,
                        ArtName = artist.ArtName,
                        NumSongs = artist.Songs.Count()
                    };
                    parsedArtist.Add(ate);
                }

                // Return Json Result
                return Json(new PagedArtistTileEntity
                {
                    OverallCount = count,
                    Page = page,
                    PageSize = limit,
                    Artists = parsedArtist
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
        public IActionResult Post([FromBody]ArtistTileEntity entity)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public IActionResult Put([FromBody]ArtistTileEntity entity)
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