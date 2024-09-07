#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER app
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["./fosku-server.csproj", "fosku-server/"]
#RUN dotnet restore "./fosku-server/fosku-server.csproj"
#COPY . .
#WORKDIR "/src/fosku-server"
#RUN dotnet build "./fosku-server.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./fosku-server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "fosku-server.dll"]

## Use the official ASP.NET Core runtime as a parent image
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#WORKDIR /app
#EXPOSE 8080
#
## Use the official .NET SDK as a build environment
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#
#RUN dotnet tool install --global dotnet-ef
#ENV PATH="$PATH:/root/.dotnet/tools"
#
#WORKDIR /src
#
## Copy the project file and restore dependencies
#COPY fosku-server.csproj ./
#RUN dotnet restore "fosku-server.csproj"
#
## Copy the rest of the application code and the res folder
#COPY . .
#WORKDIR "/src"
#
## Build the application
#RUN dotnet build "fosku-server.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
## Publish the application
#RUN dotnet publish "fosku-server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
## Use the runtime image to create the final container
###FROM base AS final
#WORKDIR /app
#
## Copy the publish output
#COPY --from=build /app/publish .
#
#ENTRYPOINT ["dotnet", "fosku-server.dll"]

# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 8080

ARG BUILD_CONFIGURATION=Release

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

WORKDIR /src

# Copy the project file and restore dependencies
COPY fosku-server.csproj ./
RUN dotnet restore "fosku-server.csproj"

# Copy the rest of the application code and the res folder
COPY . .
WORKDIR "/src"

# Build the application
RUN dotnet build "fosku-server.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
RUN dotnet publish "fosku-server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Use the runtime image to create the final container
WORKDIR /app

# Copy the publish output
##COPY /app/publish .

ENTRYPOINT ["dotnet", "fosku-server.dll"]