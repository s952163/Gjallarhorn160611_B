module Context3

open Gjallarhorn
open Gjallarhorn.Bindable
open Gjallarhorn.Validation
open OxyPlot


let create() =
    let rnd = System.Random()   
    let makeSeries n =
        seq {for _ in 1..n -> rnd.NextDouble() - 0.5} |> Seq.scan (+) 0. |> Seq.mapi (fun x y -> DataPoint(float x,y)) |> Seq.toList
        
    let subject = Binding.createSubject () 
    let date1 = Mutable.create <| System.DateTime.Now.AddYears(-1)
    let date2 = Mutable.create <| System.DateTime.Now
    
    let daysDiff =  Signal.map2 (-) date2 date1
    let chartData = daysDiff |> Signal.map (fun x -> makeSeries x.Days)
    //let chartData = Mutable.create <| makeSeries daysDiff.Value.Days

    date1 |> Binding.editDirect subject "startDt" noValidation
    date2 |> Binding.editDirect subject "endDt" noValidation
    //chartData |> Binding.editDirect subject "Data" noValidation

    subject.Watch "daysDiff" (daysDiff |> Signal.map (fun x -> x.Days))
    subject.Watch "Data" chartData

    let _sub1 = date1 |> Signal.Subscription.create(fun n1 -> printfn "Start Date: %A" n1)  
    let _sub2 = date2 |> Signal.Subscription.create(fun n1 -> printfn "End Date: %A" n1) 
    
    //let _sub3 = daysDiff |> Signal.Subscription.create(fun x -> chartData.Value <- makeSeries x.Days )        
    
    (*
    let click _ =   out1.Value <- rnd.Next in1.Value  
    
    let clickcommand = subject.Command "Click" 

    clickcommand
    |> Observable.subscribe click
    |> subject.AddDisposable
     *)
    
    _sub2 |> subject.AddDisposable
    _sub1 |> subject.AddDisposable
    //_sub3 |> subject.AddDisposable

    subject



