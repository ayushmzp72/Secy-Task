using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task2_ScoutManager : Task2_EnemyManager
{
    [Header("Scout Settings")]
    public Transform[] scanPoints;
    public float rotationSpeed = 50f;

    public float alertDuration = 5f;

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

    protected override void DetectPlayer()
    {
        if (scanPoints.Length == 0) return;

        Transform currentPoint =
            scanPoints[currentPointIndex];

        Collider2D detection =
            Physics2D.OverlapCircle(
                currentPoint.position,
                detectionRange,
                playerLayer
            );

        if (detection != null)
        {
            playerDetected = true;

            player = detection.transform;
        }
        else
        {
            playerDetected = false;
        }
    }

    private void ScanArea()
    {
        if (scanPoints.Length == 0) return;

        RotateTowardsPoint();

        Vector2 direction =scanPoints[currentPointIndex].position -transform.position;

        float targetAngle =Mathf.Atan2(direction.y, direction.x)* Mathf.Rad2Deg;

        float currentAngle =transform.eulerAngles.z ;

        // Normalize angle
        if (currentAngle > 180)
        {
            currentAngle -= 360;
        }

        // Switch scan point
        if (Mathf.Abs(currentAngle - targetAngle) < 5f)
        {
            currentPointIndex++;

            if (currentPointIndex >= scanPoints.Length)
            {
                currentPointIndex = 0;
            }
        }
    }

    private void RotateTowardsPoint() // Havent checked if this works
    {
        Transform targetPoint =scanPoints[currentPointIndex];

        Vector2 direction =targetPoint.position -transform.position;

        float angle =Mathf.Atan2(direction.y, direction.x)* Mathf.Rad2Deg;

        Quaternion targetRotation =Quaternion.Euler(0, 0, angle);

        transform.rotation =Quaternion.Lerp(transform.rotation,targetRotation,rotationSpeed * Time.deltaTime);
    }

    private void RaiseGlobalAlert()
    {
        AlertNearbyEnemies();
        Debug.Log("Camera Alert!");
    }

    public override void ReceiveAlert(Vector3 playerPosition)
    {
        base.ReceiveAlert(playerPosition);
        Debug.Log("Camera Received Alert");
    }

    protected override void Patrol()
    {

    }

    protected override void ChasePlayer()
    {

    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        if (scanPoints == null) return;

        foreach (Transform point in scanPoints)
        {
            if (point != null)
            {
                Gizmos.color = Color.cyan;

                Gizmos.DrawWireSphere(
                    point.position,
                    detectionRange
                );

                Gizmos.DrawLine(
                    transform.position,
                    point.position
                );
            }
        }
    }
}

