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
            // FORM SETTINGS: Increased width to 650 to prevent cutting
            this.Text = "Student Grade Predictor AI";
            this.Size = new Size(650, 750);
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            SetupSpaciousUI();

            string dataPath = Path.Combine(Application.StartupPath, "student_data.csv");
            if (File.Exists(dataPath)) mlManager.Train(dataPath);
        }

        private void SetupSpaciousUI()
        {
            // HEADER
            Panel header = new Panel { Dock = DockStyle.Top, Height = 100, BackColor = Color.FromArgb(28, 40, 51) };
            Label title = new Label
            {
                Text = "STUDENT PERFORMANCE ANALYSIS",
                ForeColor = Color.White,
                Font = new Font("Arial", 22, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            header.Controls.Add(title);
            this.Controls.Add(header);

            // MAIN CONTAINER (White Card)
            Panel card = new Panel
            {
                Size = new Size(550, 550),
                Location = new Point(40, 120),
                BackColor = Color.White,
                Padding = new Padding(20)
            };
            this.Controls.Add(card);

            int yOffset = 30;
            // Labels are now much wider (300px) and textboxes follow
            txtStudy = CreateInputRow(card, "Study Time (1 to 4):", ref yOffset);
            txtFailures = CreateInputRow(card, "Past Failures (0 to 3):", ref yOffset);
            txtAbsences = CreateInputRow(card, "Absences (0 to 93):", ref yOffset);
            txtG1 = CreateInputRow(card, "G1 Grade (0 to 20):", ref yOffset);
            txtG2 = CreateInputRow(card, "G2 Grade (0 to 20):", ref yOffset);

            // PREDICT BUTTON
            Button btn = new Button
            {
                Text = "PREDICT FINAL GRADE",
                Size = new Size(470, 60),
                Location = new Point(40, yOffset + 20),
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 14, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.Click += BtnPredict_Click;
            card.Controls.Add(btn);

            // RESULT LABEL
            lblResult = new Label
            {
                Text = "Result will appear here",
                Size = new Size(470, 50),
                Location = new Point(40, yOffset + 100),
                Font = new Font("Arial", 18, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.DimGray
            };
            card.Controls.Add(lblResult);
        }

        private TextBox CreateInputRow(Panel parent, string labelText, ref int y)
        {
            // Wider label to ensure text is never cut
            Label lbl = new Label
            {
                Text = labelText,
                Location = new Point(40, y),
                Width = 250,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Black
            };

            TextBox txt = new TextBox
            {
                Location = new Point(300, y - 5),
                Width = 210,
                Font = new Font("Arial", 14),
                BorderStyle = BorderStyle.FixedSingle
            };

            parent.Controls.Add(lbl);
            parent.Controls.Add(txt);
            y += 70; // High vertical spacing
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
                float score = mlManager.Predict(input);
                lblResult.Text = $"PREDICTED G3: {score:F2}";
                lblResult.ForeColor = score >= 10 ? Color.Green : Color.Red;
            }
            catch { MessageBox.Show("Please enter valid numbers in all fields."); }
        }
    }
}