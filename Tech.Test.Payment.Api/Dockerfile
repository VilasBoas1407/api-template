# Etapa 1: imagem base para o runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa 2: imagem de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiando os arquivos de projeto individualmente para restaurar as dependências
COPY ["Tech.Test.Payment.Api/Tech.Test.Payment.Api.csproj", "Tech.Test.Payment.Api/"]
COPY ["../Tech.Test.Payment.Application/Tech.Test.Payment.Application.csproj", "Tech.Test.Payment.Application/"]
COPY ["../Tech.Test.Payment.Contracts/Tech.Test.Payment.Contracts.csproj", "Tech.Test.Payment.Contracts/"]
COPY ["../Tech.Test.Payment.Infrastructure/Tech.Test.Payment.Infrastructure.csproj", "Tech.Test.Payment.Infrastructure/"]

# Restaurar as dependências
RUN dotnet restore "Tech.Test.Payment.Api/Tech.Test.Payment.Api.csproj"

# Copiar todo o código fonte para o contêiner
COPY ../ .

# Build do projeto
WORKDIR "/src/Tech.Test.Payment.Api"
RUN dotnet build "Tech.Test.Payment.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa 3: publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# Publicar o projeto
RUN dotnet publish "Tech.Test.Payment.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa 4: runtime final
FROM base AS final
WORKDIR /app

# Copiar os arquivos publicados para o contêiner final
COPY --from=publish /app/publish .

# Definir o ponto de entrada
ENTRYPOINT ["dotnet", "Tech.Test.Payment.Api.dll"]
