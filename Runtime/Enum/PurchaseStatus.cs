namespace com.ktgame.iap.core
{
    public enum PurchaseStatus
    {
        /// <summary>
        /// Purchasing may be disabled in security settings, or the store is not initialized.
        /// </summary>
        PurchasingUnavailable = 0,

        /// <summary>
        /// Another purchase is already in progress.
        /// </summary>
        ExistingPurchasePending = 1,

        /// <summary>
        /// The product was reported unavailable by the purchasing system (or does not exist).
        /// </summary>
        ProductUnavailable = 2,

        /// <summary>
        /// Signature validation of the purchase's receipt failed.
        /// </summary>
        SignatureInvalid = 3,

        /// <summary>
        /// The user opted to cancel rather than proceed with the purchase.
        /// This is not specified on platforms that do not distinguish cancellation from other failure (Amazon, Microsoft).
        /// </summary>
        UserCancelled = 4,

        /// <summary>
        /// There was a problem with the payment. This is unique to Apple platforms.
        /// </summary>
        PaymentDeclined = 5,

        /// <summary>
        /// The transaction has already been completed successfully.
        /// This error can occur on Apple platforms if the transaction is finished successfully while the user is logged out of the app store, using a receipt generated while the user was logged in.
        /// </summary>
        DuplicateTransaction = 6,

        /// <summary>
        /// A catch all for remaining purchase problems.
        /// </summary>
        Unknown = 7,

        Unauthorized = 8,

        InvalidRequest = 9,

        InvalidAppId = 10,

        ReceiptValidationFailed = 11,

        TransactionNotMatch = 12,

        UnsupportedStore = 13,

        UnexpectedResponse = 14,

        RequestTimeout = 15,
    }
}
