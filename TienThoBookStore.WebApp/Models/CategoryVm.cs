using System.ComponentModel.DataAnnotations;

namespace TienThoBookStore.WebApp.Models
{
    public class CategoryVm
    {
        public int CategoryId; 
        [Required] public string Name;
    }
}
