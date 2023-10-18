using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : MonoBehaviour
{
    private Rigidbody2D _enemyBody;

    [Header ("Movement")]
    public float distance;
    public int direction;
    private float _moveSpeed = 0.7f;
    private float _minX, _maxX;
    

    private bool _patrol;
    private Transform _playerPos;

    private Animator _anim;


    [Header("Attack")]
    public Transform attackPos;
    public float attackRange;
    public LayerMask playerLayer;
    public int damage;
    private bool _detect = false;


    private void Awake()
    {
        _anim = GetComponent<Animator>();
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
        //patrol ON/OFF when the player is in the range of the enemy.

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
        if (_anim.GetBool("Death"))
        {
            _enemyBody.velocity = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;
            _enemyBody.isKinematic = true;
            _anim.SetBool("Attack", false);
            return;
        }

        // enemy flipping the direction
        if (direction > 0)
        {
            transform.localScale = new Vector2(1.7f, -transform.position.y);
            _anim.SetBool("Attack", false);
        }
        else if (direction < 0)
        {
            transform.localScale = new Vector2(-1.7f, -transform.position.y);
        }


        // patrol dirction and movement of the skeleton
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
        else
        {
            if (Vector2.Distance(_playerPos.position, transform.position) <= 1.5f )
            {
                if (!_detect)
                {
                    _detect = true;
                    _anim.SetTrigger("Detect");
                    Debug.Log("Detect triggered");
                }

                if (_anim.GetCurrentAnimatorStateInfo(0).IsName("SkeletonDetect"))
                {
                    return;
                }
            }
    }
}

   
}
