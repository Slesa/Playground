// Learn more about F# at http://fsharp.net

module Mandelbrot

open Microsoft.FSharp.Math
open System
open System.Drawing
open System.Windows.Forms

let maxIteration = 50

let modSquared (c:Microsoft.FSharp.Math.Complex) = c.r*c.r + c.i*c.i
//let modQuared (c:Complex) = c.r*c.r+c.i

type MandelbrotResult =
    | DidNotEscape
    | Escaped of int

let mandelbrot c =
    let rec mandelbrotInner z iterations =
        if(modSquared z >= 4.0)
            then Escaped iterations
        elif iterations = maxIteration
            then DidNotEscape
        else
            mandelbrotInner (( z*z ) + c) (iterations+1)
    mandelbrotInner c 0

let min a b =
    if (a>b)
        then b
    else a

let max a b =
    if (a<b)
        then b
    else
        a

let colorbrot2 dx dy =
    match mandelbrot (Complex.Create (dx, dy)) with
        | DidNotEscape -> System.Drawing.Color.Black
        | Escaped e -> System.Drawing.Color.FromArgb(max 0 (255-7*e), max 0 (255-9*e), max 0 (255-16*e)) 

let colorbrot dx dy =
    match mandelbrot (Complex.Create (dx, dy)) with
        | DidNotEscape -> System.Drawing.Color.White
        | Escaped e -> System.Drawing.Color.FromArgb((min 255 (10*e)), (min 255 3*e),0)

let color x y newX newY zoom =
    colorbrot (((float)x - 380.0 + ((float)newX) )/(((float)zoom) * 150.0)) (((float)y - 240.0 + ((float)newY) )/(((float)zoom) * 150.0))

let drawbrot zoom (pictureBox : PictureBox) newX newY = 
    let bm = new Bitmap(pictureBox.Width, pictureBox.Height, Imaging.PixelFormat.Format24bppRgb)
    for y in [0..pictureBox.Height-1] do
        for x in [0..pictureBox.Width-1] do
            bm.SetPixel(x, y, (color x y newX newY zoom))
    pictureBox.Image <- bm
    pictureBox.Invalidate()


(* Verbesserung laut http://technofattie.blogspot.com/
let stepY = 0.1 / 40.0 //Scale goes in increments of 20 pixels for height
let stepX = 0.05 / 20.0 //Scale goes in increments of 60 pixels for width

//Simple declarations to get the height and width of the Bitmap
let numPixelsX = (int)(3.0 / stepX)
let numPixelsY = (int)(2.0 / stepY)
let bmp = new Bitmap(numPixelsX, numPixelsY)

//This is used to figure out what the current X and Y coordinates are
// during processing in order to set the appropriate pixels in the Bitmap
let getPosY y = (int)(Math.Abs((Math.Floor -1.0 - y) / stepY))
let getPosX x = (int)(Math.Abs((Math.Floor -2.0 - x) / stepX))

for y in [-1.0..stepY..1.0] do
    for x in [-2.0..stepX..1.0] do 
        match mandelbrot (Complex.Create (x, y)) with
        | DidNotEscape -> bmp.SetPixel(getPosX x, getPosY y, Color.FromArgb(255, 255, 255));
        | Escaped e -> bmp.SetPixel(getPosX x, getPosY y, Color.FromArgb((min 9 e) * 20, 0, 0));
    done;
done;

bmp.Save(String.Format("Images\\mandelBrot_{0}_{1}.bmp", numPixelsX, numPixelsY), ImageFormat.Bmp)
bmp.Save(String.Format("Images\\mandelBrot_{0}_{1}.png", numPixelsX, numPixelsY), ImageFormat.Png)
bmp.Save(String.Format("Images\\mandelBrot_{0}_{1}.jpeg", numPixelsX, numPixelsY), ImageFormat.Jpeg)
*)


