 # Stage 1: Build the application using .NET 10 SDK
FROM ://microsoft.com AS build
WORKDIR /src

# Copy the solution and project folders to restore dependencies
COPY ArchiMind.sln ./
COPY src/ArchiMind.Web/ src/ArchiMind.Web/
COPY src/ArchiMind.Application/ src/ArchiMind.Application/
COPY src/ArchiMind.Domain/ src/ArchiMind.Domain/
COPY src/ArchiMind.Infrastructure/ src/ArchiMind.Infrastructure/

RUN dotnet restore ArchiMind.sln

# Publish the Web app to a release folder
RUN dotnet publish src/ArchiMind.Web/ArchiMind.Web.csproj -c Release -o /app/publish

# Stage 2: Run the application using the light ASP.NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Render routes web traffic through port 10000 by default
ENV ASPNETCORE_HTTP_PORTS=10000

ENTRYPOINT ["dotnet", "ArchiMind.Web.dll"]
