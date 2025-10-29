using System.ComponentModel.DataAnnotations;

namespace StockManagementMVC.Models.ViewModel
{
   
        public class LoginViewModel
        {
            [Required(ErrorMessage = "Vui lòng nhập Email để đăng nhập!")]
            [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập Password để đăng nhập!")]
            [DataType(DataType.Password)]
        public string Password { get; set; }
        public Boolean RememberMe { get; set; }
        }
    
}
