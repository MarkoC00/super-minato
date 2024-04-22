using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public SpriteRenderer sp;
    public Animator animator;

    [Header("Movement")]
    //Run
    private float movementDir;
    public float movementSpeed = 7f;      
    private bool changingDirection => (rb.velocity.x > 0f && movementDir < 0f) || (rb.velocity.x < 0f && movementDir > 0f);

    //Jump
    public bool isJumping = false;
    public float jumpForce = 25f;

    void Update()
    {
        movementDir = GetInput().x;
        CheckForJump();
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
            isJumping = false;
            animator.SetBool("IsJumping", isJumping);
        }

        if (collision.gameObject.tag == "WorldBottom")
        {
            Debug.Log("Ispao sam");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "jumpable")
        {
            isJumping = true;
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
        if (Input.GetButtonDown("Jump") && !isJumping)
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
