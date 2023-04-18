using BornomalaStore.Data;
using BornomalaStore.Models;
//using Microsoft.AspNetCore.Http;
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
        public  IActionResult Create(Book obj)
        {
            //if (ModelState.IsValid)
            //{
            string uniqueFileName = UploadImage(obj.ImagePath);
            string uniqueAuthorFileName= UploadImage(obj.AuthorImage);

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
        
        private string UploadImage(IFormFile img)
        {
            string uniqueFileName = string.Empty;
            if(img != null)
            {
                string uploadFolder = Path.Combine(environment.WebRootPath, "Image/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + img.FileName;
                string filePath= Path.Combine(uploadFolder, uniqueFileName);
                using(var fileStream=new FileStream(filePath, FileMode.Create))
                {
                    img.CopyTo(fileStream);
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

        //GET method for Edit
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var bookFromDb = _db.Books.Find(id);
            
            if (bookFromDb == null)
            {
                return NotFound();
            }

            return View(bookFromDb);//as we are using get method so no need to pass any object like previous action
        }
        [HttpPost]
        
        public IActionResult Update(Book obj)
        {


            var bookFromDb = _db.Books.SingleOrDefault(e => e.Id == obj.Id);
            string uniqueFileName = string.Empty;
            if(obj.ImagePath != null)
            {
                if(bookFromDb.Path!=null)
                {
                    string filepath = Path.Combine(environment.WebRootPath, "Image/",bookFromDb.Path);
                    if (System.IO.File.Exists(filepath))
                    {
                        System.IO.File.Delete(filepath);
                    }
                }
                uniqueFileName = UploadImage(obj.ImagePath);

            }
            bookFromDb.BookTitle = obj.BookTitle;
            bookFromDb.Language = obj.Language;
            bookFromDb.AuthorName = obj.AuthorName;
            bookFromDb.AuthorAddress = obj.AuthorAddress;
            if(obj.ImagePath!=null)
            {
                bookFromDb.Path = uniqueFileName;
            }
            _db.Books.Update(bookFromDb);
            _db.SaveChanges();//pushto databse
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");

            

        }

        //GET method for Delete
        public IActionResult Delete(int? id)
        {
            if ( id == 0)
            {
                return NotFound();
            }
            else
            {
                var bookFromDb = _db.Books.SingleOrDefault(e => e.Id == id);
                if (bookFromDb != null)
                {
                    string currentImage = Path.Combine(environment.WebRootPath, "Image/",bookFromDb.Path);
                    string currentAuthorImage = Path.Combine(environment.WebRootPath, "Image/",bookFromDb.AuthorPath);
                   /*string currentImage = Path.Combine(Directory.GetCurrentDirectory(), deleteFromFolder, bookFromDb.Path);*/
                    

                    
                    
                        if (System.IO.File.Exists(currentImage))
                        {
                            System.IO.File.Delete(currentImage);
                        }
                    
                   
                    
                        if (System.IO.File.Exists(currentAuthorImage))
                        {
                            System.IO.File.Delete(currentAuthorImage);
                        }
                    
                    _db.Books.Remove(bookFromDb);
                    _db.SaveChanges();//pushto databse
                    TempData["success"] = "Category deleted successfully";
                }
            }
            
            return RedirectToAction("Index");
        }




    }
}
