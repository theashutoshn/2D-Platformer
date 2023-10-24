using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int pHealth = 200;

    private Animator _anim;
    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pHealth < 1)
        {
            Debug.Log("Dead");
        }
    }

    public void PlayerDamage(int damage)
    {
        //_anim.SetTrigger("Player Hit");
        pHealth -= damage;
        //if(playerHealth < 0)
        //{
        //    _anim.SetBool("Player Death", true);
        //}

    }
}
