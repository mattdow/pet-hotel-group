using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;
using RegexExamples;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetOwnersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetOwnersController(ApplicationContext context)
        {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        [HttpGet]
        public IEnumerable<PetOwner> GetAll()
        {
            return _context.PetOwners;
        }

        [HttpGet("{id}")]

        public ActionResult<PetOwner> GetById(int id)
        {
            PetOwner petOwner = _context.PetOwners.SingleOrDefault(petOwner => petOwner.id == id);
            if (petOwner == null)
            {
                return NotFound();
            }
            return petOwner;
        }

        [HttpPost]

        public IActionResult Post(PetOwner petOwner)
        {
            if(
            !RegexUtilities.IsValidEmail(petOwner.emailAddress)
            ) return BadRequest();
            
            _context.Add(petOwner);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Post), new {id = petOwner.id}, petOwner);
            
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            PetOwner petOwner = _context.PetOwners.SingleOrDefault(petOwner => petOwner.id == id);

            if (petOwner is null)
            {
                return NotFound();
            }
            _context.PetOwners.Remove(petOwner);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, PetOwner petOwner) 
        {
            Console.WriteLine("in PUT");
            if ( id != petOwner.id) {
                return BadRequest();
            }
            
            if(
            !RegexUtilities.IsValidEmail(petOwner.emailAddress)
            ) return BadRequest();
        
            _context.Update(petOwner);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
