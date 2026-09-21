using AppsFlyerSDK;
using UnityEngine;

namespace RiseOn.AdImpression.AppsFlyer {
    public static class AppsFlyerAdapter {
        public static AFAdRevenueData ToAdRevenueData(this AdImpression impression) => new(
            monetization: impression.Source
          , mediation: impression.Mediation.ToAppsFlyerMediation()
          , currency: impression.Currency
          , revenue: impression.Value);

        public static MediationNetwork ToAppsFlyerMediation(this Mediation mediation) => mediation switch {
            Mediation.GoogleAdMob => MediationNetwork.GoogleAdMob
          , Mediation.IronSource => MediationNetwork.IronSource
          , Mediation.ApplovinMax => MediationNetwork.ApplovinMax
          , Mediation.Fyber => MediationNetwork.Fyber
          , Mediation.Appodeal => MediationNetwork.Appodeal
          , Mediation.Admost => MediationNetwork.Admost
          , Mediation.Topon => MediationNetwork.Topon
          , Mediation.Tradplus => MediationNetwork.Tradplus
          , Mediation.Yandex => MediationNetwork.Yandex
          , Mediation.ChartBoost => MediationNetwork.ChartBoost
          , Mediation.Unity => MediationNetwork.Unity
          , Mediation.ToponPte => MediationNetwork.ToponPte
          , Mediation.Custom => MediationNetwork.Custom
          , Mediation.DirectMonetization => MediationNetwork.DirectMonetization
          , _ => UnmappedMediation(mediation)
        };

        private static MediationNetwork UnmappedMediation(Mediation mediation) {
            Debug.LogError($"{nameof(AppsFlyerAdapter)}: no AppsFlyer mapping for {mediation}");
            return MediationNetwork.Custom;
        }
    }
}