using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugController : MonoBehaviour
{
    [SerializeField] private Transform bugNextTileIndicator; 
    private Vector2 NextMovementDirection;

    private void Start()
    {
        Move();
    }

    public void Move()
    {
        NextMovementDirection = FindPossibleDirection();
        bugNextTileIndicator.localPosition = NextMovementDirection;
        
    }

    public Vector2 FindPossibleDirection()
    {
        var upColliders = Physics2D.OverlapBoxAll(transform.position + (Vector3)Vector2.up, new Vector2(1,1), 0);
        
        var downColliders = Physics2D.OverlapBoxAll(transform.position + (Vector3)Vector2.down, new Vector2(1,1), 0);
        
        var rightColliders = Physics2D.OverlapBoxAll(transform.position + (Vector3)Vector2.right, new Vector2(1,1), 0);
        
        var leftColliders = Physics2D.OverlapBoxAll(transform.position + (Vector3)Vector2.left, new Vector2(1,1), 0);
        
        
        
        return Vector2.zero;
    }
}
