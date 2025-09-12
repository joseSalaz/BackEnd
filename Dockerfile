# Imagen base para compilar con .NET 8
FROM mcr.microsoft.comdotnetsdk8.0 AS build
WORKDIR src

# Copiar archivo de solución y restaurar dependencias
COPY .sln .
COPY API.csproj .API
COPY Bussines.csproj .Bussines
COPY Constantes.csproj .Constantes
COPY DBModel.csproj .DBModel
COPY FileStorage.csproj .FileStorage
COPY Firebase.csproj .Firebase
COPY IBusiness.csproj .IBusiness
COPY IRepository.csproj .IRepository
COPY IService.csproj .IService
COPY Models.csproj .Models
COPY Repositorio.csproj .Repositorio
COPY Service.csproj .Service
COPY UtilExel.csproj .UtilExel
COPY UtilInterface.csproj .UtilInterface
COPY UtilMapper.csproj .UtilMapper
COPY UtilPDF.csproj .UtilPDF
COPY UtilSecurity.csproj .UtilSecurity

RUN dotnet restore

# Copiar todo el código
COPY . .

# Compilar y publicar
WORKDIR srcAPI
RUN dotnet publish -c Release -o app

# Imagen ligera para ejecutar con .NET 8
FROM mcr.microsoft.comdotnetaspnet8.0 AS runtime
WORKDIR app
COPY --from=build app .

# Puerto que usará Railway (lo asigna automáticamente con la variable PORT)
ENV ASPNETCORE_URLS=http+${PORT}

ENTRYPOINT [dotnet, API.dll]


