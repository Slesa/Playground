// Learn more about F# at http://fsharp.net
open System
open Mandelbrot
open System.Drawing
open System.Windows.Forms

let form = new Form(Text="Mandelbroth", Width=640, Height=480)

do
    let bm = new Bitmap(form.Width, form.Height, Imaging.PixelFormat.Format24bppRgb)
    for y in [0..bm.Height-1] do
        for x in [0..bm.Width-1] do
            bm.SetPixel(x, y, (colorbrot (((float)x - 380.0)/150.0) (((float)y - 240.0)/150.0)))
    let pictureBox = new PictureBox(Dock=DockStyle.Fill)
    pictureBox.Image <- bm
    let slider = System.Windows.Forms.TrackBar()
    form.Controls.Add(slider)
    pictureBox.MouseClick.Add(fun e -> drawbrot slider.Value pictureBox e.X e.Y)
    slider.Scroll.Add(fun _ -> drawbrot slider.Value pictureBox 0 0 )
    slider.Value <- 2
    form.Controls.Add(pictureBox)


[<STAThread>]
do Application.Run(form)
