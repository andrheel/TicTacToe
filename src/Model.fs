module App.Types

open Pages

type Model = { 
        CurrentPage : Page
        Board       : string [] []
        BlockUI     : bool
        CurrentSide : string
        FinalMsg    : string
        } with 
    static member Zero = { 
        CurrentPage = Home 
        Board       = [||] 
        BlockUI     = false 
        CurrentSide = "X"
        FinalMsg    = ""
        }
    static member ``reset-board`` (this : Model) = 
        { this with 
            Board = [| for y in 0..2 -> [| for x in 0..2 -> "" |] |] 
            BlockUI = false
            CurrentSide = "X"
            FinalMsg    = ""
        }
    member this.VerifyGameOver() = 
        let h i = set this.Board.[i] 
        let v i = set [| for y in 0..2 -> this.Board.[y].[i] |]
        let g1 () = set [| this.Board.[0].[0]; this.Board.[1].[1]; this.Board.[2].[2] |]
        let g2 () = set [| this.Board.[0].[2]; this.Board.[1].[1]; this.Board.[2].[0] |]
        let ``do-check`` (xs : _ Set) = if xs.Count = 1 && xs.MaximumElement <> "" then Some xs.MaximumElement else None
        let all = seq { for i in 0..2 do h i 
                        for i in 0..2 do v i
                        g1 ()
                        g2 () } 
        let ``is-draw``() = this.Board |> Seq.collect id |> Seq.contains "" |> not
        let msg = match all |> Seq.tryPick ``do-check`` with
                  | Some side -> $"`{side}` win"
                  | _ -> if ``is-draw``() then "Draw" else ""
        { this with FinalMsg = msg; BlockUI = msg <> "" }
        

type Msg =
    | Update of Model
    | Goto   of Page

