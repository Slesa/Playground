using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(
    typeof(ImageVisualizer.BitmapVisualizer),
    typeof (VisualizerObjectSource),
    Target = typeof (Image),
    Description = "Visualizer for .Net Images")]

namespace ImageVisualizer
{
    // .Net Magazin 12/2010, S. 67
    public class BitmapVisualizer
        : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            var image = (Bitmap) objectProvider.GetObject();
            
            var dialogTitle = String.Format("Width: {0}, Height: {1}, Format: {2}",
                                               image.Width, image.Height, image.PixelFormat);

            var form = new Form
                {
                    Text = dialogTitle,
                    ClientSize = new Size(image.Width, image.Height),
                    FormBorderStyle = FormBorderStyle.FixedToolWindow,
                };

            var pictureBox = new PictureBox
                {
                    Image = image,
                    Dock = DockStyle.Fill,
                    Parent = form,
                };

            pictureBox.MouseMove += (s, a) =>
                {
                    form.Text = String.Format("{0}, RGB({1})={2}",
                        dialogTitle, a.Location, image.GetPixel(a.Location.X, a.Location.Y));
                };

            windowService.ShowDialog(form);
        }
    }
}