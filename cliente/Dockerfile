FROM mcr.microsoft.com/dotnet/core/sdk:3.1.100-alpine AS build-env
ENV ASPNETCORE_ENVIRONMENT Development
WORKDIR /app

# Copiar csproj e restaurar dependencias
COPY *.csproj ./
# COPY *.sln .
# COPY Core/*.csproj ./Core/
# COPY Api/*.csproj ./Api/

# Restore da aplicação
RUN dotnet restore

# Build da aplicacao
COPY . ./

WORKDIR /app
RUN dotnet publish -c Release --no-restore -o out

# Build da imagem
FROM mcr.microsoft.com/dotnet/core/sdk:3.1.100-alpine AS runtime
WORKDIR /app
# COPY --from=build-env /app/Api/out .
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "Clientes.dll" ]



