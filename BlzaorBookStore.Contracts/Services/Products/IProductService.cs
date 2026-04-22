using BlzaorBookStore.Services.Dtos.Products;

namespace BlzaorProductStore.Services.Products;

public interface IProductService
{
    Task<ProductDto> CreateAsync(CreateUpdateProductDto input);
    Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input);
    Task<ProductDto> GetAsync(Guid id);
    Task<List<ProductDto>> GetListAsync(GetProductsInput input);
    Task DeleteAsync(Guid id);
}
