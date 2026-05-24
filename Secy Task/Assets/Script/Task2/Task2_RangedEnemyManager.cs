using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task2_RangedEnemyManager : Task2_EnemyManager
{
    [Header("Ranged Settings")]
    public float preferredDistance = 18f ;
    public float shootingCooldown = 2f;

    private bool canShoot = true;

    protected override void Update()
    {
        base.Update();

        if (playerDetected)
        {
            MaintainDistance();

            if (canShoot && distanceToPlayer() <= preferredDistance + 0.5f)
            {
                ShootProjectile();
            }
        }
        else
        {
            Patrol();
        }
    }

    private float distanceToPlayer()
    {
        if (player == null) return Mathf.Infinity;

        return Vector2.Distance(transform.position,player.position);
    }

    private void MaintainDistance()
    {
        if (player == null) return;

        float distance = distanceToPlayer();

        Vector2 direction =(player.position - transform.position).normalized;

        if (distance > preferredDistance)
        {
            ChasePlayer();
        }
        else if (distance < preferredDistance - 1f)
        {
            rb.velocity = new Vector2(-direction.x * moveSpeed,rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        // FlipSprite(direction.x);
    }

    private void ShootProjectile()
    {
        Debug.Log("Shoot Projectile");

        canShoot = false;

        Invoke(nameof(HandleShootCooldown), shootingCooldown);
    }

    private void HandleShootCooldown()
    {
        canShoot = true;
    }

    protected override void ChasePlayer()
    {
        base.ChasePlayer();
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position,preferredDistance);
    }
}
