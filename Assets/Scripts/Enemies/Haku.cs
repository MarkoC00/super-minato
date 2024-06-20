using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Haku : MonoBehaviour, Ihealth
{
    public GameObject player;
    public Animator animator;
    public GameObject neadlePrefab;
    public Transform pos;

    public int health = 10;
    public float attackDelay = 1.5f;
    bool attackDelayPassed = true;
    bool canAttakc = false;

    private void Update()
    {
        if (canAttakc && attackDelayPassed)
        {
            Attack();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canAttakc = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canAttakc = false;
        }
    }

    void Attack()
    {
        animator.SetBool("Throw", true);

        attackDelayPassed = false;
        Invoke("ResetAttackDelay", attackDelay);
    }

    public void ResetAttackDelay()
    {
        attackDelayPassed = true;
    }


    public void TakeDamage(int dmg)
    {
        Destroy(gameObject);
    }

    public void InstanciateNeadles()
    {
        Instantiate(neadlePrefab, pos.position, Quaternion.identity);
    }

    public void StopAttackAnimation()
    {
        animator.SetBool("Throw", false);
    }
}
