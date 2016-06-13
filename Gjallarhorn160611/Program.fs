open System
open Gjallarhorn
open Gjallarhorn.Bindable
open Gjallarhorn.Validation
open FsXaml
open System.Windows

//type MainWin = XAML<"MainWindow.xaml">
type MainWin = XAML<"MainWindow2.xaml">

[<STAThread>]
[<EntryPoint>]
let main _ =     

    let rnd = System.Random()
    let num1 = Mutable.create 0
    let num2 = Mutable.create 1
    let click() = num1.Value <- rnd.Next(num2.Value)
    let _sub1 = num2 |> Signal.Subscription.create (fun _ -> click())
    let _sub2 = num1 |> Signal.Subscription.create(fun n1 -> printfn "The lucky number is: %d" n1)


    num2.Value <- 30
        
    //printfn "%A" num1.Value


    Gjallarhorn.Wpf.install true |> ignore

    let app = Application()
    //let win = MainWin(DataContext = Context.create())
    //let win = MainWin(DataContext = Context2.create())
    let win = MainWin(DataContext = Context3.create())
    app.Run win
    