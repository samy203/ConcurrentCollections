
var ordersQueue = new Queue<string>();
PlaceOrders(ordersQueue, "Samy", 5);
PlaceOrders(ordersQueue, "Mekawy", 7);
PlaceOrders(ordersQueue, "Tag", 2);

foreach (string order in ordersQueue)
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