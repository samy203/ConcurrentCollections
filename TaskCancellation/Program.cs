// Create a cancellation token source
var cancellationTokenSource = new CancellationTokenSource();
var cancellationToken = cancellationTokenSource.Token;

var operation = Task.Run(() => StartSelllingShirts(cancellationToken));

//simulate a working day of 2000ms
await Task.Delay(2000);

cancellationTokenSource.Cancel();


try
{
    await operation;
}
catch (OperationCanceledException)
{
    Console.WriteLine("Shop got emergency closed.");
}


static async void StartSelllingShirts(CancellationToken cancellationToken)
{
    try
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            // Simulate some work
            Console.WriteLine("shop is selling...");
            //each sell operation is 100ms
            await Task.Delay(100);
        }
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("exception happened.");
        throw;
    }
}
