using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WinFormsApp8
{
    public partial class Form1 : Form
    {
        const int ROWS = 8;
        const int COLS = 10;
        const int BTN_SIZE = 50;
        const int BTN_GAP = 5;
        const int LEFT_OFFSET = 60;
        const int TOP_OFFSET = 80;

        Button[,] seats = new Button[ROWS, COLS];
        bool[,] isOccupied = new bool[ROWS, COLS];
        bool[,] isSelected = new bool[ROWS, COLS];

        // Ціна для кожного ряду
        int[] rowPrices = { 80, 80, 100, 100, 120, 120, 150, 150 };

        Label lblTitle, lblLegend, lblTotal, lblScreen;
        Button btnBook;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Бронювання квитків у кінотеатр";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(30, 30, 40);

            int formWidth = LEFT_OFFSET + COLS * (BTN_SIZE + BTN_GAP) + 60;
            int formHeight = TOP_OFFSET + ROWS * (BTN_SIZE + BTN_GAP) + 220;
            this.ClientSize = new Size(formWidth, formHeight);

            BuildUI();
            GenerateOccupied();
            RenderSeats();
            UpdateTotal();
        }

        void BuildUI()
        {
            // Екран
            lblScreen = new Label()
            {
                Text = "screen",
                ForeColor = Color.White,
                BackColor = Color.FromArgb(60, 60, 80),
                Font = new Font("Arial", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(COLS * (BTN_SIZE + BTN_GAP) - 5, 35),
                Location = new Point(LEFT_OFFSET, 15)
            };
            this.Controls.Add(lblScreen);

            // Номери рядів
            for (int r = 0; r < ROWS; r++)
            {
                var lbl = new Label()
                {
                    Text = $"{r + 1}",
                    ForeColor = Color.LightGray,
                    Font = new Font("Arial", 9),
                    TextAlign = ContentAlignment.MiddleRight,
                    Size = new Size(45, BTN_SIZE),
                    Location = new Point(5, TOP_OFFSET + r * (BTN_SIZE + BTN_GAP))
                };
                this.Controls.Add(lbl);

                // Ціна ряду справа
                var lblPrice = new Label()
                {
                    Text = $"{rowPrices[r]}грн",
                    ForeColor = Color.Gold,
                    Font = new Font("Arial", 8),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Size = new Size(50, BTN_SIZE),
                    Location = new Point(LEFT_OFFSET + COLS * (BTN_SIZE + BTN_GAP) + 3,
                                        TOP_OFFSET + r * (BTN_SIZE + BTN_GAP))
                };
                this.Controls.Add(lblPrice);
            }

            // Легенда
            int legendY = TOP_OFFSET + ROWS * (BTN_SIZE + BTN_GAP) + 10;

            AddLegendItem(LEFT_OFFSET, legendY, Color.FromArgb(50, 120, 50), "Вільне");
            AddLegendItem(LEFT_OFFSET + 110, legendY, Color.FromArgb(200, 160, 0), "Обрано");
            AddLegendItem(LEFT_OFFSET + 220, legendY, Color.FromArgb(100, 30, 30), "Зайняте");

            //вартість
            lblTotal = new Label()
            {
                Text = "Обрано: 0 місць | Сума: 0 грн",
                ForeColor = Color.White,
                Font = new Font("Arial", 11, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(this.ClientSize.Width - 20, 30),
                Location = new Point(10, legendY + 40)
            };
            this.Controls.Add(lblTotal);

            //бронювання
            btnBook = new Button()
            {
                Text = "Забронювати",
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(180, 30, 30),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(220, 45),
                Location = new Point(this.ClientSize.Width / 2 - 110, legendY + 80)
            };
            btnBook.FlatAppearance.BorderSize = 0;
            btnBook.Click += BtnBook_Click;
            this.Controls.Add(btnBook);
        }

        void AddLegendItem(int x, int y, Color color, string text)
        {
            var box = new Panel()
            {
                BackColor = color,
                Size = new Size(20, 20),
                Location = new Point(x, y + 5)
            };
            this.Controls.Add(box);

            var lbl = new Label()
            {
                Text = text,
                ForeColor = Color.LightGray,
                Font = new Font("Arial", 9),
                Location = new Point(x + 25, y + 5),
                Size = new Size(75, 20)
            };
            this.Controls.Add(lbl);
        }

        void GenerateOccupied()
        {
            // зайняті місця
            var rng = new Random(42);
            for (int r = 0; r < ROWS; r++)
                for (int c = 0; c < COLS; c++)
                    if (rng.NextDouble() < 0.25)
                        isOccupied[r, c] = true;
        }

        void RenderSeats()
        {
            for (int r = 0; r < ROWS; r++)
            {
                for (int c = 0; c < COLS; c++)
                {
                    if (seats[r, c] == null)
                    {
                        int row = r, col = c;
                        var btn = new Button()
                        {
                            Size = new Size(BTN_SIZE, BTN_SIZE),
                            Location = new Point(
                                LEFT_OFFSET + col * (BTN_SIZE + BTN_GAP),
                                TOP_OFFSET + row * (BTN_SIZE + BTN_GAP)),
                            FlatStyle = FlatStyle.Flat,
                            Font = new Font("Arial", 7),
                            Text = $"{row + 1}-{col + 1}"
                        };
                        btn.FlatAppearance.BorderSize = 1;
                        btn.Click += (s, e) => SeatClick(row, col);
                        seats[r, c] = btn;
                        this.Controls.Add(btn);
                    }

                    UpdateSeatColor(r, c);
                }
            }
        }

        void UpdateSeatColor(int r, int c)
        {
            var btn = seats[r, c];
            if (isOccupied[r, c])
            {
                btn.BackColor = Color.FromArgb(100, 30, 30);
                btn.ForeColor = Color.FromArgb(150, 60, 60);
                btn.FlatAppearance.BorderColor = Color.FromArgb(120, 40, 40);
                btn.Enabled = false;
                btn.Text = "✖";
                btn.Font = new Font("Arial", 10, FontStyle.Bold);
            }
            else if (isSelected[r, c])
            {
                btn.BackColor = Color.FromArgb(200, 160, 0);
                btn.ForeColor = Color.Black;
                btn.FlatAppearance.BorderColor = Color.Gold;
                btn.Text = "✔";
                btn.Font = new Font("Arial", 10, FontStyle.Bold);
            }
            else
            {
                btn.BackColor = Color.FromArgb(50, 120, 50);
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderColor = Color.FromArgb(70, 150, 70);
                btn.Text = $"{r + 1}-{c + 1}";
                btn.Font = new Font("Arial", 7);
            }
        }

        void SeatClick(int r, int c)
        {
            isSelected[r, c] = !isSelected[r, c];
            UpdateSeatColor(r, c);
            UpdateTotal();
        }

        void UpdateTotal()
        {
            int count = 0;
            int total = 0;
            for (int r = 0; r < ROWS; r++)
                for (int c = 0; c < COLS; c++)
                    if (isSelected[r, c])
                    {
                        count++;
                        total += rowPrices[r];
                    }

            lblTotal.Text = $"Обрано: {count} місць | Сума: {total} ₴";
            btnBook.Enabled = count > 0;
            btnBook.BackColor = count > 0
                ? Color.FromArgb(180, 30, 30)
                : Color.FromArgb(80, 80, 80);
        }

        void BtnBook_Click(object sender, EventArgs e)
        {
            var selected = new List<string>();
            int total = 0;

            for (int r = 0; r < ROWS; r++)
                for (int c = 0; c < COLS; c++)
                    if (isSelected[r, c])
                    {
                        selected.Add($"Ряд {r + 1}, Місце {c + 1} — {rowPrices[r]} ₴");
                        total += rowPrices[r];
                        isOccupied[r, c] = true;
                        isSelected[r, c] = false;
                        UpdateSeatColor(r, c);
                    }

            if (selected.Count == 0) return;

            // збереження у файл
            string filePath = "booking.txt";
            using (var sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine($"=== Бронювання від {DateTime.Now:dd.MM.yyyy HH:mm} ===");
                foreach (var s in selected)
                    sw.WriteLine(s);
                sw.WriteLine($"Загальна сума: {total} ₴");
                sw.WriteLine();
            }

            UpdateTotal();

            MessageBox.Show(
                $"Заброньовано {selected.Count} місць!\n" +
                $"Загальна сума: {total} грн\n\n" +
                $"Деталі збережено у файл:\n{Path.GetFullPath(filePath)}",
                "Бронювання успішне",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}