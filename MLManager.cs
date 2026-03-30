using Microsoft.ML;
using System.IO;

namespace Student_Performance_Predictor
{
    public class MLManager
    {
        private MLContext _mlContext;
        private ITransformer _model;
        private string _modelPath = "StudentModel.zip";

        public MLManager()
        {
            _mlContext = new MLContext(seed: 0);
        }

        public void Train(string dataPath)
        {
            if (!File.Exists(dataPath)) return;
            IDataView data = _mlContext.Data.LoadFromTextFile<StudentData>(
                dataPath, hasHeader: true, separatorChar: ',');

            var pipeline = _mlContext.Transforms.Concatenate("Features",
                "StudyTime", "Failures", "Health", "Absences", "G1", "G2")
                .Append(_mlContext.Regression.Trainers.FastTree());

            _model = pipeline.Fit(data);
            _mlContext.Model.Save(_model, data.Schema, _modelPath);
        }

        public float Predict(StudentData input)
        {
            if (_model == null && File.Exists(_modelPath))
                _model = _mlContext.Model.Load(_modelPath, out _);
            if (_model == null) return 0;
            var engine = _mlContext.Model.CreatePredictionEngine<StudentData, StudentPrediction>(_model);
            return engine.Predict(input).PredictedG3;
        }
    }
}