using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;  // Private field
         
        //Implementation of ApplicationDbContext where the conncection to the database is already made
        //Constructor
        public CategoryController(ApplicationDbContext db)  
        {
            _db = db;
        }

        //Getting data from database
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories.ToList();  //It will go to db and retrieve all the categories, convert to a list and assign that inside variable objcategoryList
            return View(objCategoryList);
        }

        //GET 
        public IActionResult Create()
        {
            return View();  //When the view is being loaded we don't have to pass any model. We create the model inside the view
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]  //For security reason
        public IActionResult Create(Category obj)
        {
            //Validation 1
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            //validation 2
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);    //Adding object to database      
                _db.SaveChanges();  //Saving changes to database
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
           return View(obj);
        }


        //GET
        //Displaying the existing functionalities of the category that was selected
        public IActionResult Edit(int? id)
        { 
            if (id == null || id == 0) 
            {
                return NotFound();  //Invalid id
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id); //The first one if there are more
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);   

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);  
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public IActionResult Edit(Category obj)
        {
            //Validation 1
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            //Validation 2
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);  //Built in method Update      
                TempData["success"] = "Category updated successfully";
                _db.SaveChanges();  
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();  //Invalid id
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id); //The first one if there are more
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);   

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)    //Passing the id and Based on that id we will first retrieve that category
        {
            var obj = _db.Categories.Find(id);  
            if (obj == null)
            {
                return NotFound();
            }
            
            _db.Categories.Remove(obj);  //Built in method Remove    
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
