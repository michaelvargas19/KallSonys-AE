#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ApiClientes/ApiClientes.csproj", "ApiClientes/"]
COPY ["Clientes.Infraestructura/Clientes.Infraestructura.csproj", "Clientes.Infraestructura/"]
COPY ["Clientes.Dominio/Clientes.Dominio.csproj", "Clientes.Dominio/"]
RUN dotnet restore "ApiClientes/ApiClientes.csproj"
COPY . .
WORKDIR "/src/ApiClientes"
RUN dotnet build "ApiClientes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiClientes.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiClientes.dll"]