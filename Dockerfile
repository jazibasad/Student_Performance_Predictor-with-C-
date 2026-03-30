# STAGE 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0-windowsservercore-ltsc2019 AS build
WORKDIR /app

# Copy project and restore
COPY ["Student_Performance_Predictor.csproj", "./"]
RUN dotnet restore

# Copy all files and publish
COPY . ./
RUN dotnet publish "Student_Performance_Predictor.csproj" -c Release -o /app/publish

# STAGE 2: Runtime (Using SDK as Runtime to ensure Desktop Frameworks exist)
FROM mcr.microsoft.com/dotnet/sdk:8.0-windowsservercore-ltsc2019
WORKDIR /app

# Copy the published files
COPY --from=build /app/publish .

# Run the .exe
ENTRYPOINT ["Student_Performance_Predictor.exe"]