using Volo.Abp.Domain.Entities;

namespace BlzaorBookStore.Entities.Products;

public class Product : Entity<Guid>
{
    public string Name { get; set; } = string.Empty;

    public float Price { get; set; }
}
