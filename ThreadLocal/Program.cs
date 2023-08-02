using ThreadLocal;

ThreadLocal<ShopConfig> shopConfig = new ThreadLocal<ShopConfig>(() =>
{
    return new ShopConfig
    {
        ShopId = Thread.CurrentThread.ManagedThreadId,
        MaxInventoryCapacity = new Random().Next(0, 100)
    };
});

Console.WriteLine("Multi-branch shop with per-thread configuration settings...");

Task[] branches = new Task[5];

for (int i = 0; i < branches.Length; i++)
{
    branches[i] = Task.Factory.StartNew(() =>
    {
        var config = shopConfig.Value;
        Console.WriteLine($"Shop with Id '{config.ShopId}': Max Inventory Capacity: {config.MaxInventoryCapacity} Shirts started operation");

        //Shop in operation
        Thread.Sleep(2000);

        Console.WriteLine($"Shop with Id '{config.ShopId}': Finished operating.");
    });

    branches[i].ContinueWith(task =>
    {
        // Dispose of the thread-local instance after each task completes
        shopConfig.Dispose();
    });
}

Task.WaitAll(branches);

Console.WriteLine("Multi-branch shop workday completed.");


