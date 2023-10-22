using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 200;

    private Animator _anim;
    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDamage(int damage)
    {
        _anim.SetTrigger("Player Hit");
        playerHealth -= damage;
        if(playerHealth < 0)
        {
            _anim.SetBool("Player Death", true);
        }

    }
}
