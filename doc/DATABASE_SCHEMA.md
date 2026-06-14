# Database Schema

Схема описывает первичный MVP и должна уточняться перед миграциями. Изменение доменной модели требует обновления `doc/DOMAIN.md` и этого файла.

## Общие поля

Большинство таблиц используют:

- `Id uuid primary key`;
- `CreatedAt timestamp with time zone`;
- `UpdatedAt timestamp with time zone null`;
- `IsDeleted boolean` только там, где нужен soft delete.

## AdminUsers

- `Id uuid primary key`.
- `Email text not null unique`.
- `PasswordHash text not null`.
- `DisplayName text not null`.
- `IsActive boolean not null`.
- `RefreshTokenHash text null`.
- `RefreshTokenExpiresAt timestamp with time zone null`.
- `HasCompletedOnboarding boolean not null`.
- `OnboardingCompletedAt timestamp with time zone null`.

## Inquiries

- `Id uuid primary key`.
- `InquiryNumber text not null unique`.
- `CustomerName text not null`.
- `Phone text null`.
- `WhatsApp text null`.
- `Telegram text null`.
- `Vk text null`.
- `Email text null`.
- `BlanketSizeId uuid null references BlanketSizes(Id)`.
- `FabricId uuid null references Fabrics(Id)`.
- `LeafShapeId uuid null references LeafShapes(Id)`.
- `DiscountId uuid null references Discounts(Id)`.
- `EstimatedPrice numeric(12,2) null`.
- `CustomerComment text null`.
- `PreferredContactMethod text not null`.
- `Status text not null`.
- `AdminNote text null`.
- `PersonalDataConsent boolean not null`.
- `ContactedAt timestamp with time zone null`.

Индексы:

- `InquiryNumber unique`.
- `Status`.
- `CreatedAt`.

## BlanketSizes

- `Id uuid primary key`.
- `Name text not null`.
- `Dimensions text not null`.
- `Coefficient numeric(8,2) not null`.
- `IsActive boolean not null`.
- `SortOrder integer not null`.

## Fabrics

- `Id uuid primary key`.
- `Name text not null`.
- `Description text null`.
- `PricePerUnit numeric(12,2) not null`.
- `IsActive boolean not null`.
- `SortOrder integer not null`.

## LeafShapes

- `Id uuid primary key`.
- `Name text not null`.
- `Description text null`.
- `Surcharge numeric(12,2) not null`.
- `IsActive boolean not null`.
- `SortOrder integer not null`.

## Discounts

- `Id uuid primary key`.
- `Name text not null`.
- `Description text null`.
- `Amount numeric(12,2) not null`.
- `IsActive boolean not null`.
- `SortOrder integer not null`.

## PriceCalculationSettings

- `Id uuid primary key`.
- `BaseAmount numeric(12,2) not null`.
- `UpdatedByAdminUserId uuid null references AdminUsers(Id)`.

## Calculator persistence strategy

В calculator persistence входят таблицы:

- `blanket_sizes`;
- `fabrics`;
- `leaf_shapes`;
- `discounts`;
- `price_calculation_settings`.

Таблица `PriceQuotes` в MVP не создается.

`PriceQuote` остается доменной моделью/snapshot предварительного расчета и не маппится в EF. При будущем `SubmitInquiry` backend пересчитывает цену и сохраняет snapshot выбранных параметров и рассчитанной цены внутри `Inquiry`.

На шаге 2.4-B1 добавлены только `DbSet` и EF configurations для этих пяти таблиц. Seed data и migrations будут отдельным шагом.

## GalleryItems

- `Id uuid primary key`.
- `Title text not null`.
- `Description text null`.
- `ImageUrl text not null`.
- `AltText text not null`.
- `IsPublished boolean not null`.
- `IsFeatured boolean not null`.
- `SortOrder integer not null`.

## Testimonials

- `Id uuid primary key`.
- `AuthorName text not null`.
- `Text text not null`.
- `Source text null`.
- `IsPublished boolean not null`.
- `SortOrder integer not null`.

## FaqItems

- `Id uuid primary key`.
- `Question text not null`.
- `Answer text not null`.
- `IsPublished boolean not null`.
- `SortOrder integer not null`.

## ContactSettings

- `Id uuid primary key`.
- `WhatsAppUrl text null`.
- `TelegramUrl text null`.
- `VkUrl text null`.
- `Email text null`.
- `Phone text null`.

## SiteSettings

- `Id uuid primary key`.
- `SiteName text not null`.
- `HeroTitle text null`.
- `HeroSubtitle text null`.
- `MetaTitle text null`.
- `MetaDescription text null`.

## AnalyticsEvents

- `Id uuid primary key`.
- `EventName text not null`.
- `PayloadJson jsonb null`.
- `SessionId text null`.
- `Path text null`.
- `Referrer text null`.
- `UserAgentHash text null`.
- `OccurredAt timestamp with time zone not null`.

Индексы:

- `EventName`.
- `OccurredAt`.
- `Path`.
