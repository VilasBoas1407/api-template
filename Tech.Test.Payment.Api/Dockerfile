FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

EXPOSE 8080
EXPOSE 8081

COPY ["Tech.Test.Payment.Api/Tech.Test.Payment.Api.csproj", "Tech.Test.Payment.Api/"]
COPY ["Tech.Test.Payment.Application/Tech.Test.Payment.Application.csproj", "Tech.Test.Payment.Application/"]
COPY ["Tech.Test.Payment.Contracts/Tech.Test.Payment.Contracts.csproj", "Tech.Test.Payment.Contracts/"]
COPY ["Tech.Test.Payment.Infrastructure/Tech.Test.Payment.Infrastructure.csproj", "Tech.Test.Payment.Infrastructure/"]


RUN dotnet restore "Tech.Test.Payment.Api/Tech.Test.Payment.Api.csproj"

COPY . .

RUN dotnet publish "Tech.Test.Payment.Api/Tech.Test.Payment.Api.csproj" -c Release -o /app


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app .

ENTRYPOINT ["dotnet", "Tech.Test.Payment.Api.dll"]
