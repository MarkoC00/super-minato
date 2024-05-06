using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EasyEnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public float moveSpeed = 4;
    int movementDir = 1;

    void Update()
    {
        rb.velocity = new Vector2(moveSpeed * movementDir, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "npcMovementTriggerRight")
        {
            movementDir = -1;
        }
        if (collision.gameObject.tag == "npcMovementTriggerLeft")
        {
            movementDir = 1;
        }
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Pitati profesora
        }
    }

    public void UnistiMe()
    {
        Destroy(gameObject);
    }
}
