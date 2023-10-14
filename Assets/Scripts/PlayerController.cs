using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _runSpeed = 3f;

    [SerializeField]
    private float _jumpForce = 5f;
    
    //[SerializeField]
    //private float _jumpHeight = 0.5f;

    private float _moveInput;

    // to get the Rigidbody2D component of the player
    private Rigidbody2D _myBody;

    //bool to flip the player
    private bool _faceRight;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public Vector3 range;

    private Animator _anim;

    private void Awake()
    {
        // asgined the rigidbody2d to _mybody
        _myBody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        CollisionJumpCheck();
    }

    public void Movement()
    {
        _moveInput = Input.GetAxisRaw("Horizontal") * _runSpeed;

        _anim.SetFloat("Speed", Mathf.Abs(_moveInput));

        _myBody.velocity = new Vector2(_moveInput, _myBody.velocity.y);

        if (_moveInput > 0 && _faceRight || _moveInput < 0 && !_faceRight)
        {
            Flip();
        } 
        
        if(_myBody.velocity.y < 0)
        {
            _anim.SetBool("Fall", true);
        }
        else
        {
            _anim.SetBool("Fall", false);
        }
        //if(Input.GetKeyUp(KeyCode.Space))
        //{
        //    if(_myBody.velocity.y > 0)
        //    {
        //        _myBody.velocity = new Vector2(_myBody.velocity.x, _myBody.velocity.y * _jumpHeight);
        //    }
        //}
    }

    void CollisionJumpCheck()
    {
        Collider2D bottomHit = Physics2D.OverlapBox(groundCheck.position, range, 0, groundLayer);
        
        if(bottomHit != null)
        {
            if(bottomHit.gameObject.tag == "Ground" && Input.GetKey(KeyCode.Space))
            {
                _myBody.velocity = new Vector2(_myBody.velocity.x, _jumpForce);
                _anim.SetBool("Jump", true);
            }
            else
            {
                _anim.SetBool("Jump", false);
            }
       

        }
    }

    // below code will show the wire box on the feet of the player.

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(groundCheck.position, range);
    //}

    public void Flip()    
    {
        _faceRight = !_faceRight;

        Vector3 transformScale = transform.localScale;
        transformScale.x *= -1;
        transform.localScale = transformScale;

    }
}
