# Student Performance Predictor (AI-Powered)

An intelligent Windows Forms application that utilizes **ML.NET** to predict a student's final academic grade (G3) based on behavioral and demographic data. This project demonstrates the integration of Machine Learning within the .NET ecosystem and is fully containerized using Docker.

## 🚀 Features
* **AI Engine:** Uses the `FastTree Regression` algorithm from Microsoft.ML.
* **Modern UI:** High-visibility, grid-based layout to prevent text overlapping and ensure clarity.
* **Real-time Prediction:** Analyzes study time, failures, absences, and period grades to forecast final results.
* **Dockerized:** Ready for cross-platform deployment and consistent environment setup.

## 📊 Dataset Information
The model is trained on the **UCI Student Performance Dataset** (`student_data.csv`).
* **Key Features:** Study Time (1-4), Failures (0-3), Absences (0-93), G1 & G2 Grades (0-20).
* **Target Variable:** G3 (Final Grade).

---

## 🛠️ Setup Instructions

### Prerequisites
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (with .NET Desktop Development workload)
* [Docker Desktop](https://www.docker.com/products/docker-desktop/) (for containerization)

### Local Development Setup
1.  **Clone the repository** (or copy the project files).
2.  **Restore NuGet Packages:**
    Open the Package Manager Console in Visual Studio and run:
    ```powershell
    Install-Package Microsoft.ML
    Install-Package Microsoft.ML.FastTree
    ```
3.  **Prepare the Dataset:**
    Ensure `student_data.csv` is in the project root. In Visual Studio, right-click the file -> **Properties** -> set **Copy to Output Directory** to **Copy always**.
4.  **Run the App:**
    Press `F5` in Visual Studio to build and launch the UI.

---

## 🐳 Docker Deployment
To build and run the project as a containerized build environment:

1.  **Build the Image:**
    ```bash
    docker build -t student-predictor-ai .
    ```
2.  **Run the Container:**
    ```bash
    docker run student-predictor-ai
    ```
    *Note: As this is a GUI application, Docker is primarily used here for verifying build integrity and ML training logic.*

---

## 📂 Project Structure
* `Form1.cs`: The modern TableLayout-based user interface.
* `MLManager.cs`: The core logic for training and saving the ML.NET model.
* `StudentData.cs`: Data schemas for input features and prediction results.
* `Dockerfile`: Multi-stage build configuration for .NET 8.
* `.dockerignore`: Optimization file to prevent build context errors.