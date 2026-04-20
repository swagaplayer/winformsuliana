namespace WinFormsApp7
{
    public partial class Form1 : Form
    {
        const int ROWS = 9, COLS = 9, MINES = 10;
        const int CELL_SIZE = 40;

        Button[,] buttons;
        int[,] board;
        bool[,] revealed;
        bool[,] flagged;
        bool gameOver = false;
        bool firstClick = true;
        int minesLeft;

        System.Windows.Forms.Timer timer;
        int seconds = 0;

        Label lblMines, lblTimer;
        Button btnReset;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Сапер";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            BuildUI();
            NewGame();
        }

        void BuildUI()
        {
            int topHeight = 60;
            this.ClientSize = new Size(COLS * CELL_SIZE + 20, ROWS * CELL_SIZE + topHeight + 20);

            lblMines = new Label()
            {
                Location = new Point(15, 15),
                Size = new Size(80, 30),
                Font = new Font("Consolas", 14, FontStyle.Bold),
                ForeColor = Color.Red,
                TextAlign = ContentAlignment.MiddleLeft
            };
            this.Controls.Add(lblMines);

            btnReset = new Button()
            {
                Location = new Point(this.ClientSize.Width / 2 - 20, 12),
                Size = new Size(40, 36),
                Font = new Font("Segoe UI Emoji", 14),
                Text = "🙂",
                FlatStyle = FlatStyle.Flat
            };
            btnReset.FlatAppearance.BorderSize = 1;
            btnReset.Click += (s, e) => NewGame();
            this.Controls.Add(btnReset);

            lblTimer = new Label()
            {
                Location = new Point(this.ClientSize.Width - 95, 15),
                Size = new Size(80, 30),
                Font = new Font("Consolas", 14, FontStyle.Bold),
                ForeColor = Color.Red,
                TextAlign = ContentAlignment.MiddleRight
            };
            this.Controls.Add(lblTimer);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += (s, e) =>
            {
                seconds++;
                lblTimer.Text = seconds.ToString("D3");
            };

            buttons = new Button[ROWS, COLS];
            for (int r = 0; r < ROWS; r++)
            {
                for (int c = 0; c < COLS; c++)
                {
                    int row = r, col = c;
                    var btn = new Button()
                    {
                        Location = new Point(10 + col * CELL_SIZE, topHeight + row * CELL_SIZE),
                        Size = new Size(CELL_SIZE, CELL_SIZE),
                        Font = new Font("Consolas", 11, FontStyle.Bold),
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.LightGray,
                        Tag = false
                    };
                    btn.FlatAppearance.BorderSize = 1;
                    btn.FlatAppearance.BorderColor = Color.Gray;
                    btn.Click += (s, e) => HandleLeftClick(row, col);
                    btn.MouseUp += (s, e) =>
                    {
                        if (e.Button == MouseButtons.Right)
                            HandleRightClick(row, col);
                    };
                    buttons[r, c] = btn;
                    this.Controls.Add(btn);
                }
            }
        }

        void NewGame()
        {
            gameOver = false;
            firstClick = true;
            seconds = 0;
            minesLeft = MINES;
            timer.Stop();

            board = new int[ROWS, COLS];
            revealed = new bool[ROWS, COLS];
            flagged = new bool[ROWS, COLS];

            lblMines.Text = minesLeft.ToString("D3");
            lblTimer.Text = "000";
            btnReset.Text = "🙂";

            for (int r = 0; r < ROWS; r++)
                for (int c = 0; c < COLS; c++)
                {
                    buttons[r, c].Text = "";
                    buttons[r, c].BackColor = Color.LightGray;
                    buttons[r, c].Enabled = true;
                    buttons[r, c].Font = new Font("Consolas", 11, FontStyle.Bold);
                }
        }

        void PlaceMines(int safeR, int safeC)
        {
            var safe = new HashSet<(int, int)>();
            for (int dr = -1; dr <= 1; dr++)
                for (int dc = -1; dc <= 1; dc++)
                    safe.Add((safeR + dr, safeC + dc));

            var rng = new Random();
            int placed = 0;
            while (placed < MINES)
            {
                int r = rng.Next(ROWS), c = rng.Next(COLS);
                if (!safe.Contains((r, c)) && board[r, c] != -1)
                {
                    board[r, c] = -1;
                    placed++;
                }
            }

            for (int r = 0; r < ROWS; r++)
                for (int c = 0; c < COLS; c++)
                    if (board[r, c] != -1)
                        board[r, c] = CountNeighborMines(r, c);
        }

        int CountNeighborMines(int r, int c)
        {
            int count = 0;
            foreach (var (nr, nc) in GetNeighbors(r, c))
                if (board[nr, nc] == -1) count++;
            return count;
        }

        List<(int, int)> GetNeighbors(int r, int c)
        {
            var list = new List<(int, int)>();
            for (int dr = -1; dr <= 1; dr++)
                for (int dc = -1; dc <= 1; dc++)
                    if ((dr != 0 || dc != 0) &&
                        r + dr >= 0 && r + dr < ROWS &&
                        c + dc >= 0 && c + dc < COLS)
                        list.Add((r + dr, c + dc));
            return list;
        }

        void HandleLeftClick(int r, int c)
        {
            if (gameOver || revealed[r, c] || flagged[r, c]) return;

            if (firstClick)
            {
                firstClick = false;
                PlaceMines(r, c);
                timer.Start();
            }

            if (board[r, c] == -1)
            {
                // Програш
                gameOver = true;
                timer.Stop();
                btnReset.Text = "😵";
                RevealAllMines(r, c);
                MessageBox.Show("💥 Підірвався! Спробуй ще.", "Програш");
                return;
            }

            Reveal(r, c);
            CheckWin();
        }

        void HandleRightClick(int r, int c)
        {
            if (gameOver || revealed[r, c]) return;

            flagged[r, c] = !flagged[r, c];

            if (flagged[r, c])
            {
                buttons[r, c].Text = "🚩";
                buttons[r, c].ForeColor = Color.Red;
                minesLeft--;
            }
            else
            {
                buttons[r, c].Text = "";
                minesLeft++;
            }

            lblMines.Text = minesLeft.ToString("D3");
        }

        void Reveal(int r, int c)
        {
            if (r < 0 || r >= ROWS || c < 0 || c >= COLS) return;
            if (revealed[r, c] || flagged[r, c]) return;

            revealed[r, c] = true;
            buttons[r, c].BackColor = Color.White;
            buttons[r, c].Enabled = false;

            if (board[r, c] > 0)
            {
                buttons[r, c].Text = board[r, c].ToString();
                buttons[r, c].ForeColor = GetNumberColor(board[r, c]);
            }

            if (board[r, c] == 0)
                foreach (var (nr, nc) in GetNeighbors(r, c))
                    Reveal(nr, nc);
        }

        Color GetNumberColor(int n) => n switch
        {
            1 => Color.Blue,
            2 => Color.Green,
            3 => Color.Red,
            4 => Color.DarkBlue,
            5 => Color.DarkRed,
            6 => Color.Teal,
            7 => Color.Purple,
            _ => Color.Gray
        };

        void RevealAllMines(int hitR, int hitC)
        {
            for (int r = 0; r < ROWS; r++)
                for (int c = 0; c < COLS; c++)
                    if (board[r, c] == -1)
                    {
                        buttons[r, c].Text = r == hitR && c == hitC ? "💥" : "💣";
                        buttons[r, c].BackColor = r == hitR && c == hitC
                            ? Color.Red : Color.LightCoral;
                        buttons[r, c].Enabled = false;
                    }
        }

        void CheckWin()
        {
            int unrevealedSafe = 0;
            for (int r = 0; r < ROWS; r++)
                for (int c = 0; c < COLS; c++)
                    if (!revealed[r, c] && board[r, c] != -1)
                        unrevealedSafe++;

            if (unrevealedSafe == 0)
            {
                gameOver = true;
                timer.Stop();
                btnReset.Text = "😎";
                MessageBox.Show($"🎉 Перемога! Час: {seconds} сек.", "Виграш!");
            }
        }
    }
}