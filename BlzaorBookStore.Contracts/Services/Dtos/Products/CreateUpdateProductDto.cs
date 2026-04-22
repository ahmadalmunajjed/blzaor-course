using System.ComponentModel.DataAnnotations;

namespace BlzaorBookStore.Services.Dtos.Products;

public class CreateUpdateProductDto
{
    [Required]
    public string? Name { get; set; }
    public float? Price { get; set; }
}
