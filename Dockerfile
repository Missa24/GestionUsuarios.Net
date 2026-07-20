FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src

COPY ["src/BackendNet.Api/BackendNet.Api.csproj", "src/BackendNet.Api/"]
COPY ["src/BackendNet.Application/BackendNet.Application.csproj", "src/BackendNet.Application/"]
COPY ["src/BackendNet.Domain/BackendNet.Domain.csproj", "src/BackendNet.Domain/"]
COPY ["src/BackendNet.Infrastructure/BackendNet.Infrastructure.csproj", "src/BackendNet.Infrastructure/"]

RUN dotnet restore "src/BackendNet.Api/BackendNet.Api.csproj"

COPY . .

RUN dotnet publish "src/BackendNet.Api/BackendNet.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "BackendNet.Api.dll"]