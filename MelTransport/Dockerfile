# Stage 1: Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy project files
COPY ["MelTransport.API/MelTransport.API.csproj", "MelTransport.API/"]
COPY ["MelTransport.Application/MelTransport.Application.csproj", "MelTransport.Application/"]
COPY ["MelTransport.Domain/MelTransport.Domain.csproj", "MelTransport.Domain/"]
COPY ["MelTransport.Infrastructure/MelTransport.Infrastructure.csproj", "MelTransport.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "MelTransport.API/MelTransport.API.csproj"

# Copy all source code
COPY . .

# Build the project (Release mode for production)
RUN dotnet build "MelTransport.API/MelTransport.API.csproj" -c Release -o /app/build

# Publish to remove dependencies not needed at runtime
RUN dotnet publish "MelTransport.API/MelTransport.API.csproj" -c Release -o /app/publish


# Stage 2: Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Create non-root user for security
RUN groupadd -r aspnet && useradd -r -g aspnet aspnet

WORKDIR /app

# Copy compiled app from build stage
COPY --from=build /app/publish .

# Change ownership to non-root user
RUN chown -R aspnet:aspnet /app

# Switch to non-root user
USER aspnet

# Expose port (Kestrel default)
EXPOSE 5000

# Configure Kestrel to listen on port 5000 (all interfaces)
ENV ASPNETCORE_URLS=http://0.0.0.0:5000
ENV ASPNETCORE_ENVIRONMENT=Development

# Run the application 
CMD ["dotnet", "MelTransport.API.dll"]
