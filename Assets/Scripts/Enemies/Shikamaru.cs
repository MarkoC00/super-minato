using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Shikamaru : MonoBehaviour, Ihealth
{
    [Header("Reference")]
    public GameObject healthBar;
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject player;
    public SpriteRenderer sp;
    public GameObject wall;


    [Header("Health")]
    public int maxHealth;
    int currentHealth;

    [Header("Combat")]
    public float attackRange;
    public int kickDamage;
    public float movementSpeed;

    [HideInInspector]
    public bool startCombat = false;

    //svastara
    public bool inKnockBack = false;
    bool canKickAgain = true;
    int moveDir = 0;
    bool canMoveAttack = true;

    private void Start()
    {
        healthBar.GetComponent<HealthSlider>().SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(moveDir == 1)
            sp.flipX = false;
        if (moveDir < 0)
            sp.flipX = true;

        if (startCombat && canMoveAttack)
        {
            StartAttacking();
        }

    }

    //Da pridje i udara
    void StartAttacking()
    {
        if (inKnockBack) return;

        if(Mathf.Abs(this.transform.position.x - player.transform.position.x) > attackRange)
        {
            Move();
        }
        else
        {
            //Zaustavi me
            rb.velocity = Vector3.zero;
            anim.SetFloat("Speed", Mathf.Abs(0));

            //Udaraj
            if(canKickAgain)
                Kick();
        }
    }
    void Move()
    {
        if (this.transform.position.x - player.transform.position.x >= 0)
            moveDir = -1;
        if (this.transform.position.x - player.transform.position.x <= 0)
            moveDir = 1;

        rb.velocity = new Vector2(moveDir * movementSpeed, rb.velocity.y);

        anim.SetFloat("Speed", Mathf.Abs(moveDir));
    }

    void Kick()
    {
        anim.SetBool("Kick", true);
    }
    public void DealKickDamage()
    {
        player.GetComponent<PlayerCombat>().TakeDamage(kickDamage);
        anim.SetBool("Kick", false);

        canKickAgain = false;
        Invoke("RestartCanKick", 1);
    }
    void RestartCanKick()
    {
        canKickAgain = true;
    }

    public void TakeDamage(int dmg)
    {
        currentHealth-= dmg;
        healthBar.GetComponent<HealthSlider>().SetHealth(currentHealth);
        KnowckBack(dmg);
        anim.SetBool("TakeDamage", true);

        if(currentHealth <= 0)
        {
            anim.SetBool("Death", true);
        }
    }

    public void KnowckBack(int dmg)
    { 
        rb.AddForce(Vector2.right * (dmg*0.1f), ForceMode2D.Impulse);

        inKnockBack = true;

        Invoke("StopKnockback", 0.5f);
    }
    void StopKnockback()
    {
        inKnockBack = false;
    }


    //Funkcije za animator
    public void StopTakeDamge() {
        anim.SetBool("TakeDamage", false);
    }
    public void DeathFunc()
    {
        GetComponent<Shikamaru>().enabled = false;
        wall.SetActive(false);

    }
    public void DisableMovementAndCombat()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        canMoveAttack = false;
    }
}
