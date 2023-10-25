using UnityEngine;

public class Skeleton_Enemy : MonoBehaviour
{
    private Rigidbody2D _enemyBody;

    private float _moveSpeed = 1.4f;
    private int _patrolDistance = 2;
    private int _patrolDirection = 1;
    private float _maxX;
    private float _minX;
    private bool _patrol = false;
    private Transform _player;

    private Animator _anim;

    public Transform attackPos;
    public float attackRange;
    public LayerMask playerLayer;

    
    private int _pdamage = 30;


    private void Awake()
    {
        _enemyBody = GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Assassin").transform;
        _anim = GetComponent<Animator>();
        
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
        
        if (enemyPlayerDistance <= 0.4f)
        {
            _enemyBody.velocity = Vector3.zero;
            _anim.SetTrigger("Attack");
        }     
        else if(enemyPlayerDistance <= 1.5)
        {
            _patrol = false;
            _anim.SetTrigger("Detect");

            float runSpeed = _moveSpeed * 2;
            Vector2 moveDirection = (_player.position - transform.position).normalized * runSpeed;
            if(moveDirection.x >0)
            {
                FaceDirection(Vector2.right);
            }
            else if (moveDirection.x < 0)
            {
                FaceDirection(Vector2.left);
            }

            // move the enemy towards player
            _enemyBody.velocity = new Vector2(moveDirection.x, transform.position.y);

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

    //FlipDirection while patrolling
    public void FlipDirection()
    {
        _patrolDirection *= -1;

        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
    }
    

    // FaceDirection once the player is detected
    public void FaceDirection(Vector2 moveDirection)
    {
        if (moveDirection == Vector2.right)
        {
            transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
        }
        else
        {
            transform.localScale = new Vector3(-1.7f, 1.7f, 1.7f);
        }
    }

    public void AttackPlayer()
    {
            

        Collider2D attackPlayer = Physics2D.OverlapCircle(attackPos.position, attackRange, playerLayer);
        if (attackPlayer != null)
        {
            if (attackPlayer.tag == "Player")
            {
                attackPlayer.gameObject.GetComponent<PlayerHealth>().PlayerDamage(_pdamage);
            }
        }
        else
        {
            Debug.LogError("No Player");
        }

    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}
