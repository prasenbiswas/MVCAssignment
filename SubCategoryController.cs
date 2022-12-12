using ShoppingWebsite.Models;
using ShoppingWebsite.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ShoppingWebsite.Controllers
{
    public class SubCatTempModel
    {
        public int SubcategoryId { get; set; }
        [Required(ErrorMessage ="Please Enter CategoryName!!")]
        public string CatName { get; set; }

        [Required(ErrorMessage ="Please Enter SubCategoryName!!")]
        public string SubCatName { get; set; }
    }

    public class SubCategoryDeleteModal
    {
        public int Id { get; set; }
    }
    public class SubCategoryController : Controller
    {
 
        CategoryContext Db = new CategoryContext();
        public ActionResult Index()
        {
            //var a = Db.SubCategories.Where(x => x.CategoryId == 3).ToList();

            List<Category> listSub = Db.Categories.ToList();
            ViewBag.DepartmentList = new SelectList(listSub, "CategoryId", "CategoryName");
            var data = Db.SubCategories.ToList();
            List<SubCategoryViewModel> subCategoryViewModels = new List<SubCategoryViewModel>();
            foreach(var item in data)
            {
                subCategoryViewModels.Add(new SubCategoryViewModel
                {
                    SubcategoryId = item.SubCategoryId,
                    CategoryName = item.Category.CategoryName,
                    SubCategoryName = item.SubCategoryName,
                    Created_on = DateTime.Now
                });
            }
            return View(subCategoryViewModels);
        }

        [HttpPost]
        public ActionResult Create(SubCatTempModel data)
        {
            int catId = int.Parse(data.CatName);
            var addData_Sub = new SubCategory()
            {

                CategoryId = catId,
                Category = Db.Categories.FirstOrDefault(x => x.CategoryId == catId),
                SubCategoryName = data.SubCatName,
                Created_On = DateTime.Now
            };
            Db.SubCategories.Add(addData_Sub);
            Db.SaveChanges();
            return View();
        }

        //---------------------- edit --------------------------


        [HttpPost]
        public ActionResult Edit(SubCatTempModel modal)
        {
            int catId = int.Parse(modal.CatName);

            var modalCat = Db.Categories.Where(x => x.CategoryId == catId).FirstOrDefault();
            var editSubCat = new SubCategory()
            {
                SubCategoryId = modal.SubcategoryId,
                CategoryId = catId,
                Category = Db.Categories.FirstOrDefault(x => x.CategoryId == catId),
                SubCategoryName = modal.SubCatName,
                Created_On = DateTime.Now
            };

            Db.Entry(editSubCat).State = EntityState.Modified;
            Db.SaveChanges();
            return View();
        }



        //----------------------- Delete --------------------------

        [HttpPost]
        public ActionResult Delete(SubCategoryDeleteModal SubCategoryDeleteModal)
        {
            var deletedata2 = Db.SubCategories.Where(model => model.SubCategoryId == SubCategoryDeleteModal.Id).FirstOrDefault();
            Db.Entry(deletedata2).State = EntityState.Deleted;
            Db.SaveChanges();
            return View(deletedata2);
        }

        public ActionResult EditDetails(int Id)
        {
            var data = Db.SubCategories.Include(_ => _.Category).Where(model => model.SubCategoryId == Id).FirstOrDefault();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


    }
}