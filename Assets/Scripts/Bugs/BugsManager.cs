using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BugsManager : MonoBehaviour
{
    public int AmountOfKilledRoaches;
    public bool AreBugsMoving { get; private set; }
    private List<BugController> bugsControllers = new();
    public event Action<int> OnBugKilled;
    [SerializeField] private GameObject bugPrefab;

    private void Awake()
    {
        bugsControllers.AddRange(FindObjectsOfType<BugController>());
    }

    public void MoveBugs()
    {
        if(AreBugsMoving) return;
        StartCoroutine(MoveBugsCoroutine());
    }

    private IEnumerator MoveBugsCoroutine()
    {
        AreBugsMoving = true;
        bugsControllers = bugsControllers
            .Where(bug => bug != null)
            .ToList();

        foreach (BugController bug in bugsControllers)
        {
            bug.Move();
        }

        yield return new WaitForSeconds(0.55f);
        AreBugsMoving = false;
    }

    public void MarkRoachKilled()
    {
        AmountOfKilledRoaches++;
        OnBugKilled?.Invoke(AmountOfKilledRoaches);
    }
}
