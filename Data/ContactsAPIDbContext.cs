using ConstactsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstactsAPI.Data
{
    public class ContactsAPIDbContext: DbContext
    {
        public ContactsAPIDbContext(DbContextOptions options) : base(options) 
        {
            
        }

        public DbSet<Contact> Contacts { get; set; }
    } 
}
