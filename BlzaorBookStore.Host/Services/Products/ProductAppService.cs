using BlzaorBookStore.Entities.Products;
using BlzaorBookStore.Services.Dtos.Products;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace BlzaorBookStore.Services.Products;

public class ProductAppService : ApplicationService, IProductService
{
    private readonly IRepository<Product, Guid> _repository;

    public ProductAppService(IRepository<Product, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
    {
        // map input to entity manully
        var entity = new Product
        {
            Name = input.Name!,
            Price = input.Price.GetValueOrDefault()
        };

        // save entity to repository
        await _repository.InsertAsync(entity);

        // map entity to DTO
        var dto = new ProductDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Price = entity.Price
        };

        return dto;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<ProductDto> GetAsync(Guid id)
    {
        var entity = await _repository.GetAsync(id);
        var dto = new ProductDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Price = entity.Price
        };
        return dto;
    }

    public async Task<List<ProductDto>> GetListAsync(GetProductsInput input)
    {
        var queryable = await _repository.GetQueryableAsync();
        // filters
        if (!string.IsNullOrEmpty(input.Filter))
        {
            queryable = queryable.Where(p => p.Name.Contains(input.Filter));
        }
        // sorting
        if (!string.IsNullOrEmpty(input.Sorting))
        {
            // Apply sorting logic here
            switch (input.Sorting)
            {
                case "name":
                    queryable = queryable.OrderBy(p => p.Name);
                    break;
                case "name_desc":
                    queryable = queryable.OrderByDescending(p => p.Name);
                    break;
                default:
                    break;
            }
        }

        var entities = await AsyncExecuter.ToListAsync(queryable);
        var dtos = entities.Select(entity => new ProductDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Price = entity.Price
        }).ToList();
        return dtos;
    }

    public async Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input)
    {
        var entity = await _repository.GetAsync(id);
        entity.Name = input.Name!;
        await _repository.UpdateAsync(entity);
        var dto = new ProductDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Price = entity.Price
        };
        return dto;
    }
}
