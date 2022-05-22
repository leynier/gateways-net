#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gateways.Api/Gateways.Api.csproj", "Gateways.Api/"]
RUN dotnet restore "Gateways.Api/Gateways.Api.csproj"
COPY . .
WORKDIR "/src/Gateways.Api"
RUN dotnet build "Gateways.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gateways.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gateways.Api.dll"]