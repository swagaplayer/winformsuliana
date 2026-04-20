namespace WinFormsApp6
{
    public partial class Form1 : Form
    {
        List<string> images = new List<string>()
        {
            @"C:\Users\Redmi\source\repos\swagaplayer\winformsuliana\WinFormsApp6\WinFormsApp6\baranova.jpg",
            @"C:\Users\Redmi\source\repos\swagaplayer\winformsuliana\WinFormsApp6\WinFormsApp6\kotost.jpg",
            @"C:\Users\Redmi\source\repos\swagaplayer\winformsuliana\WinFormsApp6\WinFormsApp6\minion.jpg",
            @"C:\Users\Redmi\source\repos\swagaplayer\winformsuliana\WinFormsApp6\WinFormsApp6\misha.jpg",
            @"C:\Users\Redmi\source\repos\swagaplayer\winformsuliana\WinFormsApp6\WinFormsApp6\baranova2.jpg"
        };

        int currentIndex = 0;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            LoadImage();
        }

        void LoadImage()
        {
            pictureBox1.Image?.Dispose();
            pictureBox1.Image = Image.FromFile(images[currentIndex]);

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            currentIndex = (currentIndex + 1) % images.Count; 
            LoadImage();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            currentIndex = (currentIndex - 1 + images.Count) % images.Count;
            LoadImage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentIndex = (currentIndex + 1) % images.Count;
            LoadImage();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentIndex = (currentIndex - 1 + images.Count) % images.Count;
            LoadImage();
        }
    }
}