using System.ComponentModel.DataAnnotations;

namespace StockManagementMVC.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Vui lòng nhập Email để đăng kí!")]
        [EmailAddress(ErrorMessage ="Email không hợp lệ!")]
        public string Email {get; set;}
        [Required(ErrorMessage = "Vui lòng nhập Password để đăng kí!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
