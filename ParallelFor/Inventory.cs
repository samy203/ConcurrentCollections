using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelFor
{
    public class Inventory
    {
        public List<TShirt> InventoryStock = new List<TShirt> { new TShirt("igeek", "IGeek", 500),
                               new TShirt("bigdata", "Big Data", 600),
                               new TShirt("ilovenode", "I Love Node", 300),
                               new TShirt("kcdc", "kcdc", 400),
                               new TShirt("docker", "Docker", 350),
                               new TShirt("qcon", "QCon", 300),
                               new TShirt("ps", "Pluralsight", 60000),
                               new TShirt("pslive", "Pluralsight Live", 60000)
        };

        public void StartRecordingInventory()
        {


            Console.WriteLine("Starting operations");

            var exceptions = new ConcurrentQueue<Exception>();

            // Using Parallel.For to square each number in the array in parallel
            Parallel.ForEach(InventoryStock, (shirt, i) =>
                {
                    try
                    {
                        if (shirt.PricePence == 300)
                        {
                            throw new InvalidOperationException($"{shirt} Minimum price has been updated to be 350");

                        }
                        Console.WriteLine($"{shirt} with index {i} has been sold");
                    }
                    catch (Exception e)
                    {
                        exceptions.Enqueue(e);
                    }
                });

            if (!exceptions.IsEmpty)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
