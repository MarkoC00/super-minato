using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public Rigidbody2D rb;
    public float kunaiSpeed;
    
    void Start()
    {
        rb.velocity = new Vector3(kunaiSpeed, rb.velocity.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "jumpable")
            rb.velocity = Vector3.zero;
    }
}
