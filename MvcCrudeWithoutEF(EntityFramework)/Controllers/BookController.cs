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
                SqlDataAdapter sqlDa = new SqlDataAdapter("Books_Proc",Con);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("calltype", "selectAll");
                sqlDa.Fill(dtbl);
            }
            return View(dtbl);
        }

        

       
        // GET: Book/Edit/5
        public IActionResult Edit(int? id)
        {
            BookViewModel bookViewModel = new BookViewModel();
            if(id > 0)
            {
                bookViewModel = FetchBookById(id);
            }
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
                    if (bookViewModel.BookId == 0)
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
                    else
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
            BookViewModel bookViewModel = FetchBookById(id);
            return View(bookViewModel);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            using (SqlConnection Con = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("Books_Proc", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("BookId",id);
                cmd.Parameters.AddWithValue("calltype", "delete");
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public BookViewModel FetchBookById ( int? id )
        {
            BookViewModel bookViewModel = new BookViewModel();

            using (SqlConnection Con = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                DataTable dtbl = new DataTable();
                Con.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("Books_Proc", Con);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("BookId", id);
                sqlDa.SelectCommand.Parameters.AddWithValue("calltype", "selectById");
                sqlDa.Fill(dtbl);
                if(dtbl.Rows.Count == 1)
                {
                    bookViewModel.BookId = Convert.ToInt32(dtbl.Rows[0]["BookId"].ToString());
                    bookViewModel.Title = dtbl.Rows[0]["Title"].ToString();
                    bookViewModel.Author = dtbl.Rows[0]["Author"].ToString();
                    bookViewModel.Price = Convert.ToInt32(dtbl.Rows[0]["Price"].ToString());
                }
                return bookViewModel;
            }

        }
    

    }
}
