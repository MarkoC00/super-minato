using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neadles : MonoBehaviour
{
    public Rigidbody2D rb;
    public int damage = 10;
    public float moveSpeed = 7.5f;

    private void Update()
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
