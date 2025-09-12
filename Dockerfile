# Etapa 1: Build con SDK de .NET 8
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solución
COPY *.sln ./

# Copiar proyectos (cada uno desde su carpeta real)
COPY API/API.csproj ./API/
COPY Bussines/Bussines.csproj ./Bussines/
COPY Constantes/Constantes.csproj ./Constantes/
COPY DBModel/DBModel.csproj ./DBModel/
COPY FileStorage/FileStorage.csproj ./FileStorage/
COPY Firebase/Firebase.csproj ./Firebase/
COPY IBusiness/IBusiness.csproj ./IBusiness/
COPY IRepository/IRepository.csproj ./IRepository/
COPY IService/IService.csproj ./IService/
COPY Models/Models.csproj ./Models/
COPY Repositorio/Repositorio.csproj ./Repositorio/
COPY Service/Service.csproj ./Service/
COPY UtilExel/UtilExel.csproj ./UtilExel/
COPY UtilInterface/UtilInterface.csproj ./UtilInterface/
COPY UtilMapper/UtilMapper.csproj ./UtilMapper/
COPY UtilPDF/UtilPDF.csproj ./UtilPDF/
COPY UtilSecurity/UtilSecurity.csproj ./UtilSecurity/

# Restaurar dependencias
RUN dotnet restore

# Copiar todo el código
COPY . .

# Compilar y publicar
WORKDIR /src/API
RUN dotnet publish -c Release -o /app

# Etapa 2: Runtime con imagen más ligera
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

# Puerto dinámico (Railway/Render)
ENV ASPNETCORE_URLS=http://+:${PORT}

# Ejecutar la API
ENTRYPOINT ["dotnet", "API.dll"]
