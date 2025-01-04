FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 as build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["eCart.API/eCart.API/eCart.API.csproj", "eCart.API/"]
RUN dotnet restore "eCart.API/eCart.API.csproj"
COPY . .
WORKDIR "eCart.API/eCart.API"
RUN dotnet build "eCart.API/eCart.API/eCart.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "eCart.API/eCart.API/eCart.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "eCart.API.dll" ]
