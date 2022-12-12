using ShoppingWebsite.Models;
using ShoppingWebsite.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShoppingWebsite.Controllers
{
    //public class UserTempModel
    //{
    //    public int Id { get; set; }
    //    public string Categoryname { get; set; }

    //    public DateTime Created_On { get; set; }
    //}
    public class UserController : Controller
    {
        // GET: User
        CategoryContext Db = new CategoryContext();
        public ActionResult Index()
        {
            var data = Db.Categories.ToList();
            return View(data);
        }
        public ActionResult SubCategory(int id)
        {

            var data = Db.SubCategories.Where(x => x.Category.CategoryId == id).ToList();         
            
            return View(data);
        }

        public ActionResult Products(int id)
        {
            var data = Db.Products.Where(x => x.SubCategory.SubCategoryId == id).ToList();
            return View(data);
        }

    }
}