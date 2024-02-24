using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BugsManager : MonoBehaviour
{
    public bool AreBugsMoving { get; private set; }
    private List<BugController> bugsControllers;
    [SerializeField] private GameObject bugPrefab;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){MoveBugs();}
    }

    public void MoveBugs()
    {
        bugsControllers = bugsControllers
            .Where(bug => bug != null)
            .ToList();

        foreach (BugController bug in bugsControllers)
        {
            bug.Move();
        }
    }
}
