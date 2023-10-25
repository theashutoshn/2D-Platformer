using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 200;

    private Animator _anim;

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        _anim.SetTrigger("Hit");
        health -= damage;
        if(health < 0)
        {
            _anim.SetBool("Death", true);
            
            
        }
    }

    // This method is added in the animation event
    public void EnemyDestory()
    {
        Destroy(gameObject);
    }
}
