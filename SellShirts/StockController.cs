using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellShirts
{
	public enum SelectResult { Success, NoStockLeft, ChosenShirtSold }
	public class StockController
	{
		private ConcurrentDictionary<string, TShirt> _stock;

		public StockController(IEnumerable<TShirt> shirts)
		{
			_stock = new ConcurrentDictionary<string, TShirt>
				(shirts.ToDictionary(x => x.Code));
		}
		public bool Sell(string code)
		{
			return _stock.TryRemove(code, out TShirt shirtRemoved);
		}
		public (SelectResult Result, TShirt Shirt) SelectRandomShirt()
		{
			var keys = _stock.Keys.ToList();
			if (keys.Count == 0)
				return (SelectResult.NoStockLeft, null);    // all shirts sold

			Thread.Sleep(Rnd.NextInt(10));
			string selectedCode = keys[Rnd.NextInt(keys.Count)];
			bool found = _stock.TryGetValue(selectedCode, out TShirt shirt);
			if (found)
				return (SelectResult.Success, shirt);
			else
				return (SelectResult.ChosenShirtSold, null);
			//			return _stock[selectedCode];
		}
		public void DisplayStock()
		{
			Console.WriteLine($"\r\n{_stock.Count} items left in stock:");
			foreach (TShirt shirt in _stock.Values)
				Console.WriteLine(shirt);
		}
	}
}
