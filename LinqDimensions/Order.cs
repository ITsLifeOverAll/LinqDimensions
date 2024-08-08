namespace LinqDimensions;

public class Order
{
    public int CustomerId { get; set; }
    public int OrderId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double Total => Quantity * Price;

    public override string ToString() => $"{OrderId, 4}, {ProductName, -8}, {Quantity, 4}, {Price, 8:N1}, {Total, 8:N1}";
}