using System;
using UnityEngine;

namespace com.ktgame.iap.core
{
	[Serializable]
	public struct ProductData
	{
		[SerializeField] private string id;
		[SerializeField] private PurchaseType type;
		[SerializeField] private string price;

		public string Id
		{
			get => id;
			set => id = value;
		}

		public PurchaseType Type
		{
			get => type;
			set => type = value;
		}

		public string Price
		{
			get => price;
			set => price = value;
		}

		public ProductData(string id, PurchaseType type, string price)
		{
			this.id = id;
			this.type = type;
			this.price = price;
		}
	}
}
