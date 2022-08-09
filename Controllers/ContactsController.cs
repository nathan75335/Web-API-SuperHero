using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroWebApi.Data;
using SuperHeroWebApi.Models;

namespace SuperHeroWebApi.Controllers
{ 
    [ApiController]
    [Route("api/Contacts")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext _db;

        public ContactsController(ContactsAPIDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            return Ok(await _db.Contacts.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute]Guid id)
        {
            var contact = await _db.Contacts.FindAsync(id);
            if(contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> AddContacts(AddContactRequest obj)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                FullName = obj.FullName,
                Email = obj.Email,
                Adress = obj.Adress,
                Phone = obj.Phone
            };
            await _db.Contacts.AddAsync(contact);
            await _db.SaveChangesAsync();

            return Ok(contact);    
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContacts([FromRoute] Guid id , UpdateContactRequest obj)
        {
            var contact = _db.Contacts.Find(id);
            if (contact != null)
            {
                contact.FullName = obj.FullName;
                contact.Email = obj.Email;
                contact.Adress = obj.Adress;
                contact.Phone = obj.Phone; 
               
                await _db.SaveChangesAsync();
                return Ok(contact); 
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContacts([FromRoute] Guid id)
        {
            var contact = await _db.Contacts.FindAsync(id);
            if (contact != null)
            {
                _db.Contacts.Remove(contact);
                _db.SaveChanges();
                return Ok(contact);
            }
           
            return NotFound();
        }
    }
}
