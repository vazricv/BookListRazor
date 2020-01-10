using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Controllers
{
    [Route("api/Book")]
    [Controller]
    public class BookController : Controller
    {
        
        private readonly ApplicationDBContext db;

        public BookController(ApplicationDBContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await db.Books.ToListAsync()});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDB = await db.Books.FirstOrDefaultAsync(b => b.Id == id);
            if(bookFromDB == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            db.Books.Remove(bookFromDB);
            await db.SaveChangesAsync();
            return Json(new { success = true, message = "Deleted succesfully" });
        }
    }
}