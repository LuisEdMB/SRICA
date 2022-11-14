FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN sed -i "s/MinProtocol = TLSv1.2/MinProtocol = TLSv1/" "/etc/ssl/openssl.cnf"
RUN sed -i "s/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g" "/etc/ssl/openssl.cnf"
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["1.3.1.2 Backend/sricaapi/API.SRICA.Aplicacion.DTO/API.SRICA.Aplicacion.DTO.csproj", "1.3.1.2 Backend/sricaapi/API.SRICA.Aplicacion.DTO/API.SRICA.Aplicacion.DTO/"]
COPY ["1.3.1.2 Backend/sricaapi/API.SRICA.Aplicacion.Implementacion/API.SRICA.Aplicacion.Implementacion.csproj", "1.3.1.2 Backend/sricaapi/API.SRICA.Aplicacion.Implementacion/"]
COPY ["1.3.1.2 Backend/sricaapi/API.SRICA.Aplicacion.Interfaz/API.SRICA.Aplicacion.Interfaz.csproj", "1.3.1.2 Backend/sricaapi/API.SRICA.Aplicacion.Interfaz/"]
COPY ["1.3.1.2 Backend/sricaapi/API.SRICA.Distribucion/API.SRICA.Distribucion.csproj", "1.3.1.2 Backend/sricaapi/API.SRICA.Distribucion/"]
COPY ["1.3.1.2 Backend/sricaapi/API.SRICA.Dominio.Entidad/API.SRICA.Dominio.Entidad.csproj", "1.3.1.2 Backend/sricaapi/API.SRICA.Dominio.Entidad/"]
COPY ["1.3.1.2 Backend/sricaapi/API.SRICA.Dominio.Excepcion/API.SRICA.Dominio.Excepcion.csproj", "1.3.1.2 Backend/sricaapi/API.SRICA.Dominio.Excepcion/"]
COPY ["1.3.1.2 Backend/sricaapi/API.SRICA.Dominio.Interfaz/API.SRICA.Dominio.Interfaz.csproj", "1.3.1.2 Backend/sricaapi/API.SRICA.Dominio.Interfaz/"]
COPY ["1.3.1.2 Backend/sricaapi/API.SRICA.Dominio.Servicio.Implementacion/API.SRICA.Dominio.Servicio.Implementacion.csproj", "1.3.1.2 Backend/sricaapi/API.SRICA.Dominio.Servicio.Implementacion/"]
COPY ["1.3.1.2 Backend/sricaapi/API.SRICA.Dominio.Servicio.Interfaz/API.SRICA.Dominio.Servicio.Interfaz.csproj", "1.3.1.2 Backend/sricaapi/API.SRICA.Dominio.Servicio.Interfaz/"]
COPY ["1.3.1.2 Backend/sricaapi/API.SRICA.Dominio.ServicioExterno.Interfaz/API.SRICA.Dominio.ServicioExterno.Interfaz.csproj", "1.3.1.2 Backend/sricaapi/API.SRICA.Dominio.ServicioExterno.Interfaz/"]
COPY ["1.3.1.2 Backend/sricaapi/API.SRICA.Infraestructura.Configuracion/API.SRICA.Infraestructura.Configuracion.csproj", "1.3.1.2 Backend/sricaapi/API.SRICA.Infraestructura.Configuracion/"]
COPY ["1.3.1.2 Backend/sricaapi/API.SRICA.Infraestructura.Injector/API.SRICA.Infraestructura.Injector.csproj", "1.3.1.2 Backend/sricaapi/API.SRICA.Infraestructura.Injector/"]
COPY ["1.3.1.2 Backend/sricaapi/API.SRICA.Infraestructura.ServicioExterno.Implementacion/API.SRICA.Infraestructura.ServicioExterno.Implementacion.csproj", "1.3.1.2 Backend/sricaapi/API.SRICA.Infraestructura.ServicioExterno.Implementacion/"]
RUN dotnet restore "1.3.1.2 Backend/sricaapi/API.SRICA.Distribucion/API.SRICA.Distribucion.csproj"
COPY . .
WORKDIR "/src/1.3.1.2 Backend/sricaapi/API.SRICA.Distribucion"
RUN dotnet build "API.SRICA.Distribucion.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "API.SRICA.Distribucion.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV TZ="America/Lima"
ENTRYPOINT ["dotnet", "API.SRICA.Distribucion.dll"]