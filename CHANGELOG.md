# Lịch sử thay đổi

Mọi thay đổi đáng kể của `com.riseon.adimpression` được ghi ở đây. Định dạng theo
[Keep a Changelog](https://keepachangelog.com/vi/1.1.0/), đánh số theo
[Semantic Versioning](https://semver.org/lang/vi/).

## [1.0.2] - 2026-09-23

### Thay đổi

- Tổ chức lại thư mục theo bố cục chuẩn của package Unity: `Core/` thành
  `Runtime/`, `Adapter/<Tên>` thành `Runtime/Adapters/<Tên>`. Tên assembly,
  namespace và GUID không đổi, nên project đang dùng không phải sửa gì.
- `LICENSE` đổi tên thành `LICENSE.md` theo bố cục chuẩn của package Unity (nút
  Licenses của Package Manager tìm file này).
- README gốc rút gọn; chi tiết dữ liệu và adapter chuyển sang `Runtime/README.md`
  và `Runtime/Adapters/README.md`.

### Thêm

- `CHANGELOG.md`.
- `documentationUrl` trong `package.json`, trỏ tới README của đúng version trên GitHub.

## [1.0.1] - 2026-09-21

### Thay đổi

- `package.json`: thêm `author` "RiseOn", `displayName` đổi thành "Ad Impression".

## [1.0.0] - 2026-09-21

Bản phát hành đầu tiên dưới dạng package UPM.

### Thêm

- Struct `AdImpression` và enum `Mediation`.
- Adapter đầu vào cho AppLovin MAX, AdMob, NativeAdMob và adapter đầu ra cho
  AppsFlyer, tự bật theo SDK có trong project.
- Giấy phép MIT.
