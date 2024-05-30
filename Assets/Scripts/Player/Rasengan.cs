using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Rasengan : MonoBehaviour
{
    public int rasenganDamage;
    public float rasenganStrength;
    [HideInInspector]
    public Transform lockPosition;

    private void Update()
    {
        if(lockPosition != null)
            transform.position = lockPosition.position;
    }

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
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Ihealth>().TakeDamage(rasenganDamage);
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
