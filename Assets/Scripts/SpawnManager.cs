using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float _arrowSpeed = 0.05f;
    private bool _right;
    void Start()
    {
        // Access the player direction 
        if (GameObject.FindGameObjectWithTag("Player").transform.localScale.x > 0)
        {
            //flipping the scale of the arrow
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
            _right = true;
            
        }
        else
        {
            
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            _right = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // based on  the player direction, the arrow will move 
        if (_right)
        {
            transform.Translate(Vector2.right * _arrowSpeed);
            
        }
        else
        {
            transform.Translate(Vector2.left * _arrowSpeed);
            
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(this.gameObject);
            other.GetComponent<EnemyHealth>().TakeDamage(20);
        }
    }

    //public void ArrowFlip()
    //{
    //    Vector3 arrowDirection = transform.localScale;
    //    arrowDirection *= -1;
    //    transform.localScale = arrowDirection;
    //}
}
