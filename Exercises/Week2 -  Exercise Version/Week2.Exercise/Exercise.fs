module Exercise

open System.Drawing
open System
open Helpers

//let generateTree width lineLength angle = 
//    let trunk = { StartPoint= Point(width/2,0); EndPoint = Point(width/2,lineLength) }
//    
//    let newLine length angle seed = { StartPoint= seed.EndPoint; EndPoint = rotateWrtPoint (Point(seed.EndPoint.X,seed.EndPoint.Y + length))  seed.EndPoint angle }
//    
//    let rec next lineLength angle seeds = 
//        seq {
//            yield seq { yield! seeds }
//            yield! 
//                [ seeds |> Seq.map (newLine lineLength angle); seeds |> Seq.map (newLine lineLength (-angle)) ] 
//                |> List.toSeq 
//                |> Seq.concat 
//                |> next (lineLength/2) (angle-15.0<degree>)
//        }
//    seq { yield trunk } |> (next lineLength angle)

//let drawLine (graphics : Graphics) pen (line : Line) =
//    graphics.DrawLine(pen,line.StartPoint,line.EndPoint)

let childNode length angle parentNode = { Start = seed.End; End = rotateWrtPoint (Point(parentNode.End.X, parentNode.End.Y + length))  parentNode.End angle }

let rec drawNodes (graphics: Graphics) pen nodes depth =
    nodes |> Seq.iter (fun node -> graphics.DrawLine(pen, node.Start, node.End))
    match depth with
    | 0 -> ()
    | n -> drawNodes graphics pen seq { yield! seq { nodes |> Seq.map (fun node -> node.Left) } }

let drawAndSaveFractalTree() = 
    let width = 1920
    let height = 1080
    let lineLength = 100
    let angle = 45.0<degree>
    let depth = 5

    let bmp = new Bitmap(width,height)

    let blackPen = new Pen(Color.Blue, 3.0f)
    
    use graphics = Graphics.FromImage(bmp)
    let drawLine' = drawLine graphics blackPen //You might be able to think of a better style for this. Think of this like mathematical derivation f(x) -> f'(x)
    generateTree width lineLength angle 
        |> Seq.take depth 
        |> Seq.concat 
        |> Seq.iter drawLine'

    bmp.Save("..\\..\\FractalTree.jpeg")

            
    

