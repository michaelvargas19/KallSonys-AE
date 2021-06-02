#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Catalogos.API/Catalogos.API.csproj", "Catalogos.API/"]
COPY ["Catalogos.Infraestructura/Catalogos.Infraestructura.csproj", "Catalogos.Infraestructura/"]
COPY ["Catalogos.Dominio/Catalogos.Dominio.csproj", "Catalogos.Dominio/"]
RUN dotnet restore "Catalogos.API/Catalogos.API.csproj"
COPY . .
WORKDIR "/src/Catalogos.API"
RUN dotnet build "Catalogos.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalogos.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalogos.API.dll"]