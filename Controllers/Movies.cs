using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using fake_dotnetcore_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Newtonsoft.Json;

namespace fake_dotnetcore_api.Controllers
{
    [Route("fakeapi/[controller]")]
    public class Movies: Controller
    {
        private readonly ApiContext _context;
        public Movies(ApiContext context)
        {
             _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            {
                var users = await _context.Movies.ToArrayAsync();

            var response = users.Select(m => new
            {
                ID = m.ID,
                Title = m.Title,
                Description = m.Description,
                Year = m.Year,
                Rating = m.Rating
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult Get (int id)
        {
            var selectedMovie = _context.Movies.FirstOrDefault(m => m.ID == id);

            if (selectedMovie == null)
            {
                return NotFound();
            }

            return new ObjectResult(selectedMovie);
        }

        // POST api/values
        [HttpPost]

        public IActionResult Post([FromBody]Movie newMovie)
        {

            if (newMovie != null ){
                _context.Movies.Add(newMovie);
                _context.SaveChanges();
            }else {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){

            var result = from r in _context.Movies where r.ID == id select r;

            if (result.Count() > 0)
            {
                Movie movieToDelete = result.First();
                _context.Movies.Remove(movieToDelete);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        [AcceptVerbs("PUT")]
        public IActionResult Put([FromBody]Movie updatedMovie)
        {

            Console.WriteLine("Updating " + updatedMovie.Title);
            
            var result = from r in _context.Movies where r.ID == updatedMovie.ID select r;
            
            Console.WriteLine("Found " + result.First().Title);

            if (result.Count() > 0)
            {
                
                foreach (Movie movieToUpdate in result)
                {
                    movieToUpdate.Title = updatedMovie.Title;
                    movieToUpdate.Description= updatedMovie.Description;
                    movieToUpdate.Year = updatedMovie.Year;
                    movieToUpdate.Rating = updatedMovie.Rating;
                }
                try {
                    _context.SaveChanges();
                }catch (Exception ex){
                    Console.WriteLine("ERROR: {0}" + ex.ToString());
                }              

                return Ok();
            }else {
                Console.WriteLine("No result");
                return BadRequest();
            }
        }
    }
}
