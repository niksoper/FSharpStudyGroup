module Helpers

open System.Drawing
open System

type TreeNode(Start: Point, End: Point) =
    member this.ChildNodes length angle =
        seq {
            yield new TreeNode(Start = this.End, End = rotateWrtPoint (Point(this.End.X, this.End.Y + length))  this.End angle)
        }   

[<Measure>] type radian
[<Measure>] type degree

let addPoints (p1 : Point) (p2 : Point) = 
    Point(p1.X + p2.X, p1.Y + p2.Y)

//<summary>Returns a new point = p1 - p2</summary>
let subtractPoints (p1 : Point) (p2 : Point) = 
    Point(p1.X - p2.X, p1.Y - p2.Y)

let convertDegreeToRadian (angle : float<degree>) : float<radian> = 
    let radiansPerDegree  = (Math.PI * 1.0<radian>) / 180.0<degree>
    radiansPerDegree * angle

let rotateWrtOrigin (p : Point) (angle : float<degree>) = 
    let oldX = float p.X
    let oldY = float p.Y
    let radianAngle = float <| convertDegreeToRadian angle
    let newX = int (Math.Round(oldX * Math.Cos(radianAngle) - oldY * Math.Sin(radianAngle)))
    let newY = int (Math.Round(oldX * Math.Sin(radianAngle) + oldY * Math.Cos(radianAngle)))
    Point(newX,newY)

let rotateWrtPoint (p : Point) (aroundPoint : Point) (angle : float<degree>) = 
    let result = addPoints aroundPoint <| rotateWrtOrigin (subtractPoints p aroundPoint) angle
    printfn "Rotating %A around %A by %A degrees to make %A" p aroundPoint angle result
    result
    
