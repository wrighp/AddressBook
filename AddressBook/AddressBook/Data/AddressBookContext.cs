using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MicroserviceTest.Models;

namespace AddressBook.Models
{
    public class AddressBookContext : DbContext
    {
        public AddressBookContext (DbContextOptions<AddressBookContext> options)
            : base(options)
        {
        }

        public DbSet<MicroserviceTest.Models.Address> Address { get; set; }
    }
}
