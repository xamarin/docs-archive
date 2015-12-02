namespace HelloWorldUrho1

open System
open UIKit
open Foundation
open Urho
open Urho.iOS

[<Register ("AppDelegate")>]
type AppDelegate () =
    inherit UIApplicationDelegate ()

    //override val Window = null with get, set

    override this.FinishedLaunching (app, options) =
        //this.Window <- new UIWindow(UIScreen.MainScreen.Bounds)

        UrhoEngine.Init()
        let o = Urho.ApplicationOptions.Default
     
        let g = new HelloWorld(o)
        g.Run() |> ignore
       
        true
