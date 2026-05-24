using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task2_EnemyManager : MonoBehaviour
{
    [Header("Detection")]
    public float detectionRange = 5f;
    public float alertRange = 10f;
    public LayerMask playerLayer;

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float chaseSpeed = 5f;
    [Header("References")]
    public Transform player;
    protected Vector3 lastKnownPlayerPosition;

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
        Collider2D detection = Physics2D.OverlapCircle(
            transform.position,
            detectionRange,
            playerLayer
        );

        if (detection != null)
        {
            playerDetected = true;
            player = detection.transform;

            lastKnownPlayerPosition = detection.transform.position;
        }
        else
        {
            if(!isAlerted)
            {
                playerDetected = false;
                // player = null;
            }
        }
    }

    protected virtual void AlertNearbyEnemies()
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position,alertRange);

        foreach (Collider2D enemy in nearbyEnemies)
        {
            Task2_EnemyManager enemyManager =
                enemy.GetComponent<Task2_EnemyManager>();

            if (enemyManager != null && enemyManager != this)
            {
                enemyManager.ReceiveAlert(lastKnownPlayerPosition);
            }
        }
    }

    public virtual void ReceiveAlert(Vector3 playerPosition)
    {
        isAlerted = true;
        playerDetected = true;
        lastKnownPlayerPosition = playerPosition;
        // Debug.Log($"{gameObject.name} received alert about player at {player.position}");
        
    }

    protected virtual void Patrol()
    {

    }

    protected virtual void ChasePlayer()
    {
        Vector2 direction =(lastKnownPlayerPosition  - transform.position).normalized;

        rb.velocity = new Vector2(direction.x * chaseSpeed,rb.velocity.y);

        float distance =Vector2.Distance(transform.position,lastKnownPlayerPosition);

        // Reached last known position
        if (distance < 0.5f)
        {
            rb.velocity = Vector2.zero;

            isAlerted = false;
            playerDetected = false;
        }

        // FlipSprite(direction.x);
    }

    protected virtual void ReturnToPatrol()
    {
        isAlerted = false;
    }

    // protected virtual void FlipSprite(float directionX)
    // {
    //     if (directionX > 0.1f)
    //     {
    //         transform.localScale = new Vector3(1, 1, 1);
    //     }
    //     else if (directionX < -0.1f)
    //     {
    //         transform.localScale = new Vector3(-1, 1, 1);
    //     }
    // }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            transform.position,
            alertRange
        );
    }

}
