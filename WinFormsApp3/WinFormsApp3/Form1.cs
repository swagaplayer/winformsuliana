using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        private Label lblTitle;
        private Label lblNum1, lblNum2, lblNum3;
        private TextBox txtNum1, txtNum2, txtNum3;
        private Button btnAnalyze;
        private Button btnClear;
        private Panel pnlResults;

        // Result labels
        private Label lblSum, lblProduct, lblMax, lblMin, lblEqual;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Аналіз трьох чисел";
            this.Size = new Size(500, 470);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(248, 245, 255);

            // ── Title ──────────────────────────────────────
            lblTitle = new Label();
            lblTitle.Text = "🔢  Аналіз трьох чисел";
            lblTitle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(80, 30, 150);
            lblTitle.Location = new Point(20, 18);
            lblTitle.Size = new Size(440, 32);

            Panel divider = new Panel();
            divider.BackColor = Color.FromArgb(80, 30, 150);
            divider.Location = new Point(20, 55);
            divider.Size = new Size(440, 2);
            this.Controls.Add(divider);

            Label lblHint = new Label();
            lblHint.Text = "Введіть будь-які числа (цілі або дробові):";
            lblHint.Font = new Font("Segoe UI", 9.5f);
            lblHint.ForeColor = Color.FromArgb(100, 80, 140);
            lblHint.Location = new Point(20, 68);
            lblHint.Size = new Size(380, 22);
            this.Controls.Add(lblHint);

            // ── Input fields ───────────────────────────────
            int[] xPos = { 20, 175, 330 };
            Label[] labels = {
                new Label(), new Label(), new Label()
            };
            TextBox[] fields = {
                new TextBox(), new TextBox(), new TextBox()
            };

            string[] names = { "Перше число", "Друге число", "Третє число" };
            lblNum1 = labels[0]; lblNum2 = labels[1]; lblNum3 = labels[2];
            txtNum1 = fields[0]; txtNum2 = fields[1]; txtNum3 = fields[2];

            for (int i = 0; i < 3; i++)
            {
                // Card background
                Panel card = new Panel();
                card.BackColor = Color.FromArgb(237, 232, 255);
                card.Location = new Point(xPos[i], 98);
                card.Size = new Size(130, 80);
                card.Paint += (s, ev) =>
                    ev.Graphics.DrawRectangle(
                        new Pen(Color.FromArgb(80, 30, 150), 1),
                        0, 0, 129, 79);
                this.Controls.Add(card);

                labels[i].Text = names[i];
                labels[i].Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
                labels[i].ForeColor = Color.FromArgb(80, 30, 150);
                labels[i].Location = new Point(8, 10);
                labels[i].Size = new Size(114, 18);
                labels[i].TextAlign = ContentAlignment.MiddleCenter;
                card.Controls.Add(labels[i]);

                fields[i].Location = new Point(8, 35);
                fields[i].Size = new Size(114, 32);
                fields[i].Font = new Font("Segoe UI", 14, FontStyle.Bold);
                fields[i].TextAlign = HorizontalAlignment.Center;
                fields[i].BorderStyle = BorderStyle.FixedSingle;
                card.Controls.Add(fields[i]);
            }

            // ── Buttons ────────────────────────────────────
            btnAnalyze = new Button();
            btnAnalyze.Text = "📊  Аналізувати числа";
            btnAnalyze.Location = new Point(20, 198);
            btnAnalyze.Size = new Size(270, 40);
            btnAnalyze.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnAnalyze.BackColor = Color.FromArgb(80, 30, 150);
            btnAnalyze.ForeColor = Color.White;
            btnAnalyze.FlatStyle = FlatStyle.Flat;
            btnAnalyze.FlatAppearance.BorderSize = 0;
            btnAnalyze.Cursor = Cursors.Hand;
            btnAnalyze.Click += new EventHandler(btnAnalyze_Click);

            btnClear = new Button();
            btnClear.Text = "✖  Очистити";
            btnClear.Location = new Point(305, 198);
            btnClear.Size = new Size(155, 40);
            btnClear.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnClear.BackColor = Color.FromArgb(170, 50, 50);
            btnClear.ForeColor = Color.White;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Cursor = Cursors.Hand;
            btnClear.Click += new EventHandler(btnClear_Click);

            // ── Results panel ──────────────────────────────
            pnlResults = new Panel();
            pnlResults.Location = new Point(20, 255);
            pnlResults.Size = new Size(440, 168);
            pnlResults.BackColor = Color.FromArgb(237, 232, 255);
            pnlResults.Paint += (s, ev) =>
                ev.Graphics.DrawRectangle(
                    new Pen(Color.FromArgb(80, 30, 150), 2),
                    0, 0, 439, 167);
            pnlResults.Visible = false;

            // Header inside panel
            Label panelHeader = new Label();
            panelHeader.Text = "  РЕЗУЛЬТАТИ АНАЛІЗУ";
            panelHeader.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            panelHeader.ForeColor = Color.White;
            panelHeader.BackColor = Color.FromArgb(80, 30, 150);
            panelHeader.Location = new Point(2, 2);
            panelHeader.Size = new Size(436, 24);
            pnlResults.Controls.Add(panelHeader);

            // Result rows
            lblSum     = MakeResultLabel(30, 35);
            lblProduct = MakeResultLabel(30, 68);
            lblMax     = MakeResultLabel(240, 35);
            lblMin     = MakeResultLabel(240, 68);
            lblEqual   = MakeResultLabel(30, 105);
            lblEqual.Size = new Size(400, 26);
            lblEqual.ForeColor = Color.FromArgb(30, 100, 160);
            lblEqual.Font = new Font("Segoe UI", 10, FontStyle.Bold | FontStyle.Italic);
            lblEqual.Visible = false;

            pnlResults.Controls.AddRange(new Control[] {
                lblSum, lblProduct, lblMax, lblMin, lblEqual
            });

            // ── Add all ────────────────────────────────────
            this.Controls.AddRange(new Control[] {
                lblTitle,
                btnAnalyze, btnClear,
                pnlResults
            });
        }

        private Label MakeResultLabel(int x, int y)
        {
            Label lbl = new Label();
            lbl.Font = new Font("Segoe UI", 10);
            lbl.ForeColor = Color.FromArgb(40, 20, 80);
            lbl.Location = new Point(x, y);
            lbl.Size = new Size(200, 28);
            lbl.AutoSize = false;
            return lbl;
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            pnlResults.Visible = false;

            try
            {
                // Parse with comma/dot support
                double n1 = ParseNumber(txtNum1.Text, "Перше число");
                double n2 = ParseNumber(txtNum2.Text, "Друге число");
                double n3 = ParseNumber(txtNum3.Text, "Третє число");

                double sum = n1 + n2 + n3;
                double product = n1 * n2 * n3;
                double max = Math.Max(n1, Math.Max(n2, n3));
                double min = Math.Min(n1, Math.Min(n2, n3));
                bool allEqual = (n1 == n2) && (n2 == n3);

                // Format nicely (no trailing zeros for whole numbers)
                string Fmt(double v) => v == Math.Floor(v) ? v.ToString("F0") : v.ToString("F4").TrimEnd('0');

                lblSum.Text     = $"➕ Сума:       {Fmt(sum)}";
                lblProduct.Text = $"✖  Добуток:  {Fmt(product)}";
                lblMax.Text     = $"⬆  Найбільше: {Fmt(max)}";
                lblMin.Text     = $"⬇  Найменше:  {Fmt(min)}";

                lblEqual.Visible = allEqual;
                if (allEqual)
                    lblEqual.Text = $"ℹ  Усі три числа однакові: {Fmt(n1)}";

                pnlResults.Visible = true;

                if (allEqual)
                {
                    MessageBox.Show(
                        $"Усі три числа однакові: {Fmt(n1)}",
                        "Однакові числа",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Порожнє поле",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Нечислові дані",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Несподівана помилка: " + ex.Message, "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private double ParseNumber(string raw, string fieldName)
        {
            raw = raw.Trim().Replace(",", ".");

            if (string.IsNullOrEmpty(raw))
                throw new ArgumentException($"«{fieldName}» не може бути порожнім.");

            if (!double.TryParse(raw,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out double result))
                throw new FormatException(
                    $"«{fieldName}»: значення «{raw}» — не є числом.\nВведіть ціле або дробове число.");

            return result;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtNum1.Clear();
            txtNum2.Clear();
            txtNum3.Clear();
            pnlResults.Visible = false;
            txtNum1.Focus();
        }

        // Добавлен отсутствующий обработчик события загрузки формы
        private void Form1_Load(object sender, EventArgs e)
        {
            // Установим фокус на первое поле ввода при загрузке формы
            txtNum1?.Focus();
        }

    }
}