#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 9000
EXPOSE 10000

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Autenticacion.API/Autenticacion.API.csproj", "Autenticacion.API/"]
COPY ["Autenticacion.Infraestructura/Autenticacion.Infraestructura.csproj", "Autenticacion.Infraestructura/"]
COPY ["Autenticacion.Dominio/Autenticacion.Dominio.csproj", "Autenticacion.Dominio/"]
RUN dotnet restore "Autenticacion.API/Autenticacion.API.csproj"
COPY . .
WORKDIR "/src/Autenticacion.API"
RUN dotnet build "Autenticacion.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Autenticacion.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Autenticacion.API.dll"]