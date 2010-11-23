module Drawings

open System
open System.IO
open System.Drawing
open Calculations

let rnd = new Random()
let randomBrush() = 
    let r, g, b = rnd.Next(256), rnd.Next(256), rnd.Next(256)
    new SolidBrush(Color.FromArgb(r, g, b))

let drawPieSegment(gr:Graphics, title, startAngle, occupiedAngle) =
    let br = randomBrush()
    gr.FillPie(br, 170, 70, 260, 260, startAngle, occupiedAngle)
    br.Dispose()

let drawStep(drawingFunc, gr:Graphics, sum, data) = 
    let rec drawStepUtil(data, angleSoFar) = 
        match data with
        | [] -> ()
        | [title, value] ->
            let angle = 360 - angleSoFar
            drawingFunc(gr, title, angleSoFar, angle)
        | (title,value)::tail ->
            let angle = int(float(value) / sum * 360.0)
            drawingFunc(gr, title, angleSoFar, angle)
            drawStepUtil(tail, angleSoFar+angle)
    drawStepUtil(data, 0)

let fnt = new Font("Times New Roman", 11.0f)
let centerX, centerY = 300.0, 200.0
let labelDistance = 150.0

let drawLabel(gr:Graphics, title, startAngle, angle) =
    let lblAngle = float(startAngle+angle/2)
    let ra = Math.PI * 2.0 * lblAngle / 360.0
    let x = centerX + labelDistance * cos(ra)
    let y = centerY + labelDistance * sin(ra)
    let size = gr.MeasureString(title, fnt)
    let rc = new PointF(float32(x) - size.Width/2.0f, float32(y) - size.Height / 2.0f)
    gr.DrawString(title, fnt, Brushes.Black, new RectangleF(rc, size))

let drawChart(file) =
    let lines = List.ofSeq(File.ReadAllLines(file))
    let data = processLines(lines)
    let sum = float(calculateSum(data))

    let pieChart = new Bitmap(600, 400)
    let gr = Graphics.FromImage(pieChart)
    gr.Clear(Color.White)
    drawStep(drawPieSegment, gr, sum, data)
    drawStep(drawLabel, gr, sum, data)

    gr.Dispose()
    pieChart

//ignore(drawChart(@"c:\temp\import.csv"))