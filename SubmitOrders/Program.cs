
var queue = new Queue<string>();
PlaceOrders(queue, "Samy", 5);
PlaceOrders(queue, "Mekawy", 7);
PlaceOrders(queue, "Tag", 2);

foreach (string order in queue)
    Console.WriteLine("ORDER: " + order);


static void PlaceOrders(Queue<string> orders, string customerName, int ordersCount)
{
    for (int i = 1; i <= ordersCount; i++)
    {
        Thread.Sleep(2);
        string orderName = $"{customerName} wants t-shirt {i}";
        orders.Enqueue(orderName);
    }
}