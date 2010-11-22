using System;
using System.Drawing;
using System.Windows.Forms;

namespace TestVisualizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog { Filter = "JPG files|*.jpg|PNG Files|*.png" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var bitmap = new Bitmap(dialog.FileName);
                pictureBox.Image = bitmap;
            }
        }
    }
}
