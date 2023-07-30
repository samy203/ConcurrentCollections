using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BuyAndSellWithLogs
{
	public class LogTradesQueue
	{
		private IProducerConsumerCollection<Trade> _tradesToLog 
			= new ConcurrentQueue<Trade>();
		private readonly StaffRecords _staffLogs;
		private bool _workingDayComplete;
		public LogTradesQueue(StaffRecords staffLogs)
		{
			_staffLogs = staffLogs;
		}
		public void SetNoMoreTrades() => _workingDayComplete = true;
		public void QueueTradeForLogging(Trade trade) => _tradesToLog.TryAdd(trade);

		public void MonitorAndLogTrades()
		{
			while (true)
			{
				Trade nextTrade;
				bool done = _tradesToLog.TryTake(out nextTrade);
				if (done)
				{
					_staffLogs.LogTrade(nextTrade);
					Console.WriteLine(
						$"Processing transaction from {nextTrade.Person.Name}");
				}
				else if (_workingDayComplete)
				{
					Console.WriteLine("No more sales to log - exiting");
					return;
				}
				else
				{
					Console.WriteLine("No transactions available");
					Thread.Sleep(500);
				}
			}
		}
	}
}
