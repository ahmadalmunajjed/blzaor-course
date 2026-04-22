using BlzaorBookStore.Services.Dtos.Products;
using Volo.Abp.Application.Services;

namespace BlzaorBookStore.Services.Products;

public interface IProductService : IApplicationService
{
    Task<ProductDto> CreateAsync(CreateUpdateProductDto input);
    Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input);
    Task<ProductDto> GetAsync(Guid id);
    Task<List<ProductDto>> GetListAsync(GetProductsInput input);
    Task DeleteAsync(Guid id);
}
