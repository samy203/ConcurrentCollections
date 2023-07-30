using BuyAndSell;

StockController controller = new StockController();
TimeSpan workDay = new TimeSpan(0, 0, 0, 0, 500);

Task task1 = Task.Run(
    () => new SalesPerson("Samy").Work(workDay, controller));
Task task2 = Task.Run(
    () => new SalesPerson("Mekawy").Work(workDay, controller));
Task task3 = Task.Run(
    () => new SalesPerson("Tag").Work(workDay, controller));

Task.WaitAll(task1, task2, task3);

controller.DisplayStock();