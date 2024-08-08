
using Bogus;
using LinqDimensions;

// LINQ: 一維資料變成二維資料 (grouping)

var students = FakeStudents();
Console.WriteLine("Students:");
students.ForEach(s => Console.WriteLine(s.ToString('g')));
Console.WriteLine();

var groupByGrade = students.GroupBy(s => s.Grade);

foreach (var group in groupByGrade.OrderByDescending(g => g.Key))
{
    Console.WriteLine(group.Key);
    foreach (var student in group)
    {
        Console.WriteLine(student);
    }
    Console.WriteLine();
}
Console.WriteLine();


// LINQ: 二維資料變成一維資料 (flattening)
// CustomerId, Name, OrderId, ProductName, Quantity, Price, Total

var customerOrders = FakeCustomerOrders(); 

var orders = customerOrders.SelectMany(c => c.Orders.Select(o => new
{
    c.CustomerId,
    c.Name,
    o.OrderId,
    o.ProductName,
    o.Quantity,
    o.Price,
    o.Total
}));

foreach (var order in orders)
{
    Console.WriteLine(
         $"{order.CustomerId, 4}, {order.Name, -8}, {order.OrderId, 4}, {order.ProductName, -25}, {order.Quantity, 4}, {order.Price, 8:N1}, {order.Total, 8:N1}");
}
Console.WriteLine();

return;

List<CustomerOrder> FakeCustomerOrders()
{
    var customerId = 100;

    var customerOrders = new Faker<CustomerOrder>("zh_TW")
        .RuleFor(c => c.CustomerId, f => customerId++)
        .RuleFor(c => c.Name, f => f.Name.FullName())
        .RuleFor(c => c.Orders, (f, c) => FakeOrders(c.CustomerId))
        .Generate(3);
    return customerOrders;
}

List<Order> FakeOrders(int customerId)
{
    var orders = new Faker<Order>("zh_TW")
        .RuleFor(o => o.CustomerId, f => customerId)
        .RuleFor(o => o.OrderId, f => f.Random.Number(1001, 9999))
        .RuleFor(o => o.ProductName, f => f.Commerce.ProductName())
        .RuleFor(o => o.Quantity, f => f.Random.Number(1,3))
        .RuleFor(o => o.Price, f => f.Random.Double(100,1000))
        .Generate(Random.Shared.Next(1,3));
    return orders;
}

List<Student> FakeStudents()
{
    var id = 1001; 
    var students = new Faker<Student>("zh_TW")
        .RuleFor(s => s.Id, f => id++)
        .RuleFor(s => s.Gender, f => f.PickRandom(Enum.GetValues<Gender>()))
        .RuleFor(s => s.Name, (f, u) => f.Name.FullName((Bogus.DataSets.Name.Gender)u.Gender))
        .RuleFor(s => s.Age, f => f.Random.Number(15, 19))
        .RuleFor(s => s.Score, f => f.Random.Double(35, 100))
        .RuleFor(s => s.Grade, (f, u) => u.SetGrade())
        .Generate(10);
    return students;
}
