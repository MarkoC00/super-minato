using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rasengan : MonoBehaviour
{
    public float rasenganStrength;

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pipes")) 
        {
          

            collision.gameObject.GetComponent<Pipe>().UnfreezePipe();

            collision.gameObject.GetComponent<Pipe>().isFrozen = false;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(rasenganStrength, 0), ForceMode2D.Impulse);
        }
    }
}
