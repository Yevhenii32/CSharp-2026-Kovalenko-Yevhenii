using System.ComponentModel.DataAnnotations;

namespace ProductManager.DBModels
{
    public enum Location
    {
        [Display(Name = "Київ")]
        Kyiv,
        [Display(Name = "Львів")]
        Lviv,
        [Display(Name = "Харків")]
        Kharkiv,
        [Display(Name = "Одеса")]
        Odesa
    }
}