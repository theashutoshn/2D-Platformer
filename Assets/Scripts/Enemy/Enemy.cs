using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Rigidbody2D _enemyBody;
    private Animator _anim;
    private Transform _playerPos;
    private bool _detect = false;

    [Header("Movement")]
    public float patrolDistance;
    public float moveSpeed = 0.7f;
    private float _minX, _maxX;
    private bool _isMovingRight = true;

    [Header("Detection")]
    public float detectionRange = 1.5f;

    [Header("Attack")]
    public float attackRange = 0.4f;
    public int damage;

    void Awake()
    {
        _enemyBody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _playerPos = GameObject.FindGameObjectWithTag("Player").transform; // It's safer to use tag than object name

        // Setup patrol boundaries
        _maxX = transform.position.x + (patrolDistance / 2);
        //_minX = transform.position.x - (patrolDistance / 2);
        _minX = _maxX - patrolDistance;
    }

    void FixedUpdate()
    {
        if (_anim.GetBool("Death"))
        {
            _enemyBody.velocity = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;
            _enemyBody.isKinematic = true;
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, _playerPos.position);

        if (distanceToPlayer <= attackRange)
        {
            // Player is within attacking range
            _anim.SetBool("Attack", true);
            //_anim.SetBool("Walk", false); // Assuming "Walk" is your walking animation state
            _enemyBody.velocity = Vector2.zero;
        }
        else if (distanceToPlayer <= detectionRange)
        {
            // Player is within detection range but not close enough to attack
            if (!_detect)
            {
                _detect = true;
                _anim.SetTrigger("Detect");
                //_anim.SetBool("Walk", false);
            }
            // You might want to add additional behavior when the enemy detects the player (like facing the player, etc.)
        }
        else
        {
            // Player is out of detection range, continue patrolling
            Patrol();
        }
    }

    void Patrol()
    {
        _detect = false; // Reset detection status
        _anim.SetBool("Attack", false);
        //_anim.SetBool("Walk", true); // Assuming "Walk" is your walking animation state

        Vector2 patrolDirection = _isMovingRight ? Vector2.right : Vector2.left;
        _enemyBody.velocity = new Vector2(patrolDirection.x * moveSpeed, _enemyBody.velocity.y);

        // Flip the enemy's direction for patrol and adjust boundaries
        if (_isMovingRight && transform.position.x >= _maxX || !_isMovingRight && transform.position.x <= _minX)
        {
            _isMovingRight = !_isMovingRight;
            Vector3 newScale = transform.localScale;
            newScale.x *= -1; // Flip the enemy's direction
            transform.localScale = newScale;
        }
    }
}


