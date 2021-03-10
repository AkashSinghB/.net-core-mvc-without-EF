using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MvcCrudeWithoutEF_EntityFramework_.Data;
using MvcCrudeWithoutEF_EntityFramework_.Models;
using System.Data.SqlClient;
using System.Data;


namespace MvcCrudeWithoutEF_EntityFramework_.Controllers
{
    public class BookController : Controller
    {
        private readonly IConfiguration _configuration;

        public BookController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // GET: Book
        public IActionResult Index()
        {
            DataTable dtbl = new DataTable();
            //using block will take care of the connection closing we dont need to close the connection manually the using block automatically take care of that
            using (SqlConnection Con = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                Con.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("BooksSelectProc",Con);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.Fill(dtbl);
            }
            return View(dtbl);
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
        public IActionResult Edit(BookViewModel bookViewModel)
        {

            if (ModelState.IsValid)
            {
                //using block will take care of the connection closing we dont need to close the connection manually the using block automatically take care of that
                using (SqlConnection Con = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Books_Proc", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("BookId", bookViewModel.BookId);
                    cmd.Parameters.AddWithValue("Title", bookViewModel.Title);
                    cmd.Parameters.AddWithValue("Author", bookViewModel.Author);
                    cmd.Parameters.AddWithValue("Price", bookViewModel.Price);
                    cmd.Parameters.AddWithValue("calltype", "insert");
                    cmd.ExecuteNonQuery();
                }
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
