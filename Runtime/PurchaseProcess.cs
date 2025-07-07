namespace com.ktgame.iap.core
{
	[System.Serializable]
	public struct PurchaseProcess
	{
		public string ProductId { get; }

		public PurchaseProcess(string productId)
		{
			ProductId = productId;
		}
	}
}