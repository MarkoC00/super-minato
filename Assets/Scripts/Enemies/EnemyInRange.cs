using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInRange : MonoBehaviour
{
    public List<GameObject> enemiesInRange = new List<GameObject>();
    public GameObject currentTarget
    {
        get
        {
            if (enemiesInRange.Count == 0)
                return null;
            else
                return enemiesInRange[0];
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !enemiesInRange.Contains(collision.gameObject))
        {
            enemiesInRange.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemiesInRange.Remove(collision.gameObject);
        }
    }
}
