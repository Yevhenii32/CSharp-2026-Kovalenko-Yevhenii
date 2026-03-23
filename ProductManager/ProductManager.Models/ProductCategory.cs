using System.ComponentModel.DataAnnotations;

namespace ProductManager.DBModels
{
    public enum ProductCategory
    {
        [Display(Name = "Електроніка")]
        Electronics,
        [Display(Name = "Меблі")]
        Furniture,
        [Display(Name = "Продукти харчування")]
        Groceries,
        [Display(Name = "Одяг")]
        Clothing
    }
}