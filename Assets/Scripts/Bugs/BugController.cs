using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class BugController : MonoBehaviour
{
    [SerializeField] private Transform bugNextTileIndicator;
    public float agressiveFactor = 0.4f;
    private Vector2 NextMovementDirection;
    private void Start()
    {
        bugNextTileIndicator.SetParent(null);
        SetIndicatorAndFindPosition();
    }

    public void Move()
    {
        bugNextTileIndicator.gameObject.SetActive(false);
        transform.DOMove(transform.position + (Vector3)(NextMovementDirection), 0.5f)
            .OnComplete(() => { SetIndicatorAndFindPosition(); })
            .SetEase(Ease.Linear);
    }

    public void SetIndicatorAndFindPosition()
    {
        NextMovementDirection = FindPossibleDirection();
        bugNextTileIndicator.position = transform.position +(Vector3) NextMovementDirection;

        if (NextMovementDirection == Vector2.zero)
        {
            bugNextTileIndicator.gameObject.SetActive(false);
        }
        else
        {
            transform.up = NextMovementDirection.normalized;
            bugNextTileIndicator.gameObject.SetActive(true);
        }
    }

    public Vector2 FindPossibleDirection()
    {
        var upColliders = Physics2D.OverlapBoxAll(transform.position + (Vector3)Vector2.up, new Vector2(0.95f, 0.95f), 0);

        var downColliders = Physics2D.OverlapBoxAll(transform.position + (Vector3)Vector2.down, new Vector2(0.95f, 0.95f), 0);

        var rightColliders = Physics2D.OverlapBoxAll(transform.position + (Vector3)Vector2.right, new Vector2(0.95f, 0.95f), 0);

        var leftColliders = Physics2D.OverlapBoxAll(transform.position + (Vector3)Vector2.left, new Vector2(0.95f, 0.95f), 0);


        List<Vector2> possibleMoves = new List<Vector2>();

        if (CanGo(upColliders)) possibleMoves.Add(Vector2.up);
        if (CanGo(downColliders)) possibleMoves.Add(Vector2.down);
        if (CanGo(rightColliders)) possibleMoves.Add(Vector2.right);
        if (CanGo(leftColliders)) possibleMoves.Add(Vector2.left);

        if (possibleMoves.Count == 0) return Vector2.zero;

        if (Random.value <= agressiveFactor)
        {
            var robotController = FindObjectOfType<RobotController>();
            return possibleMoves.OrderBy(move => Vector3.Distance(transform.position+(Vector3)move,robotController.transform.position)).First();
        }
        else
        {
            return possibleMoves[Random.Range(0, possibleMoves.Count)];
        }
    }

    private bool CanGo(Collider2D[] colliders)
    {
        colliders = colliders
            .Where(collider => collider.gameObject.GetComponent<RobotController>() == null)
            .Where(collider => !collider.isTrigger)
            .ToArray();

        if (colliders.Length == 0) return true;
        else
        {
            return false;
        }
    }

    public void Death()
    {
        FindObjectOfType<BugsManager>().MarkRoachKilled();
        GetComponent<Animator>().SetTrigger("Death");
        Destroy(gameObject,5f);
        bugNextTileIndicator.gameObject.SetActive(false);
        DestroyImmediate(GetComponent<Collider2D>());
        DestroyImmediate(this);
    }
    
}