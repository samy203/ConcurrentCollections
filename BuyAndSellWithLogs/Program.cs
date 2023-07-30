using BuyAndSellWithLogs;

StockController controller = new StockController();
TimeSpan workDay = new TimeSpan(0, 0, 0, 0, 500);
StaffRecords staffLogs = new StaffRecords();
LogTradesQueue tradesQueue = new LogTradesQueue(staffLogs);

SalesPerson[] staff =
{
                new SalesPerson("Samy"),
                new SalesPerson("Mekawy"),
                new SalesPerson("Tag"),
            };
List<Task> salesTasks = new List<Task>();

foreach (SalesPerson person in staff)
{
    salesTasks.Add(
        Task.Run(() => person.Work(workDay, controller, tradesQueue)));
}

Task[] loggingTasks =
{
                Task.Run(() => tradesQueue.MonitorAndLogTrades()),
                Task.Run(() => tradesQueue.MonitorAndLogTrades())
            };

Task.WaitAll(salesTasks.ToArray());
tradesQueue.SetNoMoreTrades();
Task.WaitAll(loggingTasks);

controller.DisplayStock();
staffLogs.DisplayCommissions(staff);