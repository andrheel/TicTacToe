module App.View

open Elmish
open Elmish.Navigation
open Elmish.UrlParser
open Fable.Core
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props

open Types
open App.State
open Pages
open UI


let root model dispatch =
  let pageHtml page =
      match page with
      | Page.About -> Page.About.view model dispatch 
      | Page.Home  -> Page.Main.view model dispatch 

  div []
      [ Navbar.view dispatch
        ``centered-content`` [ pageHtml model.CurrentPage ] 
      ]


open Elmish.React
open Elmish.HMR

// App
Program.mkProgram init update root
|> Program.toNavigable (parseHash pageParser) urlUpdate
|> Program.withReactBatched "elmish-app"
|> Program.run
