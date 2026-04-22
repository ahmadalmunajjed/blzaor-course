namespace BlzaorBookStore.Services.Dtos.Products;

public class GetProductsInput
{
    public string? Filter { get; set; }
    public string? Sorting { get; set; }
    public Guid? CategoryId { get; set; }
}
