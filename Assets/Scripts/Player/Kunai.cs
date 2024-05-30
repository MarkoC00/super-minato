using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sp;
    public float kunaiSpeed;

    [HideInInspector]
    public float direction = 0;

    
    void Start()
    {
        rb.velocity = new Vector3(kunaiSpeed*direction, rb.velocity.y, 0);
    }

    private void Update()
    {
        if(direction == -1)
        {
            sp.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "jumpable" || collision.gameObject.tag == "Enemy")
            rb.velocity = Vector3.zero;
    }
}
