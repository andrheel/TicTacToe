module Navbar

open Fable.FontAwesome
open Fable.React
open Fable.React.Props
open UI


let view dispatch =
    nav
      [ Class "shadow mb-3 navbar navbar-light navbar-expand-md bg-faded justify-content-center"  ]
      [ div
          [ Class "container" ]
          [ div
              [ Class "navbar-brand" ]
              [ h3 [ Class "navbar-item" ] [ str "X-O" ] ] 
            div
              [ Class "navbar-item" ]
              [ xref [] [str "About"] Pages.Page.About dispatch  ]
          ]
      ]

