using System;
using UnityEngine;

namespace com.ktgame.iap.core
{
	[Serializable]
	public struct ProductData
	{
		[SerializeField] private string _id;
		[SerializeField] private PurchaseType _type;
		[SerializeField] private string _price;

		public string Id
		{
			get => _id;
			set => _id = value;
		}

		public PurchaseType Type
		{
			get => _type;
			set => _type = value;
		}

		public string Price
		{
			get => _price;
			set => _price = value;
		}

		public ProductData(string id, PurchaseType type, string price)
		{
			this._id = id;
			this._type = type;
			this._price = price;
		}
	}
}
