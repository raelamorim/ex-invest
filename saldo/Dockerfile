FROM mcr.microsoft.com/dotnet/core/sdk:3.1.100-alpine AS build-env
ENV ASPNETCORE_ENVIRONMENT Development
WORKDIR /app

# Copiar csproj e restaurar dependencias
COPY *.csproj ./
COPY . ./
RUN rm -r -v /app/bin && rm -r -v /app/obj

# Restore da aplicação
RUN dotnet restore

# Build da aplicacao

WORKDIR /app
RUN dotnet publish -c Release --no-restore -o out

# Build da imagem
WORKDIR /app
FROM mcr.microsoft.com/dotnet/core/sdk:3.1.100-alpine AS runtime

# COPY --from=build-env /app/Api/out .
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "Saldos.dll" ]