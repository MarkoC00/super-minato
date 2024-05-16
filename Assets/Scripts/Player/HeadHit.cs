using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadHit : MonoBehaviour
{
    public PlayerCombat pm;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "SecretBox")
        {
            pm.HitSecretBox();

            collision.gameObject.GetComponent<SecretBox>().DestroyMe();
        }
    }

}
