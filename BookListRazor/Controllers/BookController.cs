using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookListRazor.Model;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Controllers
{
    //Route that should be used to call this controller
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Retrieves book and pass it back when this api call is called. 
        /// To support API and adding api calls add services.AddControllerWithViews and endpoints.MapControllers in startup-file.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Book.ToListAsync() });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var bookFromId = await _db.Book.FirstOrDefaultAsync(b => b.Id == id);
            if(bookFromId == null)
            {
                return Json(new { success = false, message="Error while deleting." });
            }
            else
            {
                _db.Book.Remove(bookFromId);
                await _db.SaveChangesAsync();
                return Json(new { success = true, message = "Delete successful" });
            }
        }
    }
}