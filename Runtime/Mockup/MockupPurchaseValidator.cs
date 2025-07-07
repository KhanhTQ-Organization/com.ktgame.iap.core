namespace com.ktgame.iap.core
{
	public class MockupPurchaseValidator : IPurchaseValidator
	{
		public PurchaseState Validate(string productId, string productType, string receipt)
		{
			return PurchaseState.Purchased;
		}
	}
}
