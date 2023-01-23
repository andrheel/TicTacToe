module Page.About

open Fable.FontAwesome
open Fable.React
open Fable.React.Props
open App.Types
open UI


let view model dispatch =
    let onStart _ = Msg.Goto Pages.Page.Home |> dispatch
    div [] [
      h2 [] [ str "Tic-Tac-Toe sample" ]
      ``start-xref`` dispatch
      ]


