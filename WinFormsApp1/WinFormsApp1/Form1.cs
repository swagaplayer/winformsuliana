using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class Form1 : Form
    {
        private Label lblTitle;
        private Label lblDistance;
        private TextBox txtDistance;
        private Label lblDistanceUnit;
        private Label lblCostPerKm;
        private TextBox txtCostPerKm;
        private Label lblCostUnit;
        private Button btnCalculate;
        private Button btnClear;
        private Panel pnlResult;
        private Label lblResultTitle;
        private Label lblResultValue;
        private Label lblWarning;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            divider = new Panel();
            txtDistance = new TextBox();
            txtCostPerKm = new TextBox();
            btnCalculate = new Button();
            btnClear = new Button();
            pnlResult = new Panel();
            lblResultTitle = new Label();
            lblResultValue = new Label();
            lblWarning = new Label();
            pnlResult.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 60, 140);
            lblTitle.Location = new Point(20, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(430, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🚗  Калькулятор вартості поїздки";
            // 
            // divider
            // 
            divider.BackColor = Color.FromArgb(30, 60, 140);
            divider.Location = new Point(20, 55);
            divider.Name = "divider";
            divider.Size = new Size(420, 2);
            divider.TabIndex = 1;
            // 
            // txtDistance
            // 
            txtDistance.Font = new Font("Segoe UI", 11F);
            txtDistance.Location = new Point(200, 72);
            txtDistance.Name = "txtDistance";
            txtDistance.Size = new Size(160, 47);
            txtDistance.TabIndex = 2;
            txtDistance.TextAlign = HorizontalAlignment.Right;
            // 
            // txtCostPerKm
            // 
            txtCostPerKm.Font = new Font("Segoe UI", 11F);
            txtCostPerKm.Location = new Point(200, 115);
            txtCostPerKm.Name = "txtCostPerKm";
            txtCostPerKm.Size = new Size(160, 47);
            txtCostPerKm.TabIndex = 3;
            txtCostPerKm.TextAlign = HorizontalAlignment.Right;
            // 
            // btnCalculate
            // 
            btnCalculate.BackColor = Color.FromArgb(30, 80, 160);
            btnCalculate.Cursor = Cursors.Hand;
            btnCalculate.FlatAppearance.BorderSize = 0;
            btnCalculate.FlatStyle = FlatStyle.Flat;
            btnCalculate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCalculate.ForeColor = Color.White;
            btnCalculate.Location = new Point(20, 162);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(270, 40);
            btnCalculate.TabIndex = 4;
            btnCalculate.Text = "▶  Розрахувати вартість";
            btnCalculate.UseVisualStyleBackColor = false;
            btnCalculate.Click += btnCalculate_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(180, 60, 60);
            btnClear.Cursor = Cursors.Hand;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(305, 162);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(135, 40);
            btnClear.TabIndex = 5;
            btnClear.Text = "✖  Очистити";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // pnlResult
            // 
            pnlResult.BackColor = Color.FromArgb(225, 232, 255);
            pnlResult.Controls.Add(lblResultTitle);
            pnlResult.Controls.Add(lblResultValue);
            pnlResult.Location = new Point(20, 220);
            pnlResult.Name = "pnlResult";
            pnlResult.Size = new Size(420, 100);
            pnlResult.TabIndex = 6;
            pnlResult.Visible = false;
            // 
            // lblResultTitle
            // 
            lblResultTitle.Font = new Font("Segoe UI", 10F);
            lblResultTitle.ForeColor = Color.FromArgb(60, 80, 120);
            lblResultTitle.Location = new Point(15, 12);
            lblResultTitle.Name = "lblResultTitle";
            lblResultTitle.Size = new Size(390, 22);
            lblResultTitle.TabIndex = 0;
            lblResultTitle.Text = "Загальна вартість поїздки:";
            // 
            // lblResultValue
            // 
            lblResultValue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblResultValue.ForeColor = Color.FromArgb(20, 100, 40);
            lblResultValue.Location = new Point(15, 35);
            lblResultValue.Name = "lblResultValue";
            lblResultValue.Size = new Size(390, 50);
            lblResultValue.TabIndex = 1;
            // 
            // lblWarning
            // 
            lblWarning.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblWarning.ForeColor = Color.FromArgb(180, 100, 0);
            lblWarning.Location = new Point(20, 332);
            lblWarning.Name = "lblWarning";
            lblWarning.Size = new Size(420, 30);
            lblWarning.TabIndex = 7;
            lblWarning.Visible = false;
            // 
            // Form1
            // 
            BackColor = Color.FromArgb(240, 244, 255);
            ClientSize = new Size(617, 516);
            Controls.Add(lblTitle);
            Controls.Add(divider);
            Controls.Add(txtDistance);
            Controls.Add(txtCostPerKm);
            Controls.Add(btnCalculate);
            Controls.Add(btnClear);
            Controls.Add(pnlResult);
            Controls.Add(lblWarning);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Калькулятор вартості поїздки";
            pnlResult.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private Label MakeLabel(string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 10);
            lbl.ForeColor = Color.FromArgb(40, 40, 80);
            lbl.Location = new Point(x, y);
            lbl.Size = new Size(175, 25);
            return lbl;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            pnlResult.Visible = false;
            lblWarning.Visible = false;

            try
            {
                string rawDist = txtDistance.Text.Trim().Replace(",", ".");
                string rawCost = txtCostPerKm.Text.Trim().Replace(",", ".");

                if (string.IsNullOrEmpty(rawDist) || string.IsNullOrEmpty(rawCost))
                    throw new ArgumentException("Будь ласка, заповніть обидва поля.");

                if (!double.TryParse(rawDist,
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out double distance))
                    throw new FormatException("Відстань: введіть числове значення (наприклад, 35.5).");

                if (!double.TryParse(rawCost,
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out double costPerKm))
                    throw new FormatException("Вартість км: введіть числове значення (наприклад, 8.50).");

                if (distance < 0)
                    throw new ArgumentOutOfRangeException("Відстань не може бути від'ємною.");

                if (costPerKm < 0)
                    throw new ArgumentOutOfRangeException("Вартість кілометра не може бути від'ємною.");

                if (distance == 0 || costPerKm == 0)
                    throw new ArgumentOutOfRangeException("Відстань і вартість повинні бути більше нуля.");

                double total = distance * costPerKm;

                lblResultValue.Text = $"{total:F2} грн";
                lblResultTitle.Text = $"Поїздка {distance} км  ×  {costPerKm} грн/км:";
                pnlResult.Visible = true;

                if (total > 500)
                {
                    lblWarning.Text = "⚠  Сума перевищує 500 грн!";
                    lblWarning.Visible = true;
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, "Недопустиме значення",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Порожнє поле",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Помилка формату",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Несподівана помилка: " + ex.Message, "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDistance.Clear();
            txtCostPerKm.Clear();
            pnlResult.Visible = false;
            lblWarning.Visible = false;
            txtDistance.Focus();
        }

        private Panel divider;
    }
}