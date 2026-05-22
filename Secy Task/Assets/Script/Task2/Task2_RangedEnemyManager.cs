using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task2_RangedEnemyManager : Task2_EnemyManager
{
    [Header("Ranged Settings")]
    public float preferredDistance;
    public float shootingCooldown;

    private bool canShoot = true;

    protected override void Update()
    {
        base.Update();

        if (playerDetected)
        {
            MaintainDistance();

            if (canShoot)
            {
                ShootProjectile();
            }
        }
        else
        {
            Patrol();
        }
    }

    private void MaintainDistance()
    {

    }

    private void ShootProjectile()
    {

    }

    private void HandleShootCooldown()
    {

    }

    protected override void ChasePlayer()
    {

    }
}
