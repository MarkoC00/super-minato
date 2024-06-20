using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public SpriteRenderer sp;
    public Animator animator;
    public Camera cam;

    [Header("Movement")]
    //Run
    private float movementDir;
    public float movementSpeed = 7f;      
    private bool changingDirection => (rb.velocity.x > 0f && movementDir < 0f) || (rb.velocity.x < 0f && movementDir > 0f);

    //Jump
    public bool isJumping
    {
        get
        {
            return colliderCount == 0;
        }
    }
    public int colliderCount = 0;
    public float jumpForce = 25f;
    public int availableJumps = 1;

    public Transform respawnPosition;
    [HideInInspector]
    public bool respawnEnabled = false;

    void Update()
    {
        movementDir = GetInput().x;
        CheckForJump();

        if (availableJumps > 1)
            availableJumps = 1;
    }
    private void FixedUpdate()
    {
        GetInput(); //Proveri jel moze bez ovoga ovde
        FlippSpriteWhenChangingDir();
        Run();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "jumpable")
        {
            colliderCount++;
            availableJumps++;
            animator.SetBool("IsJumping", isJumping);
        }

        if (collision.gameObject.tag == "WorldBottom")
        {
            if(!respawnEnabled)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Respawn();
            }

            
        }
        
        if (collision.gameObject.tag == "CameraDown")
        {
            cam.GetComponent<CameraFollow>().shouldOffset = true;
        }
        if (collision.gameObject.tag == "CemaeraUpp")
        {
            cam.GetComponent<CameraFollow>().shouldOffset = false;
        }

        if(collision.gameObject.tag == "ArenaEnter")
        {
            collision.gameObject.GetComponentInParent<Arena>().StartArena();
        }
        if (collision.gameObject.tag == "NextLVL")
        {
            StaticData.playerHealth = GetComponent<PlayerCombat>().currentHelath;
            StaticData.playerRasenganCharge = GetComponent<PlayerCombat>().rasenganCharge;
            StaticData.playerKunaiCharge = GetComponent<PlayerCombat>().kunaiCharge;

            SceneManager.LoadScene("lvl2");
        }
        if(collision.gameObject.tag == "Respawn")
        {
            respawnEnabled = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "jumpable")
        {
            colliderCount--;
            animator.SetBool("IsJumping", isJumping);
        }
    }


    //Prikupljanje informacija
    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void CheckForJump()
    {
        if (Input.GetButtonDown("Jump") && availableJumps > 0)
        {
            Jump();
        }
    }

    //Akcije
    private void Run()
    {
        if(!isJumping)
           animator.SetFloat("Speed", Mathf.Abs(movementDir));

        rb.velocity = new Vector2(movementDir * movementSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

        if (isJumping)
        {
            rb.AddForce(new Vector2(0, jumpForce * 1.05f), ForceMode2D.Impulse);
        }

        availableJumps--;
    }

    public void Respawn()
    {
        gameObject.GetComponent<PlayerCombat>().SetHealthOnRespawn();
        this.transform.position = respawnPosition.position;
        cam.transform.position = respawnPosition.position;
    }

    //Pomoci
    private void FlippSpriteWhenChangingDir()
    {
        switch (movementDir)
        {
            case 1f:
                sp.flipX = false;
                break;
            case -1f:
                sp.flipX = true;
                break;
        }
    }
}
