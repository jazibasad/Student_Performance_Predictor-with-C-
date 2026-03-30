# 1. Use the .NET 8 SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 2. Copy the project file and restore NuGet packages (ML.NET)
COPY ["Student_Performance_Predictor.csproj", "./"]
RUN dotnet restore "Student_Performance_Predictor.csproj"

# 3. Copy everything else and build the app
COPY . .
RUN dotnet publish "Student_Performance_Predictor.csproj" -c Release -o /app/publish

# 4. Use the Runtime image to run the app
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app/publish .

# 5. Important: Copy your dataset so the container can train the model
COPY student_data.csv . 

# 6. Start the application
ENTRYPOINT ["dotnet", "Student_Performance_Predictor.dll"]