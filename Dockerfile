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

# COPY ["ExamMSAppMVC/ExamMSAppMVC.csproj", "ExamMSAppMVC/"]
# RUN dotnet restore "ExamMSAppMVC/ExamMSAppMVC.csproj"

# Copy EVERYTHING from this subfolder
COPY . .
WORKDIR "/src/ExamMSAppMVC"

# Restore dependencies
# RUN dotnet restore

# Direct paths since we are already inside the project folder
RUN dotnet build "ExamMSAppMVC.csproj" -c Release -o /app/build

# ----------------------
# Publish stage
# ----------------------
FROM build AS publish
RUN dotnet publish "ExamMSAppMVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ----------------------
# Final image
# ----------------------
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ExamMSAppMVC.dll"]