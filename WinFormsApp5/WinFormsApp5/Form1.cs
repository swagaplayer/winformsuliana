using System;
using System.Drawing;
using System.Windows.Forms;

    namespace WinFormsApp5
{
    public partial class Form1 : Form
    {
        TextBox txtName, txtNum1, txtNum2;
        Label lblGreeting, lblResult, lblHidden;
        CheckBox chkShow;
        Panel movingPanel;

        public Form1()
        {
          
            BuildUI();
        }

        private void BuildUI()
        {
            
            this.Size = new Size(800, 800);
            this.StartPosition = FormStartPosition.CenterScreen;

            // 1. 
            Button btnColor = new Button();
            btnColor.Text = "Змінити фон";
            btnColor.Location = new Point(20, 20);
            btnColor.Click += (s, e) =>
            {
                this.BackColor = Color.FromArgb(
                    new Random().Next(256),
                    new Random().Next(256),
                    new Random().Next(256));
            };
            Controls.Add(btnColor);

            // 2. ВІТАННЯ
            txtName = new TextBox();
            txtName.Location = new Point(20, 70);
            Controls.Add(txtName);

            Button btnGreet = new Button();
            btnGreet.Text = "Привітати";
            btnGreet.Location = new Point(150, 70);
            btnGreet.Click += (s, e) =>
            {
                lblGreeting.Text = "Привіт, " + txtName.Text;
            };
            Controls.Add(btnGreet);

            lblGreeting = new Label();
            lblGreeting.Location = new Point(20, 100);
            lblGreeting.Size = new Size(200, 30);
            Controls.Add(lblGreeting);

            // 3. ДОДАВАННЯ
            txtNum1 = new TextBox();
            txtNum1.Location = new Point(20, 150);
            Controls.Add(txtNum1);

            txtNum2 = new TextBox();
            txtNum2.Location = new Point(120, 150);
            Controls.Add(txtNum2);

            Button btnAdd = new Button();
            btnAdd.Text = "+";
            btnAdd.Location = new Point(220, 150);
            btnAdd.Click += (s, e) =>
            {
                int a = int.Parse(txtNum1.Text);
                int b = int.Parse(txtNum2.Text);
                lblResult.Text = "Результат: " + (a + b);
            };
            Controls.Add(btnAdd);

            lblResult = new Label();
            lblResult.Location = new Point(20, 180);
            lblResult.Size = new Size(200, 30);
            Controls.Add(lblResult);

            // 4. CHECKBOX
            chkShow = new CheckBox();
            chkShow.Text = "Показати текст";
            chkShow.Location = new Point(20, 230);
            chkShow.CheckedChanged += (s, e) =>
            {
                lblHidden.Visible = chkShow.Checked;
            };
            Controls.Add(chkShow);

            lblHidden = new Label();
            lblHidden.Text = "Секретний текст ";
            lblHidden.Location = new Point(20, 260);
            lblHidden.Visible = false;
            Controls.Add(lblHidden);

            // 5. РУХ ОБ'ЄКТА
            movingPanel = new Panel();
            movingPanel.Size = new Size(100, 100  );
            movingPanel.BackColor = Color.Red;
            movingPanel.Location = new Point(500, 200);
            Controls.Add(movingPanel);

            Button btnUp = new Button();
            btnUp.Text = "↑";
            btnUp.Location = new Point(500, 150);
            btnUp.Click += (s, e) => movingPanel.Top -= 10;
            Controls.Add(btnUp);

            Button btnDown = new Button();
            btnDown.Text = "↓";
            btnDown.Location = new Point(500, 260);
            btnDown.Click += (s, e) => movingPanel.Top += 10;
            Controls.Add(btnDown);

            Button btnLeft = new Button();
            btnLeft.Text = "←";
            btnLeft.Location = new Point(450, 200);
            btnLeft.Click += (s, e) => movingPanel.Left -= 10;
            Controls.Add(btnLeft);

            Button btnRight = new Button();
            btnRight.Text = "→";
            btnRight.Location = new Point(560, 200);
            btnRight.Click += (s, e) => movingPanel.Left += 10;
            Controls.Add(btnRight);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}