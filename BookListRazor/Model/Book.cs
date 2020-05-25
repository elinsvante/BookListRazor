using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace BookListRazor.Model
{
    //Create a Table based on this Model in Database - Add to migration (add-migration AddBookToDb + update-database)
    public class Book
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        public string Author { get; set; }


        // When adding a new property in the model - add a new migration in "Manage Nuget Packages for Solution".
        // add-migration AddISBNToBookModel + update-database
        public string ISBN { get; set; }
    }
}
