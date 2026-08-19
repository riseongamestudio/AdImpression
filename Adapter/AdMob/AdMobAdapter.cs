using GoogleMobileAds.Api;
using AppsFlyerSDK;

namespace RiseOn.Analytics.AdMob {
    public static class AdMobAdapter {
        private const MediationNetwork MEDIATION        = MediationNetwork.GoogleAdMob;
        private const double           VALUE_MULTIPLIER = 1e-6;

        private readonly struct AdInfo {
            public readonly string Source;
            public readonly string UnitId;
            public readonly string Format;

            public AdInfo(
                string format
              , ResponseInfo responseInfo) {
                var adapterInfo = responseInfo.GetLoadedAdapterResponseInfo();

                Source = adapterInfo.AdSourceName;
                UnitId = adapterInfo.AdSourceId;
                Format = format;
            }
        }

        private static AdImpression ToImpression(this AdValue adValue, AdInfo adInfo) => new(
            source: adInfo.Source
          , unitId: adInfo.UnitId
          , format: adInfo.Format
          , mediation: MEDIATION
          , value: adValue.Value * VALUE_MULTIPLIER
          , currency: adValue.CurrencyCode);

        public static AdImpression ToImpression(this AdValue adValue, AppOpenAd ad) => adValue.ToImpression(new AdInfo(
            "APP_OPEN"
          , ad.GetResponseInfo()));

        public static AdImpression ToImpression(this AdValue adValue, BannerView ad) => adValue.ToImpression(new AdInfo(
            "BANNER"
          , ad.GetResponseInfo()));

        public static AdImpression ToImpression(this AdValue adValue, InterstitialAd ad) => adValue.ToImpression(new AdInfo(
            "INTERSTITIAL"
          , ad.GetResponseInfo()));

        public static AdImpression ToImpression(this AdValue adValue, RewardedAd ad) => adValue.ToImpression(new AdInfo(
            "REWARDED"
          , ad.GetResponseInfo()));

        public static AdImpression ToImpression(this AdValue adValue, RewardedInterstitialAd ad) => adValue.ToImpression(new AdInfo(
            "REWARDED_INTERSTITIAL"
          , ad.GetResponseInfo()));

        public static AdImpression ToImpression(this AdValue adValue, NativeOverlayAd ad) => adValue.ToImpression(new AdInfo(
            "NATIVE_OVERLAY"
          , ad.GetResponseInfo()));
    }
}