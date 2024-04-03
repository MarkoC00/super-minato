using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public PlayerMovement movement;

    GameObject enemyInRange;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForAttack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyInRange = collision.gameObject;
            Debug.Log("colideovali");
        }
    }

    void CheckForAttack()
    {
        if (movement.isJumping)
            return; //Ukoliko ne skace

        if (Input.GetKeyDown(KeyCode.J))
        {
            NormalAttack();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //Rasengan
        }
    }

    void NormalAttack()
    {
        animator.SetBool("Attack", true);   
    }

    //Funkcije za animaciju
    public void SetAttackToFalse()
    {
        animator.SetBool("Attack", false);
    }
    public void DealDamage()
    {
        enemyInRange.GetComponent<EasyEnemyMovement>().UnistiMe();
    }
}
