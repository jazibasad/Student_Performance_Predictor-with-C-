# 1. Use the .NET 8 SDK to build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 2. Copy the project file and restore
COPY ["Student_Performance_Predictor.csproj", "./"]
RUN dotnet restore "Student_Performance_Predictor.csproj"

# 3. Copy everything else and build for Windows
COPY . .
RUN dotnet publish "Student_Performance_Predictor.csproj" -c Release -o /app/publish -r win-x64 --self-contained false

# 4. Use the Runtime image
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app/publish .
COPY student_data.csv . 

ENTRYPOINT ["dotnet", "Student_Performance_Predictor.dll"]