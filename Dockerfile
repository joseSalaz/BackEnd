# Etapa 1: Build con SDK de .NET 8
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos de proyecto
COPY *.sln ./
COPY API.csproj ./API/
COPY Bussines.csproj ./Bussines/
COPY Constantes.csproj ./Constantes/
COPY DBModel.csproj ./DBModel/
COPY FileStorage.csproj ./FileStorage/
COPY Firebase.csproj ./Firebase/
COPY IBusiness.csproj ./IBusiness/
COPY IRepository.csproj ./IRepository/
COPY IService.csproj ./IService/
COPY Models.csproj ./Models/
COPY Repositorio.csproj ./Repositorio/
COPY Service.csproj ./Service/
COPY UtilExel.csproj ./UtilExel/
COPY UtilInterface.csproj ./UtilInterface/
COPY UtilMapper.csproj ./UtilMapper/
COPY UtilPDF.csproj ./UtilPDF/
COPY UtilSecurity.csproj ./UtilSecurity/

# Restaurar dependencias
RUN dotnet restore

# Copiar todo el código fuente
COPY . .

# Compilar y publicar en carpeta /app
WORKDIR /src/API
RUN dotnet publish -c Release -o /app

# Etapa 2: Runtime con imagen más ligera
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

# Railway/Render expone dinámicamente el puerto con la variable $PORT
ENV ASPNETCORE_URLS=http://+:${PORT}

# Ejecutar la API
ENTRYPOINT ["dotnet", "API.dll"]
