module Page.Main

open Fable.FontAwesome
open Fable.React
open Fable.React.Props
open App.Types
open Fable.Core.JS

let ``widget-cell`` = FunctionComponent.Of(
      fun (args : {| x : int; y : int; model : Model; dispatch : Msg -> unit |}) -> 
          let state = Hooks.useState (args.model.Board.[args.y].[args.x])
          Hooks.useEffect((fun () -> state.update args.model.Board.[args.y].[args.x]),
                          [| args.model.Board.[args.y].[args.x] |])

          let toggle() = 
              if args.model.BlockUI then () else
              Msg.Update { args.model with BlockUI = true } |> args.dispatch
              async {
                  let time = 150
                  state.update args.model.CurrentSide
                  do! Async.Sleep time
                  for i in 1..2 do 
                      state.update " "
                      do! Async.Sleep time
                      state.update args.model.CurrentSide
                      do! Async.Sleep time
                  args.model.Board.[args.y].[args.x] <- args.model.CurrentSide
                  Msg.Update 
                    { args.model with 
                        CurrentSide = if args.model.CurrentSide = "X" then "O" else "X"
                        BlockUI = false
                        Board = args.model.Board
                    } |> args.dispatch
                  } |> Async.StartImmediate

          let styles = [ Height "80px"; Width "80px"; Border "solid 1px silver"; Cursor "pointer" ]
          div [ Style styles; OnClick(fun _ -> toggle()) ] 
              [ let styles = [ FontSize "40px"; FontWeight "500"; TextAlign TextAlignOptions.Center ]
                div [ Style styles; Class "m-0 p-0" ] 
                    [ str state.current ] ]
      )

let view (model : Model) dispatch =
    div [] [
      if model.FinalMsg = "" then 
        div [] [ 
          if model.BlockUI 
          then str "Wait..."
          else span [] [ str "Now move of "; b [ ] [ str model.CurrentSide ] ]
        ]
      table [ Class "table table-bordered" ] [
        for y, line in model.Board |> Seq.indexed do
          tr [] [
            for x, cell in line |> Seq.indexed do
              td [] [ ``widget-cell`` {| x = x; y = y; model = model; dispatch = dispatch |} ]
          ]
        ]
      h4 [ Class "my-4 text-center" ] [ str model.FinalMsg ]
      if model.FinalMsg <> "" then UI.``start-xref`` dispatch
    ]



