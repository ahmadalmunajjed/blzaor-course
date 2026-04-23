using BlzaorBookStore.Services;
using Microsoft.AspNetCore.Components;

namespace BlzaorBookStore.Pages;

public class CartBase : BlzaorBookStoreComponentBase, IDisposable
{
    [Inject] protected CartService Cart { get; set; } = default!;
    [Inject] protected NavigationManager Navigation { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        Cart.OnChange += StateHasChanged;
        await Cart.InitializeAsync();
    }

    protected async Task UpdateQuantityAsync(CartItem item, int quantity)
    {
        await Cart.UpdateQuantityAsync(item.ProductId, quantity);
    }

    protected async Task IncrementAsync(CartItem item)
    {
        await Cart.UpdateQuantityAsync(item.ProductId, item.Quantity + 1);
    }

    protected async Task DecrementAsync(CartItem item)
    {
        await Cart.UpdateQuantityAsync(item.ProductId, item.Quantity - 1);
    }

    protected async Task RemoveAsync(CartItem item)
    {
        await Cart.RemoveAsync(item.ProductId);
    }

    protected async Task ClearAsync()
    {
        await Cart.ClearAsync();
    }

    protected string GetRemoveConfirmationMessage(CartItem item)
    {
        return string.Format(L["ItemWillBeDeletedMessage"], item.Name);
    }

    protected void GoToCatalog()
    {
        Navigation.NavigateTo("/catalog");
    }

    public void Dispose()
    {
        Cart.OnChange -= StateHasChanged;
    }
}
