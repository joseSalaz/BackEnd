# Imagen base para compilar con .NET 8
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar todo el contenido (menos optimizado pero más simple)
COPY . .

# Restaurar dependencias para toda la solución
RUN dotnet restore "LibreriaSaber.sln" || dotnet restore

# Buscar y compilar el proyecto principal de API
RUN dotnet publish -c Release -o /app $(find . -name "*.csproj" -path "*API*" | head -1) || \
    dotnet publish -c Release -o /app $(find . -name "*.csproj" | head -1)

# Imagen ligera para ejecutar con .NET 8
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .

# Puerto dinámico
ENV ASPNETCORE_URLS=http://+:${PORT}

# Buscar el archivo DLL principal
CMD find /app -name "*.dll" -not -name "*.Views.dll" -not -name "*.PrecompiledViews.dll" | head -1 | xargs -I {} dotnet {}