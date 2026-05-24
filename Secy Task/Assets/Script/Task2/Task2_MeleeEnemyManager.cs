using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task2_MeleeEnemyManager : Task2_EnemyManager
{
    [Header("Melee Settings")]
    public float attackRange = 1f;
    public float attackCooldown = 1f;
    [Header("Patrol")]
    public float patrolDistance = 5f;
    private Vector2 startPosition;
    private bool canAttack = true;
    private bool isAttacking = false;
     private bool movingRight = true;

    protected override void Start()
    {
        base.Start();

        startPosition = transform.position;
    }
    protected override void Update()
    {
        base.Update();

         if (isAttacking) return;         // to prevent movement while attacking


        if (playerDetected)
        {
            float distance = Vector2.Distance(transform.position,player.position);

            if (distance > attackRange + 0.1f)
            {
                ChasePlayer();
            }
            else
            {
                rb.velocity = Vector2.zero;

                if (canAttack && distance <= attackRange + 0.1f)
                {
                    PerformAttack();
                }
            }
        }
        else
        {
            Patrol();
        }
    }


    protected override void ChasePlayer()
    {
        base.ChasePlayer();
    }

    protected override void Patrol()
    {
        if(!isAlerted){
            if (movingRight)
            {
                rb.velocity = new Vector2(
                    moveSpeed,
                    rb.velocity.y
                );

                // FlipSprite(1);

                if (transform.position.x >= startPosition.x + patrolDistance)
                {
                    movingRight = false;
                }
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed,rb.velocity.y);

                // FlipSprite(-1);

                if (transform.position.x <=  startPosition.x - patrolDistance)
                {
                    movingRight = true;
                }
            }
        }
    }

    private void PerformAttack()
    {
        Debug.Log("Melee Attack");

        canAttack = false;
        isAttacking = true;


        Invoke(nameof(HandleAttackCooldown), attackCooldown);
    }

    private void HandleAttackCooldown()
    {   
        canAttack = true;
        isAttacking = false;
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position,attackRange);
    }
}

