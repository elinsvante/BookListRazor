using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookListRazor.Model;

namespace BookListRazor
{
    public class EditModel : PageModel
    {

        private ApplicationDbContext _db;

        [BindProperty]
        public Book Book { get; set; }

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Finds a book with input Id to show on the Edit page.
        /// </summary>
        /// <param name="id">ID for the book that should be edited.</param>
        /// <returns></returns>
        public async Task OnGet(int id)
        {
            Book = await _db.Book.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                //Fetch the book object from the database.
                var BookFromId = await _db.Book.FindAsync(Book.Id);
                //Updates the values
                BookFromId.Name = Book.Name;
                BookFromId.Author = Book.Author;
                BookFromId.ISBN = Book.ISBN;

                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}