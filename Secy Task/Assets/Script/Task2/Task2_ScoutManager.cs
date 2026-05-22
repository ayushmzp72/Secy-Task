using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task2_ScoutManager : Task2_EnemyManager
{
    [Header("Scout Settings")]
    public Transform[] scanPoints;
    public float rotationSpeed;
    public float alertDuration;

    private int currentPointIndex;

    protected override void Update()
    {
        base.Update();

        if (!playerDetected)
        {
            ScanArea();
        }
        else
        {
            RaiseGlobalAlert();
        }
    }

    private void ScanArea()
    {

    }

    private void RotateTowardsPoint()
    {

    }

    private void RaiseGlobalAlert()
    {

    }

    public override void ReceiveAlert(Vector3 playerPosition)
    {

    }
}