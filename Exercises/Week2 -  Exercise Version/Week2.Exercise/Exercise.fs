module Exercise

open System.Drawing
open System
open Helpers

let generateTree width lineLength angle = 
    let trunk = { StartPoint= Point(width/2,0); EndPoint = Point(width/2,lineLength) }
    let child1 = { StartPoint= Point(width/2,lineLength); EndPoint = rotateWrtPoint (Point(width/2,2*lineLength))  (Point(width/2,lineLength)) angle }
    let child2 = { StartPoint= Point(width/2,lineLength); EndPoint = rotateWrtPoint (Point(width/2,2*lineLength))  (Point(width/2,lineLength)) -angle }
    seq {
        yield seq {yield trunk}
        yield [ child1 ; child2 ] |> List.toSeq
    }

let drawLine (graphics : Graphics) pen (line : Line) =
    graphics.DrawLine(pen,line.StartPoint,line.EndPoint)

let drawAndSaveFractalTree() = 
    let width = 1920
    let height = 1080
    let lineLength = 100
    let angle = 90.0<degree>

    let bmp = new Bitmap(width,height)

    let blackPen = new Pen(Color.Blue, 3.0f)
    
    use graphics = Graphics.FromImage(bmp)
    let drawLine' = drawLine graphics blackPen //You might be able to think of a better style for this. Think of this like mathematical derivation f(x) -> f'(x)
    generateTree width lineLength angle |> Seq.take 2 |> Seq.concat |> Seq.iter drawLine'

    bmp.Save("..\\..\\FractalTree.jpeg")

            
    

