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
    /// <summary>
    /// Edit and Create in same razor page.
    /// </summary>
    public class UpsertModel : PageModel
    {

        private ApplicationDbContext _db;

        [BindProperty]
        public Book Book { get; set; }

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Finds a book with input Id to show on the Edit page.
        /// </summary>
        /// <param name="id">ID for the book that should be edited.</param>
        /// <returns></returns>
        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();
            if(id == null)
            {
                //Create
                return Page();
            }
            else
            {
                //Update
                Book = await _db.Book.FirstOrDefaultAsync(b => b.Id == id);
                if(Book == null)
                {
                    return NotFound();
                }
                else
                {
                    return Page();
                }
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if(Book.Id == 0)
                {
                    _db.Book.Add(Book);
                }
                else
                {
                    //Updates every property of the book
                    _db.Book.Update(Book);
                }

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