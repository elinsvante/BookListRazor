using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookListRazor.Model;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor
{
    public class IndexModel : PageModel
    {

        private readonly ApplicationDbContext _db;
        public IEnumerable<Book> Books { get; set; }

        /// <summary>
        /// Getting ApplicationDbContext using dependency injection.
        /// </summary>
        /// <param name="db"></param>
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Handeler that gets all Books from database and store in IEnumerable<Book> 
        /// (Async let you run multiple tasks at a time until it is awaited).
        /// </summary>
        /// <returns></returns>
        public async Task OnGet()
        {
            //Wait for all books to be found.
            Books = await _db.Book.ToListAsync();
        }

        /// <summary>
        /// Post handler that deletes the Book from the database. It is Post Handler because the Delete button is a type button and the handler name is "Delete".
        /// </summary>
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await _db.Book.FindAsync(id);
            //Book with id does not exist
            if(book == null)
            {
                return NotFound();
            }
            else
            {
                _db.Book.Remove(book);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
        }
    }
}