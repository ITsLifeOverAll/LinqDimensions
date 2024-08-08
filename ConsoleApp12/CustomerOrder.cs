namespace LinqDimensions;

public class CustomerOrder
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public List<Order> Orders { get; set; }
}
