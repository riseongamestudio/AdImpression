namespace RiseOn.AdImpression.AdMax {
    public static class AdMaxAdapter {
        private const Mediation MEDIATION = Mediation.ApplovinMax;
        private const string CURRENCY = "USD";

        public static AdImpression ToImpression(this MaxSdkBase.AdInfo adInfo) {
            return new(
                source: adInfo.NetworkName
              , unitId: adInfo.AdUnitIdentifier
              , format: adInfo.AdFormat
              , mediation: MEDIATION
              , value: adInfo.Revenue
              , currency: CURRENCY);
        }
    }
}