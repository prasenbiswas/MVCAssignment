using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingWebsite.ViewModel
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        [Required(ErrorMessage ="Please Enter Category Name!!")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage ="Please Enter SubCategory Name!!")]
        public string SubCategoryName { get; set; }
        [Required(ErrorMessage ="Please Enter Product Name!!")]
        public string ProductName { get; set; }
        [Required(ErrorMessage ="Please Enter Price!!")]
        public float Price { get; set; }    
        public DateTime? Created_on { get; set; }
    }
}