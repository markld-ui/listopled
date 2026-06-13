FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Listopled.sln ./
COPY src/Listopled.Domain/Listopled.Domain.csproj src/Listopled.Domain/
COPY src/Listopled.Application/Listopled.Application.csproj src/Listopled.Application/
COPY src/Listopled.Infrastructure/Listopled.Infrastructure.csproj src/Listopled.Infrastructure/
COPY src/Listopled.Api/Listopled.Api.csproj src/Listopled.Api/
RUN dotnet restore Listopled.sln

COPY . .
RUN dotnet publish src/Listopled.Api/Listopled.Api.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Listopled.Api.dll"]
