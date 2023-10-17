using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : MonoBehaviour
{
    private Rigidbody2D _enemyBody;

    private float _moveSpeed = 0.7f;
    private float _minX, _maxX;
    public float distance;
    public int direction;

    private bool _patrol;
    private bool _enemyFaceRight = false;
    private Transform _playerPos;


    private void Awake()
    {
        _playerPos = GameObject.Find("Assassin").transform;
        _enemyBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _maxX = transform.position.x + (distance / 2);
        _minX = _maxX - distance;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, _playerPos.position) <= 1.5f)
        {
            _patrol = false;
        }
        else
        {
            _patrol = true;
        }
    }

    private void FixedUpdate()
    {
        if (direction > 0)
        {
            transform.localScale = new Vector2(1.7f, -transform.position.y);
        }
        else if (direction < 0)
        {
            transform.localScale = new Vector2(-1.7f, -transform.position.y);
        }

        if (_patrol)
        {
            switch (direction)
            {
                case -1:
                    if (transform.position.x > _minX)
                    {

                        _enemyBody.velocity = new Vector2(-_moveSpeed, _enemyBody.velocity.y);
                    }
                    else
                    {

                        direction = 1;
                    }
                    break;

                case 1:
                    if (transform.position.x < _maxX)
                    {

                        _enemyBody.velocity = new Vector2(_moveSpeed, _enemyBody.velocity.y);
                    }
                    else
                    {

                        direction = -1;
                    }
                    break;
            }
        }   
    }

   
}
