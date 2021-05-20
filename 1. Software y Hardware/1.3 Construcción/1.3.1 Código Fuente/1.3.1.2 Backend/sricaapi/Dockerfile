FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN sed -i "s/MinProtocol = TLSv1.2/MinProtocol = TLSv1/" "/etc/ssl/openssl.cnf"
RUN sed -i "s/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g" "/etc/ssl/openssl.cnf"
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["API.SRICA.Aplicacion.DTO/API.SRICA.Aplicacion.DTO.csproj", "API.SRICA.Aplicacion.DTO/"]
COPY ["API.SRICA.Aplicacion.Implementacion/API.SRICA.Aplicacion.Implementacion.csproj", "API.SRICA.Aplicacion.Implementacion/"]
COPY ["API.SRICA.Aplicacion.Interfaz/API.SRICA.Aplicacion.Interfaz.csproj", "API.SRICA.Aplicacion.Interfaz/"]
COPY ["API.SRICA.Distribucion/API.SRICA.Distribucion.csproj", "API.SRICA.Distribucion/"]
COPY ["API.SRICA.Dominio.Entidad/API.SRICA.Dominio.Entidad.csproj", "API.SRICA.Dominio.Entidad/"]
COPY ["API.SRICA.Dominio.Excepcion/API.SRICA.Dominio.Excepcion.csproj", "API.SRICA.Dominio.Excepcion/"]
COPY ["API.SRICA.Dominio.Interfaz/API.SRICA.Dominio.Interfaz.csproj", "API.SRICA.Dominio.Interfaz/"]
COPY ["API.SRICA.Dominio.Servicio.Implementacion/API.SRICA.Dominio.Servicio.Implementacion.csproj", "API.SRICA.Dominio.Servicio.Implementacion/"]
COPY ["API.SRICA.Dominio.Servicio.Interfaz/API.SRICA.Dominio.Servicio.Interfaz.csproj", "API.SRICA.Dominio.Servicio.Interfaz/"]
COPY ["API.SRICA.Dominio.ServicioExterno.Interfaz/API.SRICA.Dominio.ServicioExterno.Interfaz.csproj", "API.SRICA.Dominio.ServicioExterno.Interfaz/"]
COPY ["API.SRICA.Infraestructura.Configuracion/API.SRICA.Infraestructura.Configuracion.csproj", "API.SRICA.Infraestructura.Configuracion/"]
COPY ["API.SRICA.Infraestructura.Injector/API.SRICA.Infraestructura.Injector.csproj", "API.SRICA.Infraestructura.Injector/"]
COPY ["API.SRICA.Infraestructura.ServicioExterno.Implementacion/API.SRICA.Infraestructura.ServicioExterno.Implementacion.csproj", "API.SRICA.Infraestructura.ServicioExterno.Implementacion/"]
RUN dotnet restore "API.SRICA.Distribucion/API.SRICA.Distribucion.csproj"
COPY . .
WORKDIR "/src/API.SRICA.Distribucion"
RUN dotnet build "API.SRICA.Distribucion.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "API.SRICA.Distribucion.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV TZ="America/Lima"
ENTRYPOINT ["dotnet", "API.SRICA.Distribucion.dll"]