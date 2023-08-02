using ParallelFor;
using System.Collections.Concurrent;


var inventory = new Inventory();

try
{
    inventory.StartRecordingInventory();
}
catch (AggregateException e)
{
    Console.WriteLine("Pending operations");
    foreach (var ex in e.Flatten().InnerExceptions)
    {
        Console.WriteLine(ex.Message);
    }
}
