FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["../MessengerServer.WebAPI/MessengerServer.WebAPI.csproj", "MessengerServer.WebAPI/"]
RUN dotnet restore "./MessengerServer.WebAPI/MessengerServer.WebAPI.csproj"
COPY . .
WORKDIR "/src/MessengerServer.WebAPI"
RUN dotnet build "./MessengerServer.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MessengerServer.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessengerServer.WebAPI.dll"]
