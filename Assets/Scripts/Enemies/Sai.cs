using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sai : MonoBehaviour, Ihealth
{
    [Header("Reference")]
    public GameObject healthBar;
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject player;
    public SpriteRenderer sp;
    public GameObject lionPrefab;
    public Transform spawnLeft;
    public Transform spawnRight;
    public GameObject victory;

    [Header("Health")]
    public int maxHealth;
    int currentHealth;

    [Header("Combat")]
    public float attackRange;

    //svastara
    int moveDir = 0;

    private void Start()
    {
        healthBar.GetComponent<HealthSlider>().SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
    }

    private void Update()
    {
        CalculateDirection();
        StartAttacking();
    }

    void CalculateDirection()
    {
        if (this.transform.position.x - player.transform.position.x >= 0)
            moveDir = -1;
        if (this.transform.position.x - player.transform.position.x <= 0)
            moveDir = 1;

        if (moveDir == 1)
            sp.flipX = false;
        if (moveDir < 0)
            sp.flipX = true;
    }

    void StartAttacking()
    {
        if (Mathf.Abs(this.transform.position.x - player.transform.position.x) < attackRange)
        {
            healthBar.SetActive(true);
            Attack();
        }
        else
        {
            StopAttack();
        }
    }

    void Attack()
    {
        anim.SetBool("Attack", true);
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        healthBar.GetComponent<HealthSlider>().SetHealth(currentHealth);

        Vector2 newPos = new Vector2 (this.transform.position.x + moveDir * (attackRange + 1.5f), this.transform.position.y);
        this.transform.position = newPos;

        if (currentHealth <= 0)
        {
            victory.SetActive(true);
            Destroy(gameObject);
        }
    }

    public void StopAttack()
    {
        anim.SetBool("Attack", false);
    }

    public void SummonLion()
    {
        if (sp.flipX)
        {
            Instantiate(lionPrefab, spawnLeft.position, Quaternion.identity);
        }
        else
        {
            Instantiate(lionPrefab, spawnRight.position, Quaternion.identity);
        }
    }
}
