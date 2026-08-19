using RiseOn.NativeAdMob;
using AppsFlyerSDK;

namespace RiseOn.Analytics.NativeAdMob {
    public static class NativeAdMobAdapter {
        private const MediationNetwork MEDIATION = MediationNetwork.GoogleAdMob;

        public static AdImpression ToImpression(this AdInfo adInfo) => new(
            source: adInfo.Source
          , unitId: adInfo.UnitId
          , format: adInfo.Format
          , mediation: MEDIATION
          , value: adInfo.Value
          , currency: adInfo.Currency);
    }
}