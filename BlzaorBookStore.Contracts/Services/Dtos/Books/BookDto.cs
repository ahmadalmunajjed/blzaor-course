using System;
using Volo.Abp.Application.Dtos;
using BlzaorBookStore.Entities.Books;

namespace BlzaorBookStore.Services.Dtos.Books;

public class BookDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; }

    public BookType Type { get; set; }

    public DateTime PublishDate { get; set; }

    public float Price { get; set; }
}