# RiseOn.AdImpression

Gom doanh thu của từng lần hiển thị quảng cáo (ad impression) từ các SDK quảng
cáo về một struct chung `AdImpression`, rồi chuyển sang dạng mà dịch vụ đo lường
cần. Pack không tự gửi gì đi: game lấy `AdImpression` từ adapter đầu vào rồi tự
quyết gửi lên đâu (AppsFlyer, Firebase, ...).

## Cài đặt

Qua OpenUPM: thêm scope `com.riseon` vào registry `https://package.openupm.com`
trong `Packages/manifest.json`, rồi:

```json
"com.riseon.adimpression": "1.0.0"
```

Hoặc qua git:

```json
"com.riseon.adimpression": "https://github.com/riseongamestudio/AdImpression.git#v1.0.0"
```

Pack không khai phụ thuộc SDK nào; mỗi adapter tự bật khi project có SDK tương
ứng (mục Adapter).

## Dữ liệu

`AdImpression` nằm trong namespace `RiseOn.AdImpression`:

| Trường | Ý nghĩa |
|---|---|
| `Mediation` | Nền tảng mediation chạy đấu giá: `Mediation.ApplovinMax`, `Mediation.GoogleAdMob`, ... |
| `Source` | Mạng thật sự trả ad (monetization network). MAX: tên mạng; AdMob: tên nguồn quảng cáo; NativeAdMob: tên class adapter mediation |
| `UnitId` | Ad unit (MAX, NativeAdMob) hoặc ad source id (AdMob) |
| `Format` | Loại ad |
| `Value` | Doanh thu của lần hiển thị, đã đổi về đơn vị tiền |
| `Currency` | Mã tiền tệ ISO 4217 |

`Mediation` có cùng danh sách giá trị với `MediationNetwork` của AppsFlyer.

## Adapter

Mỗi adapter là một assembly riêng. Asmdef của nó dùng `versionDefines` để sinh
symbol khi project có package tương ứng, và `defineConstraints` để chỉ compile khi
có symbol đó. Project thiếu SDK thì adapter không được compile và không báo lỗi.

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

Firebase chưa có adapter riêng; event chuẩn `ad_impression` ánh xạ thẳng từ các
trường: `ad_platform` ← `Mediation` (đổi sang chuỗi), `ad_source` ← `Source`,
`ad_format` ← `Format`, `ad_unit_name` ← `UnitId`, `value` ← `Value`,
`currency` ← `Currency`.

## Lưu ý

- Namespace `RiseOn.AdImpression` trùng tên với struct `AdImpression`. Code nằm
  trong một namespace `RiseOn.*` khác mà viết `AdImpression` sẽ bị C# hiểu là
  namespace (lỗi CS0118); khi đó viết `AdImpression.AdImpression`.
- Adapter AppsFlyer gặp giá trị `Mediation` chưa có ánh xạ thì log lỗi và gửi
  `MediationNetwork.Custom`, không làm mất event.

## Giấy phép

MIT, xem [LICENSE](LICENSE).
