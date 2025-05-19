FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base

RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

WORKDIR /app

# QUITAR CUANDO SE ACTUALICE EL CERTIFICADO DE DEVOPS.CONSALUD.NET
# RUN sed 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/' /etc/ssl/openssl.cnf > /etc/ssl/openssl.cnf.changed && mv /etc/ssl/openssl.cnf.changed /etc/ssl/openssl.cnf

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build

# QUITAR CUANDO SE ACTUALICE EL CERTIFICADO DE DEVOPS.CONSALUD.NET
# RUN sed 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/' /etc/ssl/openssl.cnf > /etc/ssl/openssl.cnf.changed && mv /etc/ssl/openssl.cnf.changed /etc/ssl/openssl.cnf

WORKDIR /src
# PARA CONFIGURAR EL NUGET PRIVADO DE CONSALUD
# REQUIERE PERSONAL ACCESS TOKEN (PAT) PARA CONECTAR AL SERVIDOR
# SE DEBE ENTREGAR COMO VARIABLE EN EL PIPELINE docker build -t seguridadbff -f .\SeguridadBFF\Dockerfile . --build-arg PAT=$(VARIABLE_DEFINIDA_PIPELINE)
# ARG PAT
# RUN sed -i "s|</configuration>|<packageSourceCredentials><Interno><add key=\"Username\" value=\"teamfs\" /><add key=\"ClearTextPassword\" value=\"${PAT}\" /><add key=\"ValidAuthenticationTypes\" value=\"basic\" /></Interno></packageSourceCredentials></configuration>|" nuget.config

COPY ["ProductosBFF/ProductosBFF.csproj", "ProductosBFF/"]
RUN dotnet restore "ProductosBFF/ProductosBFF.csproj"
COPY . .
WORKDIR "/src/ProductosBFF"
RUN dotnet build "ProductosBFF.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductosBFF.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductosBFF.dll"]
