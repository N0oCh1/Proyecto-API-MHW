# Multi-stage Dockerfile for Proyecto-API-MHW (ASP.NET Core / .NET 8)
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers for better caching
COPY ["Proyecto-API-MHW/Proyecto-API-MHW.csproj", "Proyecto-API-MHW/"]
RUN dotnet restore "Proyecto-API-MHW/Proyecto-API-MHW.csproj"

# Copy everything else and publish
COPY . .
WORKDIR "/src/Proyecto-API-MHW"
RUN dotnet publish "Proyecto-API-MHW.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Listen on port 5000 in the container and map to host as needed
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 80 

ENTRYPOINT ["dotnet", "Proyecto-API-MHW.dll"]
