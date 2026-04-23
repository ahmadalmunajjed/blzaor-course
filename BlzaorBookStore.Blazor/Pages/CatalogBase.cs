using BlzaorBookStore.Services;
using BlzaorBookStore.Services.Dtos.Products;
using BlzaorBookStore.Services.Products;
using Microsoft.AspNetCore.Components;

namespace BlzaorBookStore.Pages;

public class CatalogBase : BlzaorBookStoreComponentBase, IDisposable
{
    [Inject] protected IProductService ProductService { get; set; } = default!;
    [Inject] protected CartService Cart { get; set; } = default!;
    [Inject] protected NavigationManager Navigation { get; set; } = default!;

    protected IReadOnlyList<ProductDto> Items { get; private set; } = new List<ProductDto>();
    protected Dictionary<Guid, int> Quantities { get; } = new();
    protected string? LastAddedName { get; private set; }
    protected bool ShowAddedAlert { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        Cart.OnChange += StateHasChanged;
        await Cart.InitializeAsync();
        Items = await ProductService.GetListAsync(new GetProductsInput());

        foreach (var item in Items)
        {
            Quantities[item.Id] = 1;
        }
    }

    protected int GetQuantity(Guid productId)
    {
        return Quantities.TryGetValue(productId, out var q) ? q : 1;
    }

    protected void SetQuantity(Guid productId, int value)
    {
        Quantities[productId] = value < 1 ? 1 : value;
    }

    protected async Task AddToCartAsync(ProductDto product)
    {
        var qty = GetQuantity(product.Id);
        await Cart.AddAsync(product, qty);

        LastAddedName = product.Name;
        ShowAddedAlert = true;
        Quantities[product.Id] = 1;
    }

    protected void DismissAlert()
    {
        ShowAddedAlert = false;
    }

    protected void GoToCart()
    {
        Navigation.NavigateTo("/cart");
    }

    public void Dispose()
    {
        Cart.OnChange -= StateHasChanged;
    }
}
