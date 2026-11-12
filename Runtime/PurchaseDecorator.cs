using System;
using System.Collections.Generic;

namespace com.ktgame.iap.core
{
	public abstract class PurchaseDecorator : IPurchase, IDisposable
	{
		protected IPurchase Purchaser { get; }
		public event Action<PurchaseInitialize> PurchaseInitialized;
		public event Action<PurchaseError> PurchaseFailed;
		public event Action<PurchaseComplete> PurchaseCompleted;
		public event Action<PurchaseProcess> PurchaseProcessing;
		public event Action<PurchaseComplete> ServerPurchaseValid;
		public event Action<PurchaseRestore> RestoreCompleted;
		public virtual IPurchaseValidator LocalValidator => Purchaser.LocalValidator;
		public virtual IPurchaseValidator ServerValidator => Purchaser.ServerValidator;
		public virtual IEnumerable<IProduct> Products => Purchaser.Products;
		public virtual bool IsInitialized => Purchaser.IsInitialized;

		private bool _disposed;

		protected PurchaseDecorator(IPurchase purchaser)
		{
			Purchaser = purchaser ?? throw new ArgumentNullException(nameof(purchaser));
			Purchaser.PurchaseInitialized += PurchaseInitializedHandler;
			Purchaser.PurchaseFailed += PurchaseFailedHandler;
			Purchaser.PurchaseCompleted += PurchaseCompletedHandler;
			Purchaser.PurchaseProcessing += PurchaseProcessingHandler;
			Purchaser.ServerPurchaseValid += ServerPurchaseValidHandler;
			Purchaser.RestoreCompleted += RestoreCompletedHandler;
		}

		protected virtual void PurchaseInitializedHandler(PurchaseInitialize purchaseInitialize)
		{
			PurchaseInitialized?.Invoke(purchaseInitialize);
		}

		protected virtual void PurchaseFailedHandler(PurchaseError purchaseError)
		{
			PurchaseFailed?.Invoke(purchaseError);
		}

		protected virtual void PurchaseProcessingHandler(PurchaseProcess purchaseProcess)
		{
			PurchaseProcessing?.Invoke(purchaseProcess);
		}

		protected virtual void PurchaseCompletedHandler(PurchaseComplete purchaseComplete)
		{
			PurchaseCompleted?.Invoke(purchaseComplete);
		}

		protected virtual void ServerPurchaseValidHandler(PurchaseComplete purchaseComplete)
		{
			ServerPurchaseValid?.Invoke(purchaseComplete);
		}

		protected virtual void RestoreCompletedHandler(PurchaseRestore purchaseRestore)
		{
			RestoreCompleted?.Invoke(purchaseRestore);
		}

		public virtual void InitializePurchasing(IEnumerable<ProductData> productData)
		{
			Purchaser.InitializePurchasing(productData);
		}

		public virtual void Purchase(string productId)
		{
			Purchaser.Purchase(productId);
		}

		public virtual void RestorePurchases()
		{
			Purchaser.RestorePurchases();
		}

		public virtual SubscriptionInfo GetSubscriptionInfo(string productId)
		{
			return Purchaser.GetSubscriptionInfo(productId);
		}
		
		public virtual void Dispose()
		{
			if (_disposed)
			{
				return;
			}

			_disposed = true;
			Purchaser.PurchaseInitialized -= PurchaseInitializedHandler;
			Purchaser.PurchaseFailed -= PurchaseFailedHandler;
			Purchaser.PurchaseCompleted -= PurchaseCompletedHandler;
			Purchaser.PurchaseProcessing -= PurchaseProcessingHandler;
			Purchaser.ServerPurchaseValid -= ServerPurchaseValidHandler;
			Purchaser.RestoreCompleted -= RestoreCompletedHandler;
		}
	}
}
