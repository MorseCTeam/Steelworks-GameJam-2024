using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class RobotController : MonoBehaviour
{
    public Direction CurrentDirection { get; private set; } = Direction.Up;
    public bool IsBusy { get; private set; } = false;

    [SerializeField] private float oneTileMovementLength = 0.15f; 
    [SerializeField] private Transform valveTransform;
    private float _batteryLife = 100f;
    
    private ButtonsToRobotAdapterController _buttonsAdapter;
    private BugsManager _bugsManager;
    private Rigidbody2D _rigidbody;
    private int testingTilesAmount;

    private void Awake()
    {
        _buttonsAdapter = FindObjectOfType<ButtonsToRobotAdapterController>();
        _buttonsAdapter.OnMovePerformed += Move;
        _buttonsAdapter.ValvePressed += PullValve;
        _buttonsAdapter.OnTurnPerformed += direction => Rotate((Direction)direction);
    }
    private void Start()
    {
        _bugsManager = FindObjectOfType<BugsManager>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
    }

    private void Move(int amountOfTiles)
    {
        if (IsBusy) return;
        if (_bugsManager.AreBugsMoving) return;

        
        IsBusy = true;
        SnapBackToGrid();
        StartCoroutine(MovementCoroutine(amountOfTiles));
    }

    private void PullValve()
    {
        if (IsBusy) return;
        if (_bugsManager.AreBugsMoving) return;

        IsBusy = true;
        valveTransform
            .DOLocalRotate(valveTransform.eulerAngles + Vector3.forward * Random.Range(360f, 480f), Random.Range(0.7f, 0.9f),RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                IsBusy = false;
                _bugsManager.MoveBugs();
            });
        _batteryLife += 50f;

    }
    public IEnumerator MovementCoroutine(int amountOfTiles)
    {
        _rigidbody.velocity = DirectionToVector(CurrentDirection) / oneTileMovementLength;
        
        yield return new WaitForSeconds(amountOfTiles * oneTileMovementLength);
        _rigidbody.velocity = Vector2.zero;
        SnapBackToGrid();
        IsBusy = false;
        _bugsManager.MoveBugs();
    }
    public void Rotate(Direction direction)
    {
        if (IsBusy) return;
        if (_bugsManager.AreBugsMoving) return;

        IsBusy = true;

        CurrentDirection = direction;

        IsBusy = false;
        _bugsManager.MoveBugs();
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.GetComponent<BugController>()) StartCoroutine(Death()); 
    }

    private IEnumerator Death()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        FindObjectOfType<ScreenSpaceController>().ShutDown();
    }
}

public enum Direction
{   
    None,
    Up,
    Down,
    Left,
    Right
}