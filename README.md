# Student_Performance_Predictor (AI Mini Project)

## Description
This project is an AI-based Student Performance Predictor developed in .NET 8. It uses **ML.NET** with a **FastTree Regression** algorithm to predict the final grade (G3) of a student based on study time, failures, and previous grades.

## Dataset
- **Source:** UCI Student Performance Dataset (`student_data.csv`)
- **Target:** G3 (Final Grade)

## Technologies
- C# / WinForms
- Microsoft.ML (ML.NET)
- Docker

## Setup
1. Open the `.sln` in Visual Studio 2022.
2. Install `Microsoft.ML` and `Microsoft.ML.FastTree` via NuGet.
3. Ensure `student_data.csv` is in the project root.
4. Run the project to start the prediction UI.