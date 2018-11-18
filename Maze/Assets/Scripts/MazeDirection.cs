using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MazeDirection {

    F,R,U,B,L,D

}

public static class MazeDirections
{
    public const int Count = 6;

    public static MazeDirection RandomValue
    {
        get
        {
            return (MazeDirection)Random.Range(0, Count);
        }
    }

    public static MazeDirection GetOpposite(this MazeDirection direction)
    {
        return (int)direction < 3 ? direction + 3 : direction - 3;
    }

    private static Quaternion[] rotations =
    {
        Quaternion.identity,
        Quaternion.Euler(0f, 90f, 0f),
        Quaternion.Euler(-90f, 0f, 0f),
        Quaternion.Euler(0f, 180f, 0f),
        Quaternion.Euler(0f, -90f, 0f),
        Quaternion.Euler(90f, 0f, 0f)
    };
    public static Quaternion ToRotation(this MazeDirection direction)
    {
        return rotations[(int)direction];
    }

    //private static IntVector2[] vectors =
    //{
    //    new IntVector2(0,1),
    //    new IntVector2(-1,0),
    //    new IntVector2(0,-1),
    //    new IntVector2(-1,0)
    //};
    //public static IntVector2 ToIntVector2(this MazeDirection direction)
    //{
    //    return vectors[(int)direction];
    //}

    private static IntVector3[] vectors =
{
        new IntVector3(0,0,1),
        new IntVector3(1,0,0),
        new IntVector3(0,1,0),
        new IntVector3(0,0,-1),
        new IntVector3(-1,0,0),
        new IntVector3(0,-1,0)
    };
    public static IntVector3 ToIntVector3(this MazeDirection direction)
    {
        return vectors[(int)direction];
    }
}
