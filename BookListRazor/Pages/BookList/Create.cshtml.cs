using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookListRazor.Model;

namespace BookListRazor
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Property for creating book labels. BindProperty - OnPost assumes that it will be getting a Book object.
        /// </summary>
        [BindProperty]
        public Book Book { get; set; }

        public CreateModel(ApplicationDbContext db) 
        {
            _db = db;
        }


        public void OnGet()
        {

        }

        /// <summary>
        /// Post-handler for creating a new Book Object. Task of IActionResult because redirecting to a new page.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPost()
        {
            //Validation for server-side. ModelState.IsValid will return false if required properties is not entered. 
            if(ModelState.IsValid)
            {
                //Add Book to queue.
                await _db.Book.AddAsync(Book);

                //Push changes to database.
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");

            }
            else
            {
                return Page();
            }
        }
    }
}