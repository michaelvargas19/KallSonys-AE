#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Inventarios.API/Inventarios.API.csproj", "Inventarios.API/"]
COPY ["Inventarios.Infraestructura/Inventarios.Infraestructura.csproj", "Inventarios.Infraestructura/"]
COPY ["Inventarios.Dominio/Inventarios.Dominio.csproj", "Inventarios.Dominio/"]
RUN dotnet restore "Inventarios.API/Inventarios.API.csproj"
COPY . .
WORKDIR "/src/Inventarios.API"
RUN dotnet build "Inventarios.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Inventarios.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inventarios.API.dll"]