using System.Collections.Concurrent;

namespace BuyAndSellWithBlockingCollection
{
    public class LogTradesQueue
	{
		private BlockingCollection<Trade> _tradesToLog 
			= new BlockingCollection<Trade>();
		private readonly StaffRecords _staffLogs;
		public LogTradesQueue(StaffRecords staffLogs)
		{
			_staffLogs = staffLogs;
		}
		public void SetNoMoreTrades() => _tradesToLog.CompleteAdding();
		public void QueueTradeForLogging(Trade trade) => _tradesToLog.TryAdd(trade);

		public void MonitorAndLogTrades()
		{
			while (true)
			{
				try
				{
					Trade nextTrade = _tradesToLog.Take();
					_staffLogs.LogTrade(nextTrade);
					Console.WriteLine(
						$"Processing transaction from {nextTrade.Person.Name}");
				}
				catch (InvalidOperationException ex)
				{
					Console.WriteLine(ex.Message);
					return;
				}
			}
		}
	}
}
