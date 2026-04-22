using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using BlzaorBookStore.Services.Dtos.Books;

namespace BlzaorBookStore;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class BlzaorBookStoreBlazorMappers : MapperBase<BookDto, CreateUpdateBookDto>
{
    public override partial CreateUpdateBookDto Map(BookDto source);
    public override partial void Map(BookDto source, CreateUpdateBookDto destination);
}
