using System.ComponentModel.DataAnnotations;

namespace StockManagementMVC.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập mã sản phẩm")]
        [StringLength(100)]
        public string ProductCode { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [StringLength(200)]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mô tả sản phẩm")]
        [StringLength(200)]
        public string ProductDescription { get; set; }   
        [Range(0, int.MaxValue, ErrorMessage = "Vui lòng nhập số lượng sản phẩm")]
        public int ProductQuantity { get; set; }
        [Required]
        [StringLength(100)]
        public string ProductUnit { get; set; }
        [Required]

        public string Location { get; set; }



        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpdatedDate { get; set; }







    }
}
