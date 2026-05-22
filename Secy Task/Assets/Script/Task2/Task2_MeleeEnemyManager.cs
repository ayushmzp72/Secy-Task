using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task2_MeleeEnemyManager : Task2_EnemyManager
{
    [Header("Melee Settings")]
    public float attackRange;
    public float attackCooldown;

    private bool canAttack = true;

    protected override void Update()
    {
        base.Update();

        if (playerDetected)
        {
            ChasePlayer();

            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                PerformAttack();
            }
        }
        else
        {
            Patrol();
        }
    }

    protected override void ChasePlayer()
    {

    }

    private void PerformAttack()
    {

    }

    private void HandleAttackCooldown()
    {

    }
}
