using Microsoft.ML.Data;

namespace Student_Performance_Predictor
{
    public class StudentData
    {
        [LoadColumn(13)] public float StudyTime;
        [LoadColumn(14)] public float Failures;
        [LoadColumn(28)] public float Health;
        [LoadColumn(29)] public float Absences;
        [LoadColumn(30)] public float G1;
        [LoadColumn(31)] public float G2;

        [LoadColumn(32), ColumnName("Label")]
        public float FinalGrade;
    }

    public class StudentPrediction
    {
        [ColumnName("Score")]
        public float PredictedG3;
    }
}