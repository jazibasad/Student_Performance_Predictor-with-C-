using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Student_Performance_Predictor
{
    public partial class Form1 : Form
    {
        private MLManager mlManager = new MLManager();
        private TextBox txtStudy, txtFailures, txtAbsences, txtG1, txtG2;
        private Label lblResult;

        public Form1()
        {
            // 1. Make the window much wider and taller
            this.Text = "Student Performance Predictor - Clear View";
            this.Size = new Size(800, 800);
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Padding = new Padding(20);

            SetupGridUI();

            // Auto-train logic
            string dataPath = Path.Combine(Application.StartupPath, "student_data.csv");
            if (File.Exists(dataPath)) mlManager.Train(dataPath);
        }

        private void SetupGridUI()
        {
            // 2. Use a TableLayoutPanel to prevent any overlapping
            TableLayoutPanel grid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 8,
                BackColor = Color.White,
                AutoScroll = true
            };

            // Set column widths (40% for labels, 60% for textboxes)
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));

            // Define modern fonts
            Font labelFont = new Font("Segoe UI", 14, FontStyle.Bold);
            Font inputFont = new Font("Segoe UI", 16);

            // Add Input Rows
            txtStudy = AddGridRow(grid, "Study Time (1-4):", labelFont, inputFont, 0);
            txtFailures = AddGridRow(grid, "Past Failures (0-3):", labelFont, inputFont, 1);
            txtAbsences = AddGridRow(grid, "Absences (0-93):", labelFont, inputFont, 2);
            txtG1 = AddGridRow(grid, "G1 Grade (0-20):", labelFont, inputFont, 3);
            txtG2 = AddGridRow(grid, "G2 Grade (0-20):", labelFont, inputFont, 4);

            // Predict Button (Spans both columns)
            Button btnPredict = new Button
            {
                Text = "CALCULATE PREDICTION",
                Dock = DockStyle.Fill,
                Height = 80,
                BackColor = Color.Navy,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Margin = new Padding(10, 30, 10, 10)
            };
            btnPredict.Click += BtnPredict_Click;
            grid.Controls.Add(btnPredict, 0, 5);
            grid.SetColumnSpan(btnPredict, 2);

            // Result Label (Spans both columns)
            lblResult = new Label
            {
                Text = "Result will appear here",
                Dock = DockStyle.Fill,
                Height = 100,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.DarkSlateGray
            };
            grid.Controls.Add(lblResult, 0, 6);
            grid.SetColumnSpan(lblResult, 2);

            this.Controls.Add(grid);
        }

        private TextBox AddGridRow(TableLayoutPanel panel, string text, Font lblF, Font txtF, int row)
        {
            Label lbl = new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = lblF,
                Padding = new Padding(0, 0, 10, 0)
            };

            TextBox txt = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = txtF,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10, 15, 50, 15) // Adds space around the box
            };

            panel.Controls.Add(lbl, 0, row);
            panel.Controls.Add(txt, 1, row);
            return txt;
        }

        private void BtnPredict_Click(object sender, EventArgs e)
        {
            try
            {
                var input = new StudentData
                {
                    StudyTime = float.Parse(txtStudy.Text),
                    Failures = float.Parse(txtFailures.Text),
                    Absences = float.Parse(txtAbsences.Text),
                    G1 = float.Parse(txtG1.Text),
                    G2 = float.Parse(txtG2.Text),
                    Health = 3
                };
                float result = mlManager.Predict(input);
                lblResult.Text = $"PREDICTED GRADE: {result:F2}";
                lblResult.ForeColor = result >= 10 ? Color.Green : Color.Red;
            }
            catch { MessageBox.Show("Please check that all boxes have numbers."); }
        }
    }
}