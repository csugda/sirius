using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    private Animator anim;
    private Transform player;
    private BoxCollider2D playerCollider;
    private Rigidbody2D playerRigidbody;
    public Vector3 velocity;
    private ContactFilter2D floor;
    private ContactFilter2D platform;
    // Use this for initialization
    void Start()
    {
        floor.SetLayerMask(LayerMask.GetMask("Floor"));
        floor.useLayerMask = true;
        floor.SetLayerMask(LayerMask.GetMask("Platform"));
        floor.useLayerMask = true;
        anim = this.gameObject.GetComponent<Animator>();
        player = this.gameObject.transform;
        playerCollider = this.gameObject.GetComponent<BoxCollider2D>();
        playerRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        velocity = new Vector3(0, 0, 0);
    }

    public float speed;
    public float gravity;
    public float jumpPower;
    public float groundFallVelocity;
    public int maxDownSpeed;
    private bool down = false;
    private bool canDoubleJump = true;
    // Update is called once per frame
    void Update()
    {
        down = playerCollider.IsTouching(floor) || playerCollider.IsTouching(platform);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("isMoving", true);
            anim.SetFloat("faceL", 1);
            anim.SetFloat("moveL", 1);
            velocity.x = -speed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("isMoving", true);
            anim.SetFloat("faceL", 0);
            anim.SetFloat("moveL", 0);
            velocity.x = speed;
        }
        else
        {
            anim.SetBool("isMoving", false);
            velocity.x = 0;
        }

        //in the air, gravity in effect
        if (!down)
        {
            velocity.y -= gravity * Time.deltaTime;
        }

        if (velocity.y < -maxDownSpeed)
        {
            Debug.LogError("Player tried to fall through the floor; Correcting");
            velocity.y = 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (down || (!down && canDoubleJump))
            {
                velocity.y = jumpPower;
                canDoubleJump = down;
            }
        }

        playerRigidbody.MovePosition(player.position + (velocity * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
            velocity.y = groundFallVelocity;
    }
}
