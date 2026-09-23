# Adapter

[← RiseOn.AdImpression](../../README.md)

Adapter chuyển dữ liệu doanh thu của SDK quảng cáo thành
[`AdImpression`](../README.md) (đầu vào), hoặc chuyển `AdImpression` sang dạng
dịch vụ đo lường cần (đầu ra).

## Tự bật theo SDK

Mỗi adapter là một assembly riêng. Asmdef của nó dùng `versionDefines` để sinh
symbol khi project có package tương ứng, và `defineConstraints` để chỉ biên dịch
khi có symbol đó. Project thiếu SDK thì adapter không được biên dịch và không
báo lỗi.

| Adapter | Chiều | Bật khi có package | Symbol | Hàm |
|---|---|---|---|---|
| `RiseOn.AdImpression.AdMax` | MAX → AdImpression | `com.applovin.mediation.ads` | `HAS_ADMAX` | `adInfo.ToImpression()` với `MaxSdkBase.AdInfo` |
| `RiseOn.AdImpression.AdMob` | AdMob → AdImpression | `com.google.ads.mobile` | `HAS_ADMOB` | `adValue.ToImpression(ad)` với `ad` là `AppOpenAd`, `BannerView`, `InterstitialAd`, `RewardedAd`, `RewardedInterstitialAd`, `NativeOverlayAd` |
| `RiseOn.AdImpression.NativeAdMob` | NativeAdMob → AdImpression | `com.riseon.nativeadmob` | `HAS_NATIVEADMOB` | `adInfo.ToImpression()` với `RiseOn.NativeAdMob.AdInfo` |
| `RiseOn.AdImpression.AppsFlyer` | AdImpression → AppsFlyer | `appsflyer-unity-plugin` | `HAS_APPSFLYER` | `impression.ToAdRevenueData()`, `mediation.ToAppsFlyerMediation()` |

Unity chỉ nhận ra SDK cài qua UPM. SDK import bằng `.unitypackage`, hoặc AppsFlyer
bản OpenUPM (`com.appsflyer.unity`, khác tên package), thì thêm tay symbol tương
ứng vào *Player Settings → Scripting Define Symbols* để bật adapter.

## Ví dụ: gửi doanh thu MAX lên AppsFlyer

```csharp
using System.Collections.Generic;
using AppsFlyerSDK;
using RiseOn.AdImpression.AdMax;
using RiseOn.AdImpression.AppsFlyer;

MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += (adUnitId, adInfo) => {
    var impression = adInfo.ToImpression();
    AppsFlyer.logAdRevenue(
        impression.ToAdRevenueData()
      , new Dictionary<string, string> {
            { AdRevenueScheme.AD_UNIT, impression.UnitId }
          , { AdRevenueScheme.AD_TYPE, impression.Format }
        });
};
```

## Firebase

Chưa có adapter riêng. Event chuẩn `ad_impression` ánh xạ thẳng từ các trường:
`ad_platform` ← `Mediation` (đổi sang chuỗi), `ad_source` ← `Source`,
`ad_format` ← `Format`, `ad_unit_name` ← `UnitId`, `value` ← `Value`,
`currency` ← `Currency`.

## Lưu ý

- Adapter AppsFlyer gặp giá trị `Mediation` chưa có ánh xạ thì log lỗi và gửi
  `MediationNetwork.Custom`, không làm mất event.
- Bên trong namespace của một adapter, tên đoạn cuối che mất type cùng tên của
  SDK: trong `RiseOn.AdImpression.AppsFlyer`, chữ `AppsFlyer` là namespace đó,
  nên gọi class của SDK phải viết đủ `AppsFlyerSDK.AppsFlyer`.
