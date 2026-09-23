# RiseOn.AdImpression

Gom doanh thu của từng lần hiển thị quảng cáo (ad impression) từ các SDK quảng
cáo về một struct chung `AdImpression`, rồi chuyển sang dạng mà dịch vụ đo lường
cần. Pack không tự gửi gì đi: game lấy `AdImpression` từ adapter đầu vào rồi tự
quyết gửi lên đâu (AppsFlyer, Firebase, ...).

Package `com.riseon.adimpression`, namespace `RiseOn.AdImpression`.

## Mục lục

- [Yêu cầu](#yêu-cầu)
- [Cài đặt](#cài-đặt)
- [Tổng quan](#tổng-quan)
- [Hướng dẫn nhanh](#hướng-dẫn-nhanh)
- [Thành phần](#thành-phần)
- [Lịch sử thay đổi](#lịch-sử-thay-đổi)
- [Giấy phép](#giấy-phép)

## Yêu cầu

| Phụ thuộc | Cách có | Dùng cho |
|---|---|---|
| Unity 6000.3 | | Bản đang dùng để phát triển |
| AppLovin MAX (`com.applovin.mediation.ads`) | Tuỳ chọn | Adapter `AdMax` |
| Google Mobile Ads (`com.google.ads.mobile`) | Tuỳ chọn | Adapter `AdMob` |
| NativeAdMob (`com.riseon.nativeadmob`) | Tuỳ chọn | Adapter `NativeAdMob` |
| AppsFlyer (`appsflyer-unity-plugin`) | Tuỳ chọn | Adapter `AppsFlyer` |

Phần core không phụ thuộc gì. Mỗi adapter chỉ được biên dịch khi project có SDK
tương ứng; SDK cài không qua UPM thì thêm define tay, xem
[Adapter](Runtime/Adapters/README.md#tự-bật-theo-sdk).

## Cài đặt

**OpenUPM** (khuyên dùng): thêm registry OpenUPM với scope `com.riseon` vào
`Packages/manifest.json`, rồi thêm package:

```json
{
  "scopedRegistries": [
    {
      "name": "package.openupm.com",
      "url": "https://package.openupm.com",
      "scopes": ["com.riseon"]
    }
  ],
  "dependencies": {
    "com.riseon.adimpression": "1.0.2"
  }
}
```

**Git URL**: *Package Manager → + → Add package from git URL*:

```
https://github.com/riseongamestudio/AdImpression.git#v1.0.2
```

## Tổng quan

```
callback doanh thu của SDK ──► adapter đầu vào ──► AdImpression ──► adapter đầu ra / game tự gửi
   (MAX, AdMob, NativeAdMob)      .ToImpression()                      (AppsFlyer, Firebase...)
```

| Assembly | Việc |
|---|---|
| `RiseOn.AdImpression` | Struct `AdImpression` (nguồn, ad unit, loại ad, mediation, doanh thu, tiền tệ) và enum `Mediation` |
| `RiseOn.AdImpression.AdMax`, `.AdMob`, `.NativeAdMob` | Adapter đầu vào: dữ liệu doanh thu của SDK → `AdImpression` |
| `RiseOn.AdImpression.AppsFlyer` | Adapter đầu ra: `AdImpression` → `AFAdRevenueData` của AppsFlyer |

Adapter chỉ được biên dịch khi project có SDK tương ứng, nên cài pack vào project
thiếu SDK nào cũng không lỗi.

## Hướng dẫn nhanh

Lấy `AdImpression` trong callback doanh thu của SDK, rồi gửi đi. Ví dụ MAX sang
AppsFlyer:

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

AdMob làm tương tự với `adValue.ToImpression(ad)` trong `OnAdPaid`. SDK cài không
qua UPM (vd. `.unitypackage`) thì phải thêm tay define symbol để bật adapter:
xem [Adapter](Runtime/Adapters/README.md#tự-bật-theo-sdk).

## Thành phần

| Thành phần | Việc | Chi tiết |
|---|---|---|
| Dữ liệu | `AdImpression`, `Mediation` | [Runtime](Runtime/README.md) |
| Adapter | Bật theo SDK, hàm chuyển đổi, ánh xạ sang Firebase | [Runtime/Adapters](Runtime/Adapters/README.md) |

## Lịch sử thay đổi

Xem [CHANGELOG.md](CHANGELOG.md).

## Giấy phép

MIT, xem [LICENSE.md](LICENSE.md).
