using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDBContext db;

        public CreateModel(ApplicationDBContext db)
        {
            this.db = db;
        }
        [BindProperty]
        public Book book { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                await db.Books.AddAsync(book);
                await db.SaveChangesAsync();
            }
            else
            {
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}