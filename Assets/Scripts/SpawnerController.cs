using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public int RoachesKillToEnable;
    public float agressionMin = 0.6f;
    public float agressionMax = 0.7f;
    public GameObject bugPrefab;

    public BugController Spawn()
    {
        var bugController = Instantiate(bugPrefab, transform.position, Quaternion.identity)
            .GetComponent<BugController>();

        bugController.agressiveFactor = Random.Range(agressionMin, agressionMax);
        return bugController;
    }
}
