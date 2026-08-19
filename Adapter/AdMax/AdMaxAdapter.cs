using AppsFlyerSDK;

namespace RiseOn.Analytics.AdMax {
    public static class AdMaxAdapter {
        private const MediationNetwork MEDIATION = MediationNetwork.ApplovinMax;
        private const string           CURRENCY  = "USD";

        public static AdImpression ToImpression(this MaxSdkBase.AdInfo adInfo) => new(
            source: adInfo.NetworkName
          , unitId: adInfo.AdUnitIdentifier
          , format: adInfo.AdFormat
          , mediation: MEDIATION
          , value: adInfo.Revenue
          , currency: CURRENCY);
    }
}