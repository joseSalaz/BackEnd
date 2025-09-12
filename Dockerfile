# Imagen base para compilar con .NET 8
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivo de solución
COPY LibreriaSaber.sln .

# Copiar todos los proyectos con sus rutas correctas
COPY "01 APIS/API/API.csproj" "01 APIS/API/"
COPY "02 Bussines/Bussines/Bussines.csproj" "02 Bussines/Bussines/"
COPY "02 Bussines/IBussines/IBussines.csproj" "02 Bussines/IBussines/"
COPY "03 Servicios/IService/IService.csproj" "03 Servicios/IService/"
COPY "03 Servicios/Service/Service.csproj" "03 Servicios/Service/"
COPY "04 Repositorio/IRepository/IRepository.csproj" "04 Repositorio/IRepository/"
COPY "04 Repositorio/Repository/Repository.csproj" "04 Repositorio/Repository/"
COPY "05 Model/DBModel/DBModel.csproj" "05 Model/DBModel/"
COPY "05 Model/Models/Models.csproj" "05 Model/Models/"
COPY "06 Util/Constantes/Constantes.csproj" "06 Util/Constantes/"
COPY "06 Util/Firebase/Firebase.csproj" "06 Util/Firebase/"
COPY "06 Util/UtilExel/UtilExel.csproj" "06 Util/UtilExel/"
COPY "06 Util/UtilInterface/UtilInterface.csproj" "06 Util/UtilInterface/"
COPY "06 Util/UtilMapper/UtilMapper.csproj" "06 Util/UtilMapper/"
COPY "06 Util/UtilPDF/UtilPDF.csproj" "06 Util/UtilPDF/"
COPY "06 Util/UtilSecurity/UtilSecurity.csproj" "06 Util/UtilSecurity/"
COPY "10 Anotaciones/Anotaciones.csproj" "10 Anotaciones/"

# Restaurar dependencias
RUN dotnet restore

# Copiar todo el código
COPY . .

# Compilar y publicar API
WORKDIR "/src/01 APIS/API"
RUN dotnet publish -c Release -o /app

# Imagen ligera para ejecutar con .NET 8
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .

# Puerto dinámico (lo asigna Railway/Render/etc)
ENV ASPNETCORE_URLS=http://+:${PORT}

ENTRYPOINT ["dotnet", "API.dll"]
