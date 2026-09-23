# Dữ liệu impression

[← RiseOn.AdImpression](../README.md)

Assembly core `RiseOn.AdImpression`: struct `AdImpression` và enum `Mediation`.
Không phụ thuộc SDK nào.

## AdImpression

Một lần hiển thị quảng cáo có doanh thu, gom về dạng chung cho mọi SDK:

| Trường | Ý nghĩa |
|---|---|
| `Mediation` | Nền tảng mediation chạy đấu giá: `Mediation.ApplovinMax`, `Mediation.GoogleAdMob`, ... |
| `Source` | Mạng thật sự trả ad (monetization network). MAX: tên mạng; AdMob: tên nguồn quảng cáo; NativeAdMob: tên class adapter mediation |
| `UnitId` | Ad unit (MAX, NativeAdMob) hoặc ad source id (AdMob) |
| `Format` | Loại ad |
| `Value` | Doanh thu của lần hiển thị, đã đổi về đơn vị tiền |
| `Currency` | Mã tiền tệ ISO 4217 |

Thường không tự tạo mà lấy từ [adapter đầu vào](Adapters/README.md). SDK chưa có
adapter thì dựng bằng constructor:

```csharp
var impression = new AdImpression(
    source: networkName, unitId: adUnitId, format: "banner",
    mediation: Mediation.Custom, value: revenue, currency: "USD");
```

## Mediation

Cùng danh sách giá trị với `MediationNetwork` của AppsFlyer: `GoogleAdMob`,
`IronSource`, `ApplovinMax`, `Fyber`, `Appodeal`, `Admost`, `Topon`, `Tradplus`,
`Yandex`, `ChartBoost`, `Unity`, `ToponPte`, `Custom`, `DirectMonetization`.

## Lưu ý

Namespace `RiseOn.AdImpression` trùng tên với struct `AdImpression`. Code nằm
trong một namespace `RiseOn.*` khác mà viết `AdImpression` sẽ bị C# hiểu là
namespace (lỗi CS0118); khi đó viết `AdImpression.AdImpression`.
