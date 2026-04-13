using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WinFormsApp4
{
    public partial class Form1 : Form
    {
        private Button[,] board = new Button[3, 3];
        private bool isXTurn = true;
        private int moveCount = 0;
        private Label lblGameStatus;

        private TextBox txtFileContent;
        private Button btnOpenFile;
        private Button btnSaveFile;
        private Label lblFileName;
        private string currentFilePath = "";

        private Button btnRunaway;
        private Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            this.Text = "WinFormsApp4";
            this.AutoScaleMode = AutoScaleMode.None;
            this.ClientSize = new Size(1150, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 245, 255);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // ══════════════════════════════════════
            // ЛІВА КОЛОНКА (x: 10..230)
            // ══════════════════════════════════════

            // --- Секція 1: кнопка що тікає ---
            AddLabel("1. Кнопка, що тікає", 10, 10, 220, FontStyle.Bold);

            btnRunaway = new Button();
            btnRunaway.Text = "Натисни";
            btnRunaway.Size = new Size(130, 36);
            btnRunaway.Location = new Point(10, 40);
            btnRunaway.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnRunaway.BackColor = Color.FromArgb(200, 60, 60);
            btnRunaway.ForeColor = Color.White;
            btnRunaway.FlatStyle = FlatStyle.Flat;
            btnRunaway.FlatAppearance.BorderSize = 0;
            btnRunaway.MouseEnter += (s, e) => MoveRunawayButton();
            btnRunaway.Click += (s, e) =>
                MessageBox.Show("Спіймав!", "Ура!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Controls.Add(btnRunaway);

            // --- Секція 2: хрестики-ноліки ---
            AddLabel("2. Хрестики-ноліки", 10, 95, 220, FontStyle.Bold);

            lblGameStatus = new Label();
            lblGameStatus.Text = "Хід: X";
            lblGameStatus.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblGameStatus.ForeColor = Color.FromArgb(40, 80, 160);
            lblGameStatus.Location = new Point(10, 120);
            lblGameStatus.Size = new Size(220, 20);
            this.Controls.Add(lblGameStatus);

            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                {
                    Button cell = new Button();
                    cell.Size = new Size(58, 58);
                    cell.Location = new Point(10 + c * 62, 145 + r * 62);
                    cell.Font = new Font("Segoe UI", 18, FontStyle.Bold);
                    cell.BackColor = Color.White;
                    cell.FlatStyle = FlatStyle.Flat;
                    cell.FlatAppearance.BorderColor = Color.FromArgb(40, 80, 160);
                    cell.Tag = new int[] { r, c };
                    cell.Click += Cell_Click;
                    board[r, c] = cell;
                    this.Controls.Add(cell);
                }

            Button btnReset = new Button();
            btnReset.Text = "Нова гра";
            btnReset.Location = new Point(10, 355);
            btnReset.Size = new Size(100, 30);
            btnReset.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnReset.BackColor = Color.FromArgb(40, 120, 80);
            btnReset.ForeColor = Color.White;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.Click += btnResetGame_Click;
            this.Controls.Add(btnReset);

            // ══════════════════════════════════════
            // ПРАВА КОЛОНКА (x: 250..860)
            // ══════════════════════════════════════
            // Вертикальний роздільник
            Panel divider = new Panel();
            divider.BackColor = Color.FromArgb(200, 200, 220);
            divider.Location = new Point(240, 0);
            divider.Size = new Size(2, this.ClientSize.Height);
            this.Controls.Add(divider);

            AddLabel("3. Редактор текстового файлу", 255, 0, 600, FontStyle.Bold);

            btnOpenFile = new Button();
            btnOpenFile.Text = " Відкрити файл";
            btnOpenFile.Location = new Point(255, 38);
            btnOpenFile.Size = new Size(150, 40);
            btnOpenFile.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnOpenFile.BackColor = Color.FromArgb(40, 80, 160);
            btnOpenFile.ForeColor = Color.White;
            btnOpenFile.FlatStyle = FlatStyle.Flat;
            btnOpenFile.FlatAppearance.BorderSize = 0;
            btnOpenFile.Click += btnOpenFile_Click;
            this.Controls.Add(btnOpenFile);

            btnSaveFile = new Button();
            btnSaveFile.Text = "Зберегти";
            btnSaveFile.Location = new Point(415, 38);
            btnSaveFile.Size = new Size(120, 32);
            btnSaveFile.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnSaveFile.BackColor = Color.FromArgb(40, 120, 80);
            btnSaveFile.ForeColor = Color.White;
            btnSaveFile.FlatStyle = FlatStyle.Flat;
            btnSaveFile.FlatAppearance.BorderSize = 0;
            btnSaveFile.Enabled = false;
            btnSaveFile.Click += btnSaveFile_Click;
            this.Controls.Add(btnSaveFile);

            lblFileName = new Label();
            lblFileName.Text = "Файл не обрано";
            lblFileName.Font = new Font("Segoe UI", 8);
            lblFileName.ForeColor = Color.Gray;
            lblFileName.Location = new Point(255, 76);
            lblFileName.Size = new Size(550, 18);
            this.Controls.Add(lblFileName);

            txtFileContent = new TextBox();
            txtFileContent.Location = new Point(255, 98);
            txtFileContent.Size = new Size(780, 560);
            txtFileContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtFileContent.Multiline = true;
            txtFileContent.ScrollBars = ScrollBars.Both;
            txtFileContent.Font = new Font("Consolas", 10);
            txtFileContent.BackColor = Color.FromArgb(30, 30, 40);
            txtFileContent.ForeColor = Color.FromArgb(200, 220, 255);
            txtFileContent.BorderStyle = BorderStyle.FixedSingle;
            txtFileContent.Enabled = false;
            txtFileContent.Text = "Відкрийте файл для редагування...";
            this.Controls.Add(txtFileContent);
        }

        // ══════════════════════════════════════
        // ЛОГІКА
        // ══════════════════════════════════════

        private void MoveRunawayButton()
        {
            int maxX = this.ClientSize.Width - btnRunaway.Width - 5;
            int maxY = this.ClientSize.Height - btnRunaway.Height - 5;
            // Тримаємо в лівій колонці (до x=230)
            int newX = rnd.Next(5, Math.Min(maxX, 100));
            int newY = rnd.Next(5, Math.Min(maxY, 550));
            btnRunaway.Location = new Point(newX, newY);
        }

        private void Cell_Click(object sender, EventArgs e)
        {
            Button cell = (Button)sender;
            if (cell.Text != "") return;

            cell.Text = isXTurn ? "X" : "O";
            cell.ForeColor = isXTurn ? Color.FromArgb(200, 60, 60) : Color.FromArgb(40, 100, 200);
            moveCount++;

            if (CheckWinner(cell.Text))
            {
                lblGameStatus.Text = $"Переміг: {cell.Text} ";
                DisableBoard();
                MessageBox.Show($"Переміг гравець {cell.Text}!", "Гра закінчена",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (moveCount == 9)
            {
                lblGameStatus.Text = "Нічия!";
                MessageBox.Show("Нічия!", "Гра закінчена",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            isXTurn = !isXTurn;
            lblGameStatus.Text = $"Хід: {(isXTurn ? "X" : "O")}";
        }

        private bool CheckWinner(string m)
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0].Text == m && board[i, 1].Text == m && board[i, 2].Text == m) return true;
                if (board[0, i].Text == m && board[1, i].Text == m && board[2, i].Text == m) return true;
            }
            if (board[0, 0].Text == m && board[1, 1].Text == m && board[2, 2].Text == m) return true;
            if (board[0, 2].Text == m && board[1, 1].Text == m && board[2, 0].Text == m) return true;
            return false;
        }

        private void DisableBoard()
        {
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    board[r, c].Enabled = false;
        }

        private void btnResetGame_Click(object sender, EventArgs e)
        {
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                {
                    board[r, c].Text = "";
                    board[r, c].Enabled = true;
                    board[r, c].BackColor = Color.White;
                }
            isXTurn = true;
            moveCount = 0;
            lblGameStatus.Text = "Хід: X";
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        currentFilePath = ofd.FileName;
                        txtFileContent.Text = File.ReadAllText(currentFilePath);
                        txtFileContent.Enabled = true;
                        btnSaveFile.Enabled = true;
                        lblFileName.Text =  currentFilePath;
                        lblFileName.ForeColor = Color.FromArgb(40, 120, 80);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Помилка: " + ex.Message, "Помилка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(currentFilePath, txtFileContent.Text);
                MessageBox.Show("Файл збережено!", "Збережено",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message, "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddLabel(string text, int x, int y, int width, FontStyle style)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 10, style);
            lbl.ForeColor = Color.FromArgb(30, 60, 140);
            lbl.Location = new Point(x, y);
            lbl.Size = new Size(width, 22);
            this.Controls.Add(lbl);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}