module UI

open Elmish
open Elmish.Navigation
open Elmish.UrlParser
open Browser

open Fable.React
open Fable.React.Props
open Fable.Core.JsInterop

open Pages
open App.Types

let ``centered-content`` xs = 
    let ``center-classes`` = "d-flex align-items-center justify-content-center "
    let ``inner-classes``  = ``center-classes`` + "flex-column "
    div [ Class ``center-classes`` ] 
        [ div [ Class ``inner-classes``; Style [ Height "75vh" ] ]
              xs 
        ]


let xref (attr :Props.IHTMLProp list) label (page : Pages.Page) (dispatch : Msg -> unit) =
    a ( !!(OnClick (fun _ -> Msg.Goto page |> dispatch)) 
        :: !!(Style [ Cursor "pointer"; Color "blue"; TextDecoration "underline" ])
        :: attr ) label  


let ``start-xref`` dispatch = 
      div [ Class "mt-4 text-center" ] 
          [ str "press "; 
            xref [ ] [ str "Start" ] 
                 Pages.Page.Home dispatch
            str " to begin" ]
