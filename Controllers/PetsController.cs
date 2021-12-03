using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetsController(ApplicationContext context)
        {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        [HttpGet]
        public IEnumerable<Pet> GetPets()
        {
            return _context.Pets.Include(pet => pet.petOwner);
        }

        [HttpPost]
        public IActionResult Create(Pet pet)
        {
            _context.Add(pet);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Create), new { id = pet.id }, pet);
        }
        [HttpGet("{id}")]
        public Pet GetById(int id)
        {
            return _context.Pets
                .Include(Pet => Pet.petOwner)
                .SingleOrDefault(Pet => Pet.id == id);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Pet pet = _context.Pets.SingleOrDefault(pet => pet.id == id);

            if (pet is null)
            {
                return NotFound();
            }
            _context.Pets.Remove(pet);
            _context.SaveChanges();

            return NoContent();
        }
        [HttpPut("{id}")]

        public IActionResult Update(int id, Pet pet)
        {
            Console.WriteLine("Update", id);
            Console.WriteLine("Update", pet.breed);
            if (id != pet.id)
            {
                Console.WriteLine(id.ToString(), pet.id);
                return BadRequest();
            }
            _context.Update(pet);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}/checkin")]

        public IActionResult CheckIn(int id)
        {

            Pet pet = _context.Pets.SingleOrDefault(pet => pet.id == id);

            if (pet is null)
            {
                Console.WriteLine(id.ToString(), pet.id);
                return BadRequest();
            }
            pet.checkInAt = DateTime.Now;
            _context.Update(pet);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
