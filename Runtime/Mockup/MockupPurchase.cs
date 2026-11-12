using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace com.ktgame.iap.core
{
	public class MockupPurchase : IPurchase
	{
		public event Action<PurchaseInitialize> PurchaseInitialized;
		public event Action<PurchaseError> PurchaseFailed;
		public event Action<PurchaseComplete> PurchaseCompleted;
		public event Action<PurchaseProcess> PurchaseProcessing;
		public event Action<PurchaseComplete> ServerPurchaseValid;
		public event Action<PurchaseRestore> RestoreCompleted;
		public IPurchaseValidator LocalValidator => _validator;
		public IPurchaseValidator ServerValidator => null;

		public IEnumerable<IProduct> Products => _products;
		public bool IsInitialized { private set; get; }

		private readonly MockupPurchaseOptions _options;
		private readonly IPurchaseValidator _validator;
		private readonly List<MockupProduct> _products;
		private bool _purchasing;

		public MockupPurchase() : this(null)
		{
		}

		public MockupPurchase(MockupPurchaseOptions options)
		{
			_options = options ?? new MockupPurchaseOptions();
			_validator = _options.Validator ?? new MockupPurchaseValidator();
			_products = new List<MockupProduct>();
		}

		public void InitializePurchasing(IEnumerable<ProductData> productData)
		{
			_products.Clear();
			if (productData != null)
			{
				foreach (var product in productData)
				{
					if (string.IsNullOrEmpty(product.Id) || FindProduct(product.Id) != null)
					{
						continue;
					}

					_products.Add(new MockupProduct(product));
				}
			}

			IsInitialized = true;
			PurchaseInitialized?.Invoke(new PurchaseInitialize(InitializationStatus.Ready));
		}

		public void Purchase(string productId)
		{
			PurchaseAsync(productId);
		}

		public void RestorePurchases()
		{
			RestoreAsync();
		}

		public SubscriptionInfo GetSubscriptionInfo(string productId)
		{
			return null;
		}

		private async void PurchaseAsync(string productId)
		{
			var started = false;
			try
			{
				var product = FindProduct(productId);
				if (!IsInitialized)
				{
					Fail(productId, PurchaseStatus.PurchasingUnavailable, "Not initialized");
					return;
				}

				if (product == null)
				{
					Fail(productId, PurchaseStatus.ProductUnavailable, "Unknown product");
					return;
				}

				if (!_options.AllowPurchases)
				{
					Debug.LogError("[MockupPurchase] Mock purchases are disabled in this build. Is the real store (e.g. UNITY_PURCHASE) configured?");
					Fail(productId, PurchaseStatus.PurchasingUnavailable, "Mock purchases disabled");
					return;
				}

				if (_purchasing)
				{
					Fail(productId, PurchaseStatus.ExistingPurchasePending, "Another purchase is in progress");
					return;
				}

				if (product.IsOwnedNonConsumable)
				{
					Fail(productId, PurchaseStatus.DuplicateTransaction, "Already owned");
					return;
				}

				_purchasing = true;
				started = true;
				PurchaseProcessing?.Invoke(new PurchaseProcess(productId));

				await DelayAsync(_options.DelaySeconds);

				var failure = _options.FailureProvider?.Invoke(productId);
				if (failure.HasValue)
				{
					Fail(productId, failure.Value, "Simulated failure");
					return;
				}

				var transactionId = "mock_" + Guid.NewGuid().ToString("N");
				var receipt = CreateReceipt(transactionId);
				if (_validator.Validate(productId, product.Type.ToString(), receipt) != PurchaseState.Purchased)
				{
					Fail(productId, PurchaseStatus.SignatureInvalid, "Validation failed");
					return;
				}

				product.SetPurchased(receipt);
				PurchaseCompleted?.Invoke(new PurchaseComplete(productId, receipt, transactionId));
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}
			finally
			{
				if (started)
				{
					_purchasing = false;
				}
			}
		}

		private async void RestoreAsync()
		{
			try
			{
				if (!IsInitialized)
				{
					RestoreCompleted?.Invoke(new PurchaseRestore(false, "Not initialized"));
					return;
				}

				await DelayAsync(_options.DelaySeconds);

				foreach (var product in _products.ToArray())
				{
					if (product.HasReceipt)
					{
						var transactionId = ReadTransactionId(product.Receipt);
						PurchaseCompleted?.Invoke(new PurchaseComplete(product.Id, product.Receipt, transactionId, true));
					}
				}

				RestoreCompleted?.Invoke(new PurchaseRestore(true));
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}
		}

		private void Fail(string productId, PurchaseStatus status, string message)
		{
			PurchaseFailed?.Invoke(new PurchaseError(productId, status, message));
		}

		private MockupProduct FindProduct(string productId)
		{
			foreach (var product in _products)
			{
				if (product.Id == productId)
				{
					return product;
				}
			}

			return null;
		}
		
		private static async Task DelayAsync(float seconds)
		{
			var end = Time.realtimeSinceStartup + Mathf.Max(0f, seconds);
			do
			{
				await Task.Yield();
			} while (Time.realtimeSinceStartup < end);
		}

		private static string CreateReceipt(string transactionId)
		{
			return "{\"Store\":\"fake\",\"TransactionID\":\"" + transactionId + "\",\"Payload\":\"ThisIsFakeReceiptData\"}";
		}

		private static string ReadTransactionId(string receipt)
		{
			const string key = "\"TransactionID\":\"";
			var start = receipt.IndexOf(key, StringComparison.Ordinal);
			if (start < 0)
			{
				return null;
			}

			start += key.Length;
			var end = receipt.IndexOf('"', start);
			return end > start ? receipt.Substring(start, end - start) : null;
		}
	}
}
