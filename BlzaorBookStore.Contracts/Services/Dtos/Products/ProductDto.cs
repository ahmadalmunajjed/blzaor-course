using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BlzaorBookStore.Services.Dtos.Products;

public class ProductDto : EntityDto<Guid>
{
    public string? Name { get; set; }
    public float? Price { get; set; }
}
