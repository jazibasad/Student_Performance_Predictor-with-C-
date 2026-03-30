# STAGE 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy and restore
COPY ["Student_Performance_Predictor.csproj", "./"]
RUN dotnet restore

# Copy all files
COPY . .

# CHANGE: Target win-x64 so WinForms libraries are included
RUN dotnet publish "Student_Performance_Predictor.csproj" \
    -c Release \
    -o /app/publish \
    -r win-x64 \
    --self-contained false

# STAGE 2: Runtime
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app

# Copy the published files
COPY --from=build /app/publish .

# Run the DLL using the dotnet command
ENTRYPOINT ["dotnet", "Student_Performance_Predictor.dll"]