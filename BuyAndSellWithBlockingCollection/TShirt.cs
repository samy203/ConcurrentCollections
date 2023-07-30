﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BuyAndSellWithBlockingCollection
{
	public class TShirt
	{
		public string Code { get; }
		public string Name { get; }
		public int PricePence { get; }

		public TShirt(string code, string name, int pricePence)
		{
			Code = code;
			Name = name;
			PricePence = pricePence;
		}

		public override string ToString() 
			=> $"{Name} ({DisplayPrice(PricePence)})";

		private string DisplayPrice(int pricePence)
			 => $"${pricePence/100}.{pricePence%100:00}";
	}
}
