using System.Globalization;

namespace com.ktgame.iap.core
{
	public class MockupProduct : IProduct
	{
		public string Id { get; }
		public PurchaseType Type { get; }
		public decimal LocalizedPrice { private set; get; }
		public string IsoCurrencyCode { private set; get; }
		public string LocalizedPriceString { private set; get; }
		public string LocalizedTitle { private set; get; }
		public string LocalizedDescription { private set; get; }
		public bool HasReceipt => false;
		public string Receipt => string.Empty;
		public bool IsOwnedNonConsumable => false;
		public bool IsSubscribed => false;

		public MockupProduct(ProductData product)
		{
			Id = product.Id;
			Type = product.Type;
			LocalizedPriceString = product.Price;
			IsoCurrencyCode = "$";
			if (LocalizedPriceString.Contains(IsoCurrencyCode))
			{
				LocalizedPrice = decimal.Parse(LocalizedPriceString.Replace(IsoCurrencyCode, ""), CultureInfo.InvariantCulture);
			}
		}

		public void SetLocalizedTitle(string localizedTitle)
		{
			LocalizedTitle = localizedTitle;
		}

		public void SetLocalizedDescription(string localizedDescription)
		{
			LocalizedDescription = localizedDescription;
		}
	}
}
