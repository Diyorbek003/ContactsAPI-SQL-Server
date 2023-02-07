using ConstactsAPI.Data;
using ConstactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstactsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext _context;
        public ContactsController(ContactsAPIDbContext contacts)
        {
            _context= contacts;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
           return Ok (await _context.Contacts.ToListAsync());
          
        }
        [HttpGet]
        [Route("{id:guid")]
        public async Task<IActionResult> GetContact(Guid id)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);
            if(contact == null)
            {
                return NotFound();
            }
            return Ok (contact);
        }

        [HttpPost]
        public async Task <IActionResult> AddContacts(AddContactsRequest addContactsRequest)
        {
            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                Address = addContactsRequest.Address,
                Email = addContactsRequest.Email,
                FullName = addContactsRequest.FullName,
                Phone = addContactsRequest.Phone
            };
            await _context.Contacts.AddAsync(contact);
             await _context.SaveChangesAsync();
            return Ok(contact);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact(Guid id,UpdateContactRequest updateContactRequest)
        {
           var contact =  _context.Contacts.Find(id);

            if(contact != null) 
            {
                contact.FullName = updateContactRequest.FullName;
                contact.Address = updateContactRequest.Address;
                contact.Email = updateContactRequest.Email;
                contact.Phone = updateContactRequest.Phone;
                await _context.SaveChangesAsync();

                return Ok(contact);
            }

            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if(contact != null)
            {
                _context.Contacts.Remove(contact); 
                await _context.SaveChangesAsync();
                return Ok();
            }

            return NotFound();
        }
    }
}
