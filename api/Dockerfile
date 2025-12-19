# 1. Usar imagen del SDK de .NET para compilar
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar el proyecto y restaurar dependencias
COPY ["api/Portfolio.Api.csproj", "api/"]
RUN dotnet restore "api/Portfolio.Api.csproj"

# Copiar todo el código fuente del backend
COPY api/ api/
WORKDIR "/src/api"

# Compilar la versión final
RUN dotnet publish "Portfolio.Api.csproj" -c Release -o /app/publish

# 2. Usar imagen ligera de ASP.NET para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080

# Copiar los archivos compilados desde el paso anterior
COPY --from=build /app/publish .

# Crear directorio para la DB y dar permisos (Importante para SQLite en Linux)
RUN mkdir -p /app/data
ENV ConnectionStrings__DefaultConnection="Data Source=/app/data/portfolio.db"

ENTRYPOINT ["dotnet", "Portfolio.Api.dll"]