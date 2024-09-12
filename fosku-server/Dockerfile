FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY fosku-server.csproj ./
RUN dotnet restore "fosku-server.csproj"

COPY . .
WORKDIR "/src"

RUN dotnet build "fosku-server.csproj" -c $BUILD_CONFIGURATION -o /app/build

RUN dotnet publish "fosku-server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "fosku-server.dll"]