namespace com.ktgame.iap.core
{
	public interface IPurchaseValidator
	{
		PurchaseState Validate(string productId, string productType, string receipt);
	}
}
