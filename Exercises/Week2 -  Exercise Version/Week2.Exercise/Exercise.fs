module Exercise

open System.Drawing
open System
open Helpers

let generateTree width lineLength angle = 
    let trunk = { StartPoint= Point(width/2,0); EndPoint = Point(width/2,lineLength) }
    
    let newLine seed angle = { StartPoint= seed.EndPoint; EndPoint = rotateWrtPoint (Point(seed.EndPoint.X,seed.EndPoint.Y + lineLength))  seed.EndPoint angle }
    let left seed = newLine seed angle
    let right seed = newLine seed -angle
    
    let rec next previousLevel = 
        seq {
            yield seq { yield! previousLevel }
            yield! 
                [ previousLevel |> Seq.map left;  previousLevel |> Seq.map right ] 
                |> List.toSeq 
                |> Seq.concat 
                |> next
        }
    seq { yield trunk } |> next

let drawLine (graphics : Graphics) pen (line : Line) =
    graphics.DrawLine(pen,line.StartPoint,line.EndPoint)

let drawAndSaveFractalTree() = 
    let width = 1920
    let height = 1080
    let lineLength = 100
    let angle = 60.0<degree>
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

            
    

