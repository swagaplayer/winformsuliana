using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        private Label lblTitle;
        private Label[] lblItems = new Label[5];
        private TextBox[] txtItems = new TextBox[5];
        private Button btnCheck;
        private Button btnClear;
        private Panel pnlOutput;
        private Label lblOutputTitle;
        private RichTextBox rtbOutput;
        private Label lblCount;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Перевірка списку покупок";
            this.Size = new Size(520,620 );
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(245, 250, 245);

            // ── Title ──────────────────────────────────────
            lblTitle = new Label();
            lblTitle.Text = "🛒  Список покупок";
            lblTitle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 110, 50);
            lblTitle.Location = new Point(20, 18);
            lblTitle.Size = new Size(460, 32);

            Panel divider = new Panel();
            divider.BackColor = Color.FromArgb(30, 110, 50);
            divider.Location = new Point(20, 55);
            divider.Size = new Size(460, 2);

            Label lblSubtitle = new Label();
            lblSubtitle.Text = "Введіть назви 5 товарів:";
            lblSubtitle.Font = new Font("Segoe UI", 10);
            lblSubtitle.ForeColor = Color.FromArgb(60, 80, 60);
            lblSubtitle.Location = new Point(20, 68);
            lblSubtitle.Size = new Size(300, 22);
            this.Controls.Add(lblSubtitle);

            // ── 5 Item fields ──────────────────────────────
            string[] placeholders = {
                "Наприклад: Молоко",
                "Наприклад: Хліб",
                "Наприклад: Яйця",
                "Наприклад: Масло",
                "Наприклад: Сир"
            };

            for (int i = 0; i < 5; i++)
            {
                // Number badge
                Panel badge = new Panel();
                badge.BackColor = Color.FromArgb(30, 110, 50);
                badge.Location = new Point(20, 100 + i * 46);
                badge.Size = new Size(28, 28);
                this.Controls.Add(badge);

                Label badgeNum = new Label();
                badgeNum.Text = (i + 1).ToString();
                badgeNum.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                badgeNum.ForeColor = Color.White;
                badgeNum.TextAlign = ContentAlignment.MiddleCenter;
                badgeNum.Location = new Point(0, 0);
                badgeNum.Size = new Size(28, 28);
                badge.Controls.Add(badgeNum);

                lblItems[i] = new Label();
                lblItems[i].Text = $"Товар {i + 1}:";
                lblItems[i].Font = new Font("Segoe UI", 10);
                lblItems[i].ForeColor = Color.FromArgb(40, 70, 40);
                lblItems[i].Location = new Point(58, 104 + i * 46);
                lblItems[i].Size = new Size(100, 22);
                this.Controls.Add(lblItems[i]);

                txtItems[i] = new TextBox();
                txtItems[i].Location = new Point(165, 101 + i * 46);
                txtItems[i].Size = new Size(320, 28);
                txtItems[i].Font = new Font("Segoe UI", 10);
                txtItems[i].ForeColor = Color.FromArgb(40, 70, 40);
                this.Controls.Add(txtItems[i]);

                this.Controls.Add(badge);
            }

            // ── Buttons ────────────────────────────────────
            btnCheck = new Button();
            btnCheck.Text = "✔  Перевірити список";
            btnCheck.Location = new Point(20, 340);
            btnCheck.Size = new Size(240, 38);
            btnCheck.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnCheck.BackColor = Color.FromArgb(30, 110, 50);
            btnCheck.ForeColor = Color.White;
            btnCheck.FlatStyle = FlatStyle.Flat;
            btnCheck.FlatAppearance.BorderSize = 0;
            btnCheck.Cursor = Cursors.Hand;
            btnCheck.Click += new EventHandler(btnCheck_Click);

            btnClear = new Button();
            btnClear.Text = "✖  Очистити";
            btnClear.Location = new Point(275, 340);
            btnClear.Size = new Size(200, 38);
            btnClear.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnClear.BackColor = Color.FromArgb(170, 50, 50);
            btnClear.ForeColor = Color.White;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Cursor = Cursors.Hand;
            btnClear.Click += new EventHandler(btnClear_Click);

            // ── Output panel ───────────────────────────────
            pnlOutput = new Panel();
            pnlOutput.Location = new Point(20, 392);
            pnlOutput.Size = new Size(460, 100);
            pnlOutput.BackColor = Color.FromArgb(230, 245, 232);
            pnlOutput.BorderStyle = BorderStyle.None;
            pnlOutput.Paint += (s, e) =>
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(30, 110, 50), 1),
                    0, 0, pnlOutput.Width - 1, pnlOutput.Height - 1);
            pnlOutput.Visible = false;

            lblOutputTitle = new Label();
            lblOutputTitle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblOutputTitle.ForeColor = Color.FromArgb(30, 110, 50);
            lblOutputTitle.Location = new Point(10, 8);
            lblOutputTitle.Size = new Size(440, 20);

            rtbOutput = new RichTextBox();
            rtbOutput.Location = new Point(10, 30);
            rtbOutput.Size = new Size(440, 60);
            rtbOutput.Font = new Font("Segoe UI", 9.5f);
            rtbOutput.BackColor = Color.FromArgb(230, 245, 232);
            rtbOutput.ForeColor = Color.FromArgb(30, 70, 30);
            rtbOutput.ReadOnly = true;
            rtbOutput.BorderStyle = BorderStyle.None;
            rtbOutput.ScrollBars = RichTextBoxScrollBars.None;

            pnlOutput.Controls.Add(lblOutputTitle);
            pnlOutput.Controls.Add(rtbOutput);

            // ── Add all ────────────────────────────────────
            this.Controls.AddRange(new Control[] {
                lblTitle, divider,
                btnCheck, btnClear,
                pnlOutput
            });
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            pnlOutput.Visible = false;

            // Find empty fields
            StringBuilder emptyList = new StringBuilder();
            for (int i = 0; i < 5; i++)
            {
                if (string.IsNullOrWhiteSpace(txtItems[i].Text))
                    emptyList.AppendLine($"  • Поле «Товар {i + 1}» порожнє");
            }

            if (emptyList.Length > 0)
            {
                MessageBox.Show(
                    "Виявлено порожні поля:\n\n" + emptyList.ToString() +
                    "\nБудь ласка, заповніть усі 5 полів.",
                    "Є порожні поля",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Highlight empty fields
                for (int i = 0; i < 5; i++)
                {
                    txtItems[i].BackColor = string.IsNullOrWhiteSpace(txtItems[i].Text)
                        ? Color.FromArgb(255, 220, 220)
                        : Color.White;
                }
                return;
            }

            // Reset highlight
            foreach (TextBox tb in txtItems) tb.BackColor = Color.White;

            // Collect items
            int count = 0;
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < 5; i++)
            {
                string item = txtItems[i].Text.Trim();
                if (!string.IsNullOrEmpty(item))
                {
                    count++;
                    output.Append($"  {count}. {item}");
                    if (i < 4) output.Append("   |   ");
                }
            }

            lblOutputTitle.Text = $"✔  Всього товарів введено: {count}";
            rtbOutput.Text = output.ToString();
            pnlOutput.Visible = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (TextBox tb in txtItems)
            {
                tb.Clear();
                tb.BackColor = Color.White;
            }
            pnlOutput.Visible = false;
            txtItems[0].Focus();
        }

        
    }
}