using BornomalaStore.Data;
using BornomalaStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BornomalaStore.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BookController(ApplicationDbContext db)
        {
            _db = db;
            
        }
        public IActionResult Index()
        {
            IEnumerable<Book> objBookList = _db.Books;
            return View(objBookList);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book obj)
        {
            //if (ModelState.IsValid)
            //{
                _db.Books.Add(obj);
                _db.SaveChanges();//pushto databse
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");

            //}
            //return View(obj);
        }
        public IActionResult BookDetail(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var BookFromDb = _db.Books.Find(id);
            if (BookFromDb == null)
            {
                return NotFound();
            }
            return View(BookFromDb);
        }

        public IActionResult AuthorDetail(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var BookFromDb = _db.Books.Find(id);
            if (BookFromDb == null)
            {
                return NotFound();
            }
            return View(BookFromDb);
        }
    }
}
