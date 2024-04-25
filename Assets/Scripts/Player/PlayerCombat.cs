using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public PlayerMovement movement;

    private int rasenganCharge = 0;

    public Transform rasPosR;
    public Transform rasPosL;
    public GameObject rasenganPrefab;

    public int rasenganChargePerSecret;


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

        }
        if (collision.gameObject.tag == "SecretBox")
        {
            rasenganCharge += rasenganChargePerSecret;

            collision.gameObject.GetComponent<SecretBox>().DestroyMe();
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
            if(rasenganCharge > 0)
            {
                Rasengan();
            }
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

    public void Rasengan()
    {
        animator.SetBool("Rasengan", true);
        rasenganCharge -= 1;
    }

    public void StopRasengan()
    {
        animator.SetBool("Rasengan", false);
    }

    public void SpawnRasengan()
    {
        Instantiate(rasenganPrefab, rasPosR.position, Quaternion.identity);
    }
}
