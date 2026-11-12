namespace com.ktgame.iap.core
{
	[System.Serializable]
	public struct PurchaseComplete
	{
		public string ProductId { get; }
		
		public string Receipts { get; }
		
		public string TransactionId { get; }
		
		public bool IsRestored { get; }

		public PurchaseComplete(string productId, string receipts)
			: this(productId, receipts, null, false)
		{
		}

		public PurchaseComplete(string productId, string receipts, string transactionId, bool isRestored = false)
		{
			ProductId = productId;
			Receipts = receipts;
			TransactionId = transactionId;
			IsRestored = isRestored;
		}
	}
}
