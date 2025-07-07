 using System;
using System.Collections.Generic;

namespace com.ktgame.iap.core
{
    public class MockupPurchase : IPurchase
    {
        public event Action<PurchaseInitialize> PurchaseInitialized;
        public event Action<PurchaseError> PurchaseFailed;
        public event Action<PurchaseComplete> PurchaseCompleted;
        public event Action<PurchaseProcess> PurchaseProcessing;
        public event Action<PurchaseComplete> ServerPurchaseValid;
        public IPurchaseValidator LocalValidator { private set; get; }
        public IPurchaseValidator ServerValidator { private set; get; }

        public IEnumerable<IProduct> Products => _products;
        public bool IsInitialized { private set; get; }

        private readonly IPurchaseValidator _validator;
        private readonly List<IProduct> _products;

        public MockupPurchase()
        {
            _validator = new MockupPurchaseValidator();
            _products = new List<IProduct>();
        }

        public void InitializePurchasing(IEnumerable<ProductData> productData)
        {
            foreach (var product in productData)
            {
                _products.Add(new MockupProduct(product));
            }

            IsInitialized = true;
            LocalValidator = null;
            ServerValidator = null;
            PurchaseInitialized?.Invoke(new PurchaseInitialize(InitializationStatus.Ready));
        }

        public void Purchase(string productId)
        {
            if (IsValidated(productId))
            {
                var purchase = new PurchaseComplete(productId, string.Empty);
                PurchaseCompleted?.Invoke(purchase);
            }
        }

        public void RestorePurchases() { }

        public SubscriptionInfo GetSubscriptionInfo(string productId)
        {
            return null;
        }

        private bool IsValidated(string productId)
        {
            return _validator.Validate(productId, string.Empty, string.Empty) == PurchaseState.Purchased;
        }
    }
}