#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["PaymentGateway.API/PaymentGateway.API.csproj", "PaymentGateway.API/"]
COPY ["PaymentGateway.Core/PaymentGateway.Core.csproj", "PaymentGateway.Core/"]
COPY ["PaymentGateway.Services/PaymentGateway.Services.csproj", "PaymentGateway.Services/"]
COPY ["PaymentGateway.Data/PaymentGateway.Data.csproj", "PaymentGateway.Data/"]
COPY ["PaymentGateway.Logging/PaymentGateway.Logging.csproj", "PaymentGateway.Logging/"]
RUN dotnet restore "PaymentGateway.API/PaymentGateway.API.csproj"
COPY . .
WORKDIR "/src/PaymentGateway.API"
RUN dotnet build "PaymentGateway.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentGateway.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentGateway.API.dll"]
