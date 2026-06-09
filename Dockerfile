# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy everything from this folder into the container
COPY . .

# Restore dependencies
RUN dotnet restore

# Build using a wildcard search for your project file
RUN dotnet build **/*.csproj -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish **/*.csproj -c Release -o /app/publish /p:UseAppHost=false

# Final Production Stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Looks for either ExamMSAppMVC.dll or ExamMSMVCApp.dll automatically
ENTRYPOINT ["dotnet", "ExamMSAppMVC.dll"]