using RiseOn.NativeAdMob;

namespace RiseOn.AdImpression.NativeAdMob {
    public static class NativeAdMobAdapter {
        private const Mediation MEDIATION = Mediation.GoogleAdMob;

        public static AdImpression ToImpression(this AdInfo adInfo) => new(
            source: adInfo.Source
          , unitId: adInfo.UnitId
          , format: adInfo.Format
          , mediation: MEDIATION
          , value: adInfo.Value
          , currency: adInfo.Currency);
    }
}