using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public Direction CurrentDirection { get; private set; } = Direction.Up;
    public bool IsBusy { get; private set; } = false;

    [SerializeField] private float oneTileMovementLength = 0.25f;

    public void Move(int amountOfTiles)
    {
        if(IsBusy)
        {
            Debug.LogWarning("Tried to use robot method while robot was busy.");
            return;
        }

        IsBusy = true;
        transform
            .DOMove(transform.position + (Vector3)DirectionToVector(CurrentDirection) * amountOfTiles,
                oneTileMovementLength * amountOfTiles)
            .OnComplete(() => { IsBusy = false; });
    }

    public void Rotate(Direction direction)
    {
        if(IsBusy)
        {
            Debug.LogWarning("Tried to use robot method while robot was busy.");
            return;
        }
        
        IsBusy = true;
        
        CurrentDirection = direction;
        
        IsBusy = false;
    }
    
    public Vector2 DirectionToVector(Direction direction)
    {
        return direction switch
        {
            Direction.Up => Vector2.up,
            Direction.Down => Vector2.down,
            Direction.Left => Vector2.left,
            Direction.Right => Vector2.right,
            _ => throw new ArgumentException("Illegal direction? How is that possible?")
        };
    }
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}
