using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class Pipe : MonoBehaviour
{
    public Rigidbody2D rb;

    [HideInInspector]
    public bool isFrozen;

    private void Start()
    {
        isFrozen = true;
    }
    void Update()
    {
        if (isFrozen)
        {
            rb.constraints |= RigidbodyConstraints2D.FreezePositionX;
            rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }

    public void UnfreezePipe()
    {
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;

        Invoke(nameof(DestroyMe), 1f);
    }
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
