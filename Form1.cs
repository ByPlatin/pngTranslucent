using System.Drawing.Imaging;
namespace pngTranslucent
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string fullPath = "";

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select a file",
                Filter = "Image Files (*.png)|*.png",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                fullPath = selectedFilePath;
                label2.Text = selectedFilePath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (fullPath.Count() < 1)
            {
                MessageBox.Show("Please select file!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string inputPath = fullPath;  // Ýþlenecek PNG dosyasýnýn yolu
            string outputPath = fullPath.Replace(".png", "") + "_t.png"; // Çýktý PNG dosyasýnýn yolu

            // Görseli yükle
            using (Bitmap bitmap = new Bitmap(inputPath))
            {
                try
                {
                    button1.Enabled = false;
                    button2.Enabled = false;
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        for (int y = 0; y < bitmap.Height; y++)
                        {
                            Color pixelColor = bitmap.GetPixel(x, y);

                            int gray = pixelColor.A;
                            Color newColor = Color.FromArgb(255, gray, gray, gray);
                            bitmap.SetPixel(x, y, newColor);
                        }
                    }
                    bitmap.Save(outputPath, ImageFormat.Png);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    fullPath = "";
                    label2.Text = "Please select file!";
                    button1.Enabled = true;
                    button2.Enabled = true;
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Text = "Please select file!";
        }
    }
}
