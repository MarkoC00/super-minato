using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lion : MonoBehaviour, Ihealth
{
    [Header("Reference")]
    public SpriteRenderer sp;
    public Rigidbody2D rb;
    GameObject player;

    [Header("Promenljive")]
    public float movementSpeed;
    public int damage;

    //svastara
    int moveDir = 0;
    bool canMove = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        Invoke("DestroyMe", 1.5f);
    }

    private void Update()
    {
        CalculateDirection();

        if(canMove)
            rb.velocity = new Vector2(moveDir * movementSpeed, rb.velocity.y);
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

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    public void SetCanMoveToTrue()
    {
        canMove = true;
    }

    public void TakeDamage(int dmg)
    {
        Destroy(gameObject);
    }
}
