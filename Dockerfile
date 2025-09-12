# Imagen base para compilar con .NET 8
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar todo el proyecto
COPY . .

# Restaurar dependencias
RUN dotnet restore "LibreriaSaber.sln"

# Compilar y publicar específicamente el proyecto API
RUN dotnet publish "01 APIs/API/API.csproj" -c Release -o /app/publish

# Imagen ligera para ejecutar con .NET 8
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Configurar puerto dinámico
ENV ASPNETCORE_URLS=http://+:${PORT}

# Ejecutar el DLL de tu API
ENTRYPOINT ["dotnet", "API.dll"]