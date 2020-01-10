using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDBContext db;

        public UpsertModel(ApplicationDBContext db)
        {
            this.db = db;
        }

        [BindProperty]
        public Book Book { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();
            if(id == null)
            {
                return Page();
            }

            //update
            Book = await db.Books.FirstOrDefaultAsync(b => b.Id == id);
            if(Book == null)
            {
                return NotFound();

            }
            return Page();
            //Book = await db.Books.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if(Book.Id == 0)
                {
                    await db.Books.AddAsync(Book);
                }
                else
                {
                    db.Books.Update(Book);
                }
                
                await db.SaveChangesAsync();

                return RedirectToPage("Index");
            }

            return RedirectToPage();
        }
    }
}