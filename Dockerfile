# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set the working directory inside the container to /src
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY eCart.API/eCart.API/eCart.API.csproj eCart.API/eCart.API/
RUN dotnet restore "eCart.API/eCart.API/eCart.API.csproj"

# Copy the rest of the files and build the project
COPY . .
WORKDIR "/src/eCart.API/eCart.API"
RUN dotnet build "eCart.API.csproj" -c Release -o /app/build

# Publish the application
RUN dotnet publish "eCart.API.csproj" -c Release -o /app/publish

# Use a smaller runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "eCart.API.dll"]

# Expose the port the app will run on (default is 80)
EXPOSE 80
