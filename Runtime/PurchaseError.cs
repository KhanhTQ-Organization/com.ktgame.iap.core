namespace com.ktgame.iap.core
{
	[System.Serializable]
	public struct PurchaseError
	{
		public string ProductId { get; }
		public PurchaseStatus ErrorType { get; }
		
		public string Message { get; }

		public PurchaseError(string productId, PurchaseStatus errorType)
			: this(productId, errorType, null)
		{
		}

		public PurchaseError(string productId, PurchaseStatus errorType, string message)
		{
			ProductId = productId;
			ErrorType = errorType;
			Message = message;
		}
	}
}
