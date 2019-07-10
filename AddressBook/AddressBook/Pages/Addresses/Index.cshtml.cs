using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AddressBook.Models;
using MicroserviceTest.Models;

namespace AddressBook.Pages.Addresses
{
    public class IndexModel : PageModel
    {
        private readonly AddressBook.Models.AddressBookContext _context;

        public IndexModel(AddressBook.Models.AddressBookContext context)
        {
            _context = context;
        }

        public IList<Address> Address { get;set; }

        public async Task OnGetAsync()
        {
            Address = await _context.Address.ToListAsync();
        }
    }
}
