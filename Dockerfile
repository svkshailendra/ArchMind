# Stage 1: Build the application using .NET 10 SDK
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy the solution and project folders directly from the root
COPY ArchMind.slnx ./
COPY ArchMind.Web/ ArchMind.Web/
COPY ArchMind.Application/ ArchMind.Application/
COPY ArchMind.Domain/ ArchMind.Domain/
COPY ArchMind.Infrastructure/ ArchMind.Infrastructure/

# Restore dependencies using your slnx solution file
RUN dotnet restore ArchMind.Web/ArchMind.Web.csproj

# Publish the Web project directly
RUN dotnet publish ArchMind.Web/ArchMind.Web.csproj -c Release -o /app/publish

# Stage 2: Run the application using the light ASP.NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Render routes web traffic through port 10000 by default
ENV ASPNETCORE_HTTP_PORTS=10000

ENTRYPOINT ["dotnet", "ArchMind.Web.dll"]
