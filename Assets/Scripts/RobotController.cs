using System;
using System.Collections;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public Direction CurrentDirection { get; private set; } = Direction.Up;
    public bool IsBusy { get; private set; } = false;

    [SerializeField] private float oneTileMovementLength = 0.25f;

    private Rigidbody2D _rigidbody;

    private int testingTilesAmount;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Testing purposes only
        if (Input.GetKeyDown(KeyCode.P)) testingTilesAmount++;
        if (Input.GetKeyDown(KeyCode.Space)) Move(testingTilesAmount);
        if (Input.GetKeyDown(KeyCode.DownArrow)) Rotate(Direction.Down);
        if (Input.GetKeyDown(KeyCode.UpArrow)) Rotate(Direction.Up);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) Rotate(Direction.Left);
        if (Input.GetKeyDown(KeyCode.RightArrow)) Rotate(Direction.Right);
    }

    public void Move(int amountOfTiles)
    {
        if (IsBusy)
        {
            Debug.LogWarning("Tried to use robot method while robot was busy.");
            return;
        }

        IsBusy = true;
        SnapBackToGrid();
        StartCoroutine(MovementCoroutine(amountOfTiles));
    }

    public IEnumerator MovementCoroutine(int amountOfTiles)
    {
        _rigidbody.velocity = DirectionToVector(CurrentDirection) / oneTileMovementLength;
        
        yield return new WaitForSeconds(amountOfTiles * oneTileMovementLength);
        _rigidbody.velocity = Vector2.zero;
        SnapBackToGrid();
        IsBusy = false;
    }

    public void Rotate(Direction direction)
    {
        if (IsBusy)
        {
            Debug.LogWarning("Tried to use robot method while robot was busy.");
            return;
        }

        IsBusy = true;

        CurrentDirection = direction;

        IsBusy = false;
    }

    private void SnapBackToGrid()
    {
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y),
            Mathf.RoundToInt(transform.position.z));
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