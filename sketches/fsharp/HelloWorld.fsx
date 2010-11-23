open System.Drawing
open System.Windows.Forms

type HelloWindow() =
    let form = new Form(Width=400, Height=140)
    let font = new Font("Times New Roman", 28.0f)
    let label = new Label(Dock=DockStyle.Fill, Font=font, TextAlign=ContentAlignment.MiddleCenter)
    do form.Controls.Add(label)

    member x.SayHello(name) = 
        let msg = "Hello " + name + "!"
        label.Text <- msg

    member x.Run() = 
        form.Show()
//       Application.Run(form)
       
do
    let hello = new HelloWindow()
    hello.SayHello("dear reader")
    hello.Run()