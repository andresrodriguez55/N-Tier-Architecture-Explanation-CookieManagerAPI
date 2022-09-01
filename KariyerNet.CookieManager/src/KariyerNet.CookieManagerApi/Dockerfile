#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["KariyerNet.CookieManager/src/KariyerNet.CookieManagerApi/KariyerNet.CookieManagerApi.csproj", "KariyerNet.CookieManager/src/KariyerNet.CookieManagerApi/"]
COPY ["KariyerNet.CookieManager/src/KariyerNet.CookieManager.Data/KariyerNet.CookieManager.Data.csproj", "KariyerNet.CookieManager/src/KariyerNet.CookieManager.Data/"]
COPY ["KariyerNet.CookieManager/src/KariyerNet.CookieManager.Data.Entity/KariyerNet.CookieManager.Data.Entity/KariyerNet.CookieManager.Data.Entity.csproj", "KariyerNet.CookieManager/src/KariyerNet.CookieManager.Data.Entity/KariyerNet.CookieManager.Data.Entity/"]
COPY ["KariyerNet.CookieManager/src/KariyerNet.CookieManager.Common/KariyerNet.CookieManager.Common.csproj", "KariyerNet.CookieManager/src/KariyerNet.CookieManager.Common/"]
COPY ["KariyerNet.CookieManager/src/KariyerNet.CookieManager.Data.Contract/KariyerNet.CookieManager.Data.Contract.csproj", "KariyerNet.CookieManager/src/KariyerNet.CookieManager.Data.Contract/"]
COPY ["KariyerNet.CookieManager/src/KariyerNet.CookieManager.Business/KariyerNet.CookieManager.Business.csproj", "KariyerNet.CookieManager/src/KariyerNet.CookieManager.Business/"]
COPY ["KariyerNet.CookieManager/src/KariyerNet.CookieManager.Business.Contract/KariyerNet.CookieManager.Business.Contract.csproj", "KariyerNet.CookieManager/src/KariyerNet.CookieManager.Business.Contract/"]
COPY ["KariyerNet.CookieManager/src/KariyerNet.CookieManager.Business.Dto/KariyerNet.CookieManager.Business.Dto.csproj", "KariyerNet.CookieManager/src/KariyerNet.CookieManager.Business.Dto/"]
RUN dotnet restore "KariyerNet.CookieManager/src/KariyerNet.CookieManagerApi/KariyerNet.CookieManagerApi.csproj"
COPY . .
WORKDIR "/src/KariyerNet.CookieManager/src/KariyerNet.CookieManagerApi"
RUN dotnet build "KariyerNet.CookieManagerApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KariyerNet.CookieManagerApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KariyerNet.CookieManagerApi.dll"]