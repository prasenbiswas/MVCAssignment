using ShoppingWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ShoppingWebsite.ViewModel;
using System.Reflection;
using System.Web.Helpers;

namespace ShoppingWebsite.Controllers
{
    public class ProductTempModel
    {
        public int ProductId { get; set; }
        public string CategoryName { get; set; }
        public int SubCategoryId { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
    }

    public class ProductDeleteModel
    {
        public int Id { get; set; }
    }

    public class ProductController : Controller
    {
        // GET: Product
        CategoryContext Db = new CategoryContext();
        public ActionResult Index()
        {
            List<Category> list = Db.Categories.ToList();
            ViewBag.CategoryList = new SelectList(list, "CategoryId", "CategoryName");

            var data = Db.Products.ToList();
            List<ProductViewModel> ProductViewModels = new List<ProductViewModel>();
            foreach (var item in data)
            {
                ProductViewModels.Add(new ProductViewModel
                {
                    ProductId = item.ProductId,
                    CategoryName = item.SubCategory.Category.CategoryName,
                    SubCategoryName = item.SubCategory.SubCategoryName,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Created_on = DateTime.Now
                });
            }
            return View(ProductViewModels);
        }

        //------------------- Data create Action -------------------

        [HttpPost]
        public ActionResult Create(ProductTempModel data)
        {
            int catId = int.Parse(data.CategoryName);
            int SubcatId = (data.SubCategoryId);

            var addData = new Product()
            { 
                SubCategoryId = SubcatId,
                SubCategory = Db.SubCategories.FirstOrDefault(x => x.SubCategoryId == SubcatId),
                ProductName = data.ProductName,
                Price=data.Price,
                Created_On = DateTime.Now
            };
            Db.Products.Add(addData);
            Db.SaveChanges();
            return View();
        }

        //-------------- Edit -------------------

        [HttpPost]
        public ActionResult Edit(ProductTempModel modal)
        {
            int catId = int.Parse(modal.CategoryName);
            int subcatId = (modal.SubCategoryId);
            var modalCat = Db.Categories.Where(x => x.CategoryId == catId).FirstOrDefault();
            var modalSubCat = Db.SubCategories.Where(x => x.SubCategoryId == subcatId).FirstOrDefault();

            var editProduct = new Product { 
                ProductId = modal.ProductId,
                SubCategoryId =subcatId,
               SubCategory = Db.SubCategories.FirstOrDefault(x=>x.SubCategoryId == subcatId),
                ProductName = modal.ProductName,
                Price = modal.Price,
                Created_On = DateTime.Now
            };
            
            Db.Entry(editProduct).State = EntityState.Modified;
            Db.SaveChanges();
            return View();
        }


        //------------------- Delete Data -------------

        [HttpPost]
        public ActionResult Delete(ProductDeleteModel ProductDeleteModel)
        {
            var deletedata3 = Db.Products.Where(model => model.ProductId == ProductDeleteModel.Id).FirstOrDefault();
            Db.Entry(deletedata3).State = EntityState.Deleted;
            Db.SaveChanges();
            return View(deletedata3);
        }

        //------------------------- DropDown Action Method For Add---------------- 
        public JsonResult GetSubCategoryList(int CategoryId)
        {
            Db.Configuration.ProxyCreationEnabled = false;
            List<SubCategory> subCategoryList = Db.SubCategories.Where(x => x.CategoryId == CategoryId).Include(y => y.Category).ToList();
           
            return Json(subCategoryList,JsonRequestBehavior.AllowGet);
        } 
        
        //------------------------- DropDown Action Method For Edit---------------- 
        public JsonResult GetSubCategoryListEdit(int CategoryId)
        {
            Db.Configuration.ProxyCreationEnabled = false;
            List<SubCategory> subCategoryList = Db.SubCategories.Where(x => x.CategoryId == CategoryId).Include(y => y.Category).ToList();
           
            return Json(subCategoryList,JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDataEdit(int Id)
        {
            var data = Db.Products.Include(_ => _.SubCategory).Where(model => model.ProductId == Id).FirstOrDefault();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}