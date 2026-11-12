using System;

namespace com.ktgame.iap.core
{
	public class MockupPurchaseOptions
	{
		public float DelaySeconds { get; set; } = 0.3f;
		public Func<string, PurchaseStatus?> FailureProvider { get; set; }
		public IPurchaseValidator Validator { get; set; }
		public bool AllowPurchases { get; set; } = DefaultAllowPurchases;

		public static bool DefaultAllowPurchases
		{
			get
			{
#if UNITY_EDITOR || DEVELOPMENT
				return true;
#else
				return false;
#endif
			}
		}
	}
}
