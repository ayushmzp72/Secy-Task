using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task2_EnemyManager : MonoBehaviour
{
    [Header("Detection")]
    public float detectionRange = 5f;
    public LayerMask playerLayer;

    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("References")]
    public Transform player;

    protected Rigidbody2D rb;
    protected bool playerDetected;
    protected bool isAlerted;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerDetected = false;
        isAlerted = false;
    }

    protected virtual void Update()
    {
        DetectPlayer();

        if (playerDetected)
        {
            AlertNearbyEnemies();
        }
    }

    protected virtual void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            transform.position,
            detectionRange,
            playerLayer
        );

        if (hit != null)
        {
            playerDetected = true;
            player = hit.transform;
        }
        else
        {
            playerDetected = false;
        }
    }

    protected virtual void AlertNearbyEnemies()
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position,detectionRange);

        foreach (Collider2D enemy in nearbyEnemies)
        {
            Task2_EnemyManager enemyManager =
                enemy.GetComponent<Task2_EnemyManager>();

            if (enemyManager != null && enemyManager != this)
            {
                enemyManager.ReceiveAlert(player.position);
            }
        }
    }

    public virtual void ReceiveAlert(Vector3 playerPosition)
    {
        isAlerted = true;
    }

    protected virtual void Patrol()
    {

    }

    protected virtual void ChasePlayer()
    {
        if (player == null) return;

        Vector2 direction =
            (player.position - transform.position).normalized;

        rb.velocity = new Vector2(
            direction.x * moveSpeed,
            rb.velocity.y
        );

        FlipSprite(direction.x);
    }

    protected virtual void ReturnToPatrol()
    {
        isAlerted = false;
    }

    protected virtual void FlipSprite(float directionX)
    {
        if (directionX > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (directionX < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            transform.position,
            detectionRange
        );
    }

}
