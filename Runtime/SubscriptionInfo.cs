using System;

namespace com.ktgame.iap.core
{
    public class SubscriptionInfo
    {
        public bool IsSubscribed { get; private set; }
        public bool IsExpired { get; private set; }
        public bool IsCancelled { get; private set; }
        public bool IsFreeTrial { get; private set; }
        public bool IsAutoRenewing { get; private set; }
        public bool IsIntroductoryPricePeriod { get; private set; }
        public DateTime PurchaseDate { get; private set; }
        public DateTime SubscriptionExpireDate { get; private set; }
        public DateTime SubscriptionCancelDate { get; private set; }
        public TimeSpan RemainedTime { get; private set; }
        public string IntroductoryPrice { get; private set; }
        public TimeSpan IntroductoryPricePeriod { get; private set; }
        public long IntroductoryPriceCycles { get; private set; }
        public TimeSpan FreeTrialPeriod { get; private set; }
        public TimeSpan SubscriptionPeriod { get; private set; }

        public SubscriptionInfo(bool isSubscribed, bool isExpired, bool isCancelled, bool isFreeTrial, bool isAutoRenewing, bool isIntroductoryPricePeriod,
            DateTime purchaseDate, DateTime subscriptionExpireDate, DateTime subscriptionCancelDate, TimeSpan remainedTime, string introductoryPrice,
            TimeSpan introductoryPricePeriod, long introductoryPriceCycles, TimeSpan freeTrialPeriod, TimeSpan subscriptionPeriod)
        {
            IsSubscribed = isSubscribed;
            IsExpired = isExpired;
            IsCancelled = isCancelled;
            IsFreeTrial = isFreeTrial;
            IsAutoRenewing = isAutoRenewing;
            IsIntroductoryPricePeriod = isIntroductoryPricePeriod;
            PurchaseDate = purchaseDate;
            SubscriptionExpireDate = subscriptionExpireDate;
            SubscriptionCancelDate = subscriptionCancelDate;
            RemainedTime = remainedTime;
            IntroductoryPrice = introductoryPrice;
            IntroductoryPricePeriod = introductoryPricePeriod;
            IntroductoryPriceCycles = introductoryPriceCycles;
            FreeTrialPeriod = freeTrialPeriod;
            SubscriptionPeriod = subscriptionPeriod;
        }
    }
}