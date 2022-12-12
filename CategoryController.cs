using ShoppingWebsite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingWebsite.Controllers
{
    public class CategoryEditModal
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Please Enter Category Name!!")]
        public string CategoryName { get; set; }
    }
    public class CategoryDeleteModal
    {
        public int Id { get; set; }
    }

    public class CategoryController : Controller
    {

        // GET: Category
        CategoryContext Db = new CategoryContext();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
            var data = Db.Categories.ToList();
            return View(data);
        }



        [HttpPost]
        public ActionResult Edit(CategoryEditModal modal)
        {
            
            var modalToBeEdited = Db.Categories.Where(x => x.CategoryId == modal.CategoryId).FirstOrDefault();
            modalToBeEdited.CategoryName = modal.CategoryName;
            Db.Entry(modalToBeEdited).State = EntityState.Modified;
            Db.SaveChanges();
            return View();
        }

        [HttpPost]
        public ActionResult Delete(CategoryDeleteModal categoryDeleteModal)
        {
            var deletedata = Db.Categories.Where(model => model.CategoryId == categoryDeleteModal.Id).FirstOrDefault();
            Db.Entry(deletedata).State = EntityState.Deleted;
            Db.SaveChanges();
            return View(deletedata);
        }
    }
}