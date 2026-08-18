using AppsFlyerSDK;

namespace RiseOn.Analytics.AdMax {
    public static class AdMaxAdapter {
        private const string CURRENCY = "USD";

        private static AdFormat ConvertFormat(string format) => format switch {
            "APPOPEN"        => AdFormat.APP_OPEN
          , "BANNER"         => AdFormat.BANNER
          , "LEADER"         => AdFormat.LEADER
          , "MREC"           => AdFormat.MREC
          , "INTER"          => AdFormat.INTERSTITIAL
          , "REWARDED"       => AdFormat.REWARDED
          , "REWARDED_INTER" => AdFormat.REWARDED_INTERSTITIAL
          , "NATIVE"         => AdFormat.NATIVE
          , _                => AdFormat.UNKNOWN
        };

        public static AdImpression ToImpression(this MaxSdkBase.AdInfo adInfo) => new(
            source: adInfo.NetworkName
          , unitId: adInfo.AdUnitIdentifier
          , format: ConvertFormat(adInfo.AdFormat)
          , mediation: MediationNetwork.ApplovinMax
          , value: adInfo.Revenue
          , currency: CURRENCY);
    }
}