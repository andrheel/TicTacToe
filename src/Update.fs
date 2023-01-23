module App.State

open Elmish
open Elmish.Navigation
open Elmish.UrlParser
open Browser
open Pages
open Types

let pageParser: Parser<Page->Page,Page> =
    oneOf [ map Home  (s "home")
            map About (s "about")
          ]


let urlUpdate (result : Page option) model =
    match result with
    | None      -> console.error("Error parsing url")
                   model, Navigation.modifyUrl (toHash model.CurrentPage)
    | Some (Pages.Page.Home & page) 
                -> { Model.``reset-board`` model with CurrentPage = page }, []
    | Some page -> { model with CurrentPage = page }, []


let init result =
    urlUpdate result (Model.``reset-board`` Model.Zero)


let update msg model =
    match msg with
    | Update model when model.BlockUI = false  -> model.VerifyGameOver(), Cmd.none
    | Update model  -> model, Cmd.none
    | Goto page     -> urlUpdate (Some page) model

   