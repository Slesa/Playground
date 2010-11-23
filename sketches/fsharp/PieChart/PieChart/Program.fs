module Program

open System
open System.Drawing
open System.Windows.Forms
open Drawings

let mainForm = new Form(Width=620, Height=450, Text="Pie Chart")

let menu = new ToolStrip()
let btnOpen = new ToolStripButton("Open")
let btnSave = new ToolStripButton("Save", Enabled=false)
ignore(menu.Items.Add(btnOpen))
ignore(menu.Items.Add(btnSave))

let boxChart = new PictureBox(BackColor=Color.White, Dock=DockStyle.Fill, SizeMode=PictureBoxSizeMode.CenterImage)

mainForm.Controls.Add(menu)
mainForm.Controls.Add(boxChart)

let openAndDrawChart(e) = 
    let dlg = new OpenFileDialog(Filter="CSV Files|*.csv")
    if( dlg.ShowDialog() = DialogResult.OK) then
        let pieChart = Drawings.drawChart(dlg.FileName)
        boxChart.Image <- pieChart
        btnSave.Enabled <- true

let saveDrawing(e) = 
    let dlg = new SaveFileDialog(Filter="PNG Files|*.png")
    if( dlg.ShowDialog() = DialogResult.OK) then
        boxChart.Image.Save(dlg.FileName)

[<STAThread>]
do
    btnOpen.Click.Add(openAndDrawChart)
    btnSave.Click.Add(saveDrawing)
    Application.Run(mainForm)
