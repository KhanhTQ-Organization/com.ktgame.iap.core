using System.Globalization;
using System.Text;

namespace com.ktgame.iap.core
{
	public class MockupProduct : IProduct
	{
		private const string DefaultCurrencyCode = "USD";

		public string Id { get; }
		public PurchaseType Type { get; }
		public decimal LocalizedPrice { private set; get; }
		public string IsoCurrencyCode { private set; get; }
		public string LocalizedPriceString { private set; get; }
		public string LocalizedTitle { private set; get; }
		public string LocalizedDescription { private set; get; }
		public bool HasReceipt => !string.IsNullOrEmpty(Receipt);
		public string Receipt { private set; get; }
		public bool IsOwnedNonConsumable => Type == PurchaseType.NonConsumable && HasReceipt;
		public bool IsSubscribed => Type == PurchaseType.Subscription && HasReceipt;

		public MockupProduct(ProductData product)
		{
			Id = product.Id;
			Type = product.Type;
			LocalizedPriceString = product.Price ?? string.Empty;
			LocalizedTitle = product.Id;
			LocalizedDescription = string.Empty;
			IsoCurrencyCode = DefaultCurrencyCode;
			LocalizedPrice = ParsePrice(LocalizedPriceString);
			Receipt = string.Empty;
		}

		public void SetLocalizedTitle(string localizedTitle)
		{
			LocalizedTitle = localizedTitle;
		}

		public void SetLocalizedDescription(string localizedDescription)
		{
			LocalizedDescription = localizedDescription;
		}
		
		internal void SetPurchased(string receipt)
		{
			if (Type != PurchaseType.Consumable)
			{
				Receipt = receipt;
			}
		}
		
		internal static decimal ParsePrice(string price)
		{
			if (string.IsNullOrEmpty(price))
			{
				return 0m;
			}

			var builder = new StringBuilder(price.Length);
			foreach (var c in price)
			{
				if (char.IsDigit(c) || c == '.' || c == ',')
				{
					builder.Append(c);
				}
			}
			
			var number = builder.ToString();
			var decimalSeparator = number.LastIndexOf(',') > number.LastIndexOf('.') ? ',' : '.';
			var thousandsSeparator = decimalSeparator == ',' ? "." : ",";
			number = number.Replace(thousandsSeparator, string.Empty).Replace(decimalSeparator, '.');

			return decimal.TryParse(number, NumberStyles.Number, CultureInfo.InvariantCulture, out var result) ? result : 0m;
		}
	}
}
