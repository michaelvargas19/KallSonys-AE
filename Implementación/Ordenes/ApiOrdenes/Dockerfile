#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ApiOrdenes/ApiOrdenes.csproj", "ApiOrdenes/"]
COPY ["Ordenes.Infraestructura/Ordenes.Infraestructura.csproj", "Ordenes.Infraestructura/"]
COPY ["Ordenes.Dominio/Ordenes.Dominio.csproj", "Ordenes.Dominio/"]
RUN dotnet restore "ApiOrdenes/ApiOrdenes.csproj"
COPY . .
WORKDIR "/src/ApiOrdenes"
RUN dotnet build "ApiOrdenes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiOrdenes.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiOrdenes.dll"]