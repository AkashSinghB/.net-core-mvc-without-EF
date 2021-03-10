using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcCrudeWithoutEF_EntityFramework_.Data;
using MvcCrudeWithoutEF_EntityFramework_.Models;

namespace MvcCrudeWithoutEF_EntityFramework_.Controllers
{
    public class BookController : Controller
    {
        

        public BookController()
        {
          
        }

        // GET: Book
        public IActionResult Index()
        {
            return View();
        }

        

       
        // GET: Book/Edit/5
        public IActionResult Edit(int? id)
        {
            BookViewModel bookViewModel = new BookViewModel();

            return View(bookViewModel);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("BookId,Title,Author,Price")] BookViewModel bookViewModel)
        {

            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(bookViewModel);
        }

        // GET: Book/Delete/5
        public IActionResult Delete(int? id)
        {
          

            return View();
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
           
            return RedirectToAction(nameof(Index));
        }

        
    }
}
