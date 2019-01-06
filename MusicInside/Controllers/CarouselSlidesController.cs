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
    [Route(WebConstants.ROUTES.SLIDE_LIST_ROUTE)]
    public class CarouselSlidesController : Controller
    {
        private MusicInsideDbContext _context;
        private WebRepositoriesOptions _webOptions;

        public CarouselSlidesController(MusicInsideDbContext context, IOptions<WebRepositoriesOptions> options)
        {
            _context = context;
            _webOptions = options.Value;
        }

        [HttpGet("active")]
        public IActionResult GetActive()
        {
            IEnumerable<Slide> slides = _context.Slides.Where(x => x.ValidityFrom <= DateTime.Now && x.ValidityTo >= DateTime.Now).ToList();
            return Json(slides.MapToEntityList(_webOptions.Slide));
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int limit = int.MaxValue, [FromQuery] int page = 1)
        {
            if (limit > 0 && page > 0)
            {
                // Ask for number of total slides
                int count = _context.Slides.Count();
                // Retrieve page of slides
                IEnumerable<Slide> slides = _context.Slides
                    .OrderBy(s => s.Id)
                    .Skip((page - 1) * limit)
                    .Take(limit)
                    .ToList();

                // Return result
                return Json(new PagedSlideEntity
                {
                    OverallCount = count,
                    Page = page,
                    PageSize = limit,
                    Slides = slides.MapToEntityList(_webOptions.Slide)
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
        public IActionResult Post([FromBody]SlideEntity entity)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public IActionResult Put([FromBody]SlideEntity entity)
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