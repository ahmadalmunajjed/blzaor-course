using System.Text.Json;
using BlzaorBookStore.Services.Dtos.Products;
using Microsoft.JSInterop;

namespace BlzaorBookStore.Services;

public class CartService
{
    private const string StorageKey = "bookstore.cart";

    private readonly IJSRuntime _jsRuntime;
    private readonly List<CartItem> _items = new();
    private bool _initialized;

    public CartService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public IReadOnlyList<CartItem> Items => _items;

    public int TotalQuantity => _items.Sum(i => i.Quantity);

    public float TotalPrice => _items.Sum(i => i.Subtotal);

    public event Action? OnChange;

    public async Task InitializeAsync()
    {
        if (_initialized)
        {
            return;
        }

        try
        {
            var json = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", StorageKey);
            if (!string.IsNullOrWhiteSpace(json))
            {
                var stored = JsonSerializer.Deserialize<List<CartItem>>(json);
                if (stored is not null)
                {
                    _items.Clear();
                    _items.AddRange(stored);
                }
            }
        }
        catch
        {
            // ignore corrupted storage — start with empty cart
        }

        _initialized = true;
        OnChange?.Invoke();
    }

    public async Task AddAsync(ProductDto product, int quantity = 1)
    {
        if (quantity <= 0)
        {
            return;
        }

        var existing = _items.FirstOrDefault(i => i.ProductId == product.Id);
        if (existing is null)
        {
            _items.Add(new CartItem
            {
                ProductId = product.Id,
                Name = product.Name ?? string.Empty,
                Price = product.Price.GetValueOrDefault(),
                Quantity = quantity
            });
        }
        else
        {
            existing.Quantity += quantity;
        }

        await PersistAsync();
    }

    public async Task UpdateQuantityAsync(Guid productId, int quantity)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item is null)
        {
            return;
        }

        if (quantity <= 0)
        {
            _items.Remove(item);
        }
        else
        {
            item.Quantity = quantity;
        }

        await PersistAsync();
    }

    public async Task RemoveAsync(Guid productId)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item is null)
        {
            return;
        }

        _items.Remove(item);
        await PersistAsync();
    }

    public async Task ClearAsync()
    {
        _items.Clear();
        await PersistAsync();
    }

    private async Task PersistAsync()
    {
        var json = JsonSerializer.Serialize(_items);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
        OnChange?.Invoke();
    }
}
