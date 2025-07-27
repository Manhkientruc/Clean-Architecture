using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Web.Models;

public class Product
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
    public string Name { get; set; } = "";

    [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
    public decimal Price { get; set; }
}
