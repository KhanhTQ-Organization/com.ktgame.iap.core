namespace com.ktgame.iap.core
{
	[System.Serializable]
	public struct PurchaseRestore
	{
		public bool IsSuccess { get; }
		
		public string Message { get; }

		public PurchaseRestore(bool isSuccess, string message = null)
		{
			IsSuccess = isSuccess;
			Message = message;
		}
	}
}
