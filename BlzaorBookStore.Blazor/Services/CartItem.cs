namespace BlzaorBookStore.Services;

public class CartItem
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public float Price { get; set; }
    public int Quantity { get; set; }

    public float Subtotal => Price * Quantity;
}
