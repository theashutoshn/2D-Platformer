using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _anim;

    private bool _activeTimeToReset;

    private float _defaultComboTimer = 0.2f, _currentComboTimer;

    private int _combo;


    public Transform attackPos;
    public float attackRange;
    public LayerMask enemyLayer;

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ResetComboState();
        SwordAttack();
    }

    void SwordAttack()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            if(_combo < 3)
            {
                _anim.SetBool("SwordAttack", true);
                _activeTimeToReset = true;
                _currentComboTimer = _defaultComboTimer;

                Collider2D[] attackEnemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
                Debug.Log("Enemies in range: " + attackEnemies.Length);
                //for (int i = 0; i< attackEnemies.Length; i++)
                //{
                //    Debug.Log("Hit");
                //}

            }
            else
            {
                _anim.SetBool("SwordAttack", false);
            }
        }
    }

    void IncreaseComboNumber()
    {
        _combo++;
    }

    void ResetCombo()
    {
        _combo = 0;
    }

    void ResetComboState()
    {
        if (_activeTimeToReset)
        {
            _currentComboTimer -= Time.deltaTime;
            if(_currentComboTimer <= 0)
            {
                _anim.SetBool("SwordAttack", false);
                _activeTimeToReset = false;
                _currentComboTimer = _defaultComboTimer;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}
