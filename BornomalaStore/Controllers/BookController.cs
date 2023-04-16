using BornomalaStore.Data;
using BornomalaStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BornomalaStore.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment environment;

        public BookController(ApplicationDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            this.environment = environment;
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
            string uniqueFileName= UploadImage(obj);
            string uniqueAuthorFileName= UploadImage(obj);

            var data = new Book()
            {
                BookTitle = obj.BookTitle,
                Language = obj.Language,
                Path = uniqueFileName,
                AuthorName = obj.AuthorName,
                AuthorAddress = obj.AuthorAddress,
                AuthorPath = uniqueAuthorFileName,

            };
                
                _db.Books.Add(data);
                _db.SaveChanges();//pushto databse
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");

            //}
            //return View(obj);
        }
        
        private string UploadImage(Book obj)
        {
            string uniqueFileName = string.Empty;
            if(obj.ImagePath != null)
            {
                string uploadFolder = Path.Combine(environment.WebRootPath, "Image/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + obj.ImagePath.FileName;
                string filePath= Path.Combine(uploadFolder, uniqueFileName);
                using(var fileStream=new FileStream(filePath, FileMode.Create))
                {
                    obj.ImagePath.CopyTo(fileStream);
                }
            }
            if (obj.AuthorImage != null)
            {
                string uploadFolder = Path.Combine(environment.WebRootPath, "Image/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + obj.AuthorImage.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    obj.AuthorImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
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
