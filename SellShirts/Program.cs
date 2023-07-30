using SellShirts;

var stock = new List<TShirt> { new TShirt("igeek", "IGeek", 500),
                               new TShirt("bigdata", "Big Data", 600),
                               new TShirt("ilovenode", "I Love Node", 750),
                               new TShirt("kcdc", "kcdc", 400),
                               new TShirt("docker", "Docker", 350),
                               new TShirt("qcon", "QCon", 300),
                               new TShirt("ps", "Pluralsight", 60000),
                               new TShirt("pslive", "Pluralsight Live", 60000)
                             };

StockController controller = new StockController(stock);
TimeSpan workDay = new TimeSpan(0, 0, 0, 0, 500);

Task task1 = Task.Run(() => new SalesPerson("Samy").Work(workDay, controller));
Task task2 = Task.Run(() => new SalesPerson("Mekawy").Work(workDay, controller));
Task task3 = Task.Run(() => new SalesPerson("Tag").Work(workDay, controller));
Task.WaitAll(task1, task2, task3);
controller.DisplayStock();