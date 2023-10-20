using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Enemy : MonoBehaviour
{
    private Rigidbody2D _enemyBody;

    private float _moveSpeed = 1.4f;
    private int _patrolDistance = 2;
    private int _patrolDirection = 1;
    private float _maxX;
    private float _minX;
    private bool _patrol;
    private Transform _player;


    private void Awake()
    {
        _enemyBody = GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Assassin").transform;
    }

    void Start()
    {
        _maxX = transform.position.x + (_patrolDistance / 2);
        _minX = _maxX - _patrolDistance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float enemyPlayerDistance = Vector2.Distance(_player.position, transform.position);
        
        if(enemyPlayerDistance <= 1.5)
        {
            _patrol = false;
        }
        else
        {
            Patrol();
        }

        
    }

    public void Patrol()
    {
        // moving the enemey between _minX & _maxX
        _enemyBody.velocity = new Vector2(_patrolDirection * _moveSpeed, transform.position.y);
        
        //Flipping the direction once the enemy is at _minX or _maxX position
       if(transform.position.x >= _maxX && _patrolDirection > 0)
       {
            FlipDirection();
       }
       else if (transform.position.x <= _minX && _patrolDirection < 0)
       {
            FlipDirection();
       }

    }

    public void FlipDirection()
    {
        _patrolDirection *= -1;

        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
    }

   
}
