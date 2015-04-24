module Exercise

open System.Drawing
open System
open Helpers

let generateTree width height = 
    let trunk = { StartPoint= Point(width/2,height); EndPoint = Point(width/2,height-100) }
    let child1 = { StartPoint= Point(width/2,height-100); EndPoint = rotateWrtPoint (Point(width/2,height-200))  (Point(width/2,height-100)) 45.0<degree> }
    let child2 = { StartPoint= Point(width/2,height-100); EndPoint = rotateWrtPoint (Point(width/2,height-200))  (Point(width/2,height-100)) -45.0<degree> }
    seq {
        yield seq {yield trunk}
        yield [ child1 ; child2 ] |> List.toSeq
    }

let drawLine (graphics : Graphics) pen (line : Line) =
    graphics.DrawLine(pen,line.StartPoint,line.EndPoint)

let drawAndSaveFractalTree (width : int)  (height : int) = 
    let bmp = new Bitmap(width,height)

    let pen = new Pen(Color.Aqua, 3.0f)
    
    use graphics = Graphics.FromImage(bmp)
    graphics.Clear(Color.Black)
    let drawLine' = drawLine graphics pen //You might be able to think of a better style for this. Think of this like mathematical derivation f(x) -> f'(x)
    generateTree width height |> Seq.take 2 |> Seq.concat |> Seq.iter drawLine'

    bmp.Save("..\\..\\FractalTree.jpeg")

            
    

