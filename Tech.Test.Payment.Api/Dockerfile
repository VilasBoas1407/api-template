# Etapa de construção
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar os arquivos de projeto e restaurar dependências
COPY ["Tech.Test.Payment.Api/Tech.Test.Payment.Api.csproj", "Tech.Test.Payment.Api/"]
COPY ["Tech.Test.Payment.Application/Tech.Test.Payment.Application.csproj", "Tech.Test.Payment.Application/"]
COPY ["Tech.Test.Payment.Contracts/Tech.Test.Payment.Contracts.csproj", "Tech.Test.Payment.Contracts/"]
COPY ["Tech.Test.Payment.Infrastructure/Tech.Test.Payment.Infrastructure.csproj", "Tech.Test.Payment.Infrastructure/"]
RUN dotnet restore "Tech.Test.Payment.Api/Tech.Test.Payment.Api.csproj"

# Copiar todo o código
COPY . .

# Construir e publicar a aplicação
RUN dotnet publish "Tech.Test.Payment.Api/Tech.Test.Payment.Api.csproj" -c Release -o /out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Instalar a ferramenta dotnet-ef na imagem de runtime
RUN apt-get update && apt-get install -y \
    curl \
    && curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 8.0 --install-dir /usr/local/bin

# Adicionar dotnet-tools ao PATH
ENV PATH="$PATH:/root/.dotnet/tools"

# Expor a porta
EXPOSE 80

## Aplicar migrations
#RUN dotnet ef database update --project Tech.Test.Payment.Infrastructure/Tech.Test.Payment.Infrastructure.csproj

# Definir o comando de entrada
ENTRYPOINT ["dotnet", "Tech.Test.Payment.Api.dll"]
