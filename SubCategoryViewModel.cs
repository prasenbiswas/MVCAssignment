using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingWebsite.ViewModel
{
    public class SubCategoryViewModel
    {
        public int SubcategoryId { get; set; }
        [Required(ErrorMessage = "Please Enter Category Name!!")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Please Enter SubCategory Name!!")]
        public string SubCategoryName { get; set; }
        public DateTime? Created_on { get; set; }
    }
}