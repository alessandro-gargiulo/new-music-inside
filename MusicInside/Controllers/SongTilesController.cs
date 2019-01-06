using Microsoft.AspNetCore.Mvc;
using MusicInside.DataAccessLayer.Context;
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
        public IActionResult Get([FromQuery] int limit, [FromQuery] int page)
        {
            if(limit > 0 && page > 0)
            {
                DataAccessLayer.Models.Song s = _context.Songs.Where(x => x.Id == 200).FirstOrDefault();
                return BadRequest();
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