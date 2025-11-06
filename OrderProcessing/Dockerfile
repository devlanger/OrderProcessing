FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OrderProcessing/OrderProcessing.csproj", "OrderProcessing/"]
COPY ["OrderProcessing.Abstractions/OrderProcessing.Abstractions.csproj", "OrderProcessing.Abstractions/"]
COPY ["OrderProcessing.Core/OrderProcessing.Core.csproj", "OrderProcessing.Core/"]
COPY ["OrderProcessing.Application/OrderProcessing.Application.csproj", "OrderProcessing.Application/"]
COPY ["OrderProcessing.Infrastructure/OrderProcessing.Infrastructure.csproj", "OrderProcessing.Infrastructure/"]
RUN dotnet restore "OrderProcessing/OrderProcessing.csproj"
COPY . .
WORKDIR "/src/OrderProcessing"
RUN dotnet build "OrderProcessing.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OrderProcessing.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderProcessing.dll"]
