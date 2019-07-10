using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AddressBook.Models;
using MicroserviceTest.Models;

namespace AddressBook.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly AddressBookContext _context;

        public AddressesController(AddressBookContext context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public IEnumerable<Address> GetAddress()
        {
            return _context.Address;
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddress([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var address = await _context.Address.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // PUT: api/Addresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress([FromRoute] int id, [FromBody] Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != address.Id)
            {
                return BadRequest();
            }

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Addresses
        [HttpPost]
        public async Task<IActionResult> PostAddress([FromBody] Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Address.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = address.Id }, address);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var address = await _context.Address.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Address.Remove(address);
            await _context.SaveChangesAsync();

            return Ok(address);
        }

        private bool AddressExists(int id)
        {
            return _context.Address.Any(e => e.Id == id);
        }


        
        // GET: api/AddressesByCity
        //Return type is a query so async/task is not needed
        [HttpGet("AddressesByCity")]
        public IQueryable<IGrouping<string, Address> > GetAddressByCity()
        {

            //StringComparer.InvariantCultureIgnoreCase is not understood by a queryable under Entity Framework
            //strings "London" and "london" are not equal and will be grouped separately without ToUpper(), however this is not optimal
            //Simply grouping by ToUpper() or ToUpperInvariant() will prevent query optimization
            //Example: https://stackoverflow.com/questions/19602589/case-insensitive-string-comparison-not-working-in-c
            //Example 2: https://stackoverflow.com/questions/3843060/linq-to-entities-case-sensitive-comparison
            //Database column should be forced to ignore case ideally

            //Added string formatting so that cities of the same name of different countries are not grouped together
            //e.g. New York, New York vs. New York, Florida
            //Can use anonymous key selector, string formatting, city ID's, etc.
            return _context.Address
                .GroupBy(address => string.Format("{0}&&{1}", address.City.ToUpper(), address.Country.ToUpper()));
        }
    }
}