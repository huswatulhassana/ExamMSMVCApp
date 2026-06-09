# ----------------------
# Base runtime image
# ----------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# ----------------------
# Build stage
# ----------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy EVERYTHING from where the Dockerfile is located
COPY . .

# Restore dependencies automatically without specifying a folder path
RUN dotnet restore

# Build using a wildcard search to find your .csproj file automatically
RUN dotnet build **/*.csproj -c Release -o /app/build

# ----------------------
# Publish stage
# ----------------------
FROM build AS publish
RUN dotnet publish **/*.csproj -c Release -o /app/publish /p:UseAppHost=false

# ----------------------
# Final image
# ----------------------
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# ENTRYPOINT points to the compiled application DLL
ENTRYPOINT ["dotnet", "ExamMSAppMVC.dll"]