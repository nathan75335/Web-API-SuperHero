using Microsoft.EntityFrameworkCore;
using SuperHeroWebApi.Models;

namespace SuperHeroWebApi.Data
{
    public class ContactsAPIDbContext : DbContext
    {
        public ContactsAPIDbContext(DbContextOptions<ContactsAPIDbContext> options):base(options)
        {

        }
        public DbSet<Contact> Contacts { get; set; }
    }
}
