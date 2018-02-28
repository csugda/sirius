using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    private Animator anim;
    private Transform player;
    private BoxCollider2D playerCollider;
    public Vector3 velocity;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        player = this.gameObject.transform;
        playerCollider = this.gameObject.GetComponent<BoxCollider2D>();
        velocity = new Vector3(0, 0, 0);
    }

    public float speed;
    public float gravity;
    public float jumpPower;
    //public float maxDownSpeed;
    public bool down = false;
    private bool canDoubleJump = true;
    // Update is called once per frame
    void Update()
    {
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

        player.GetComponent<Rigidbody2D>().MovePosition(player.position + (velocity * Time.deltaTime));

        //player.SetPositionAndRotation(player.position + ( velocity*Time.deltaTime), player.rotation);
    }
    public float groundFallVelocity;
    public int touching = 0;
    public int maxDownSpeed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        touching++;
        //Debug.Log("hit");
        if (velocity.y < 0)
        {
            down = true;
        }
        velocity.y = groundFallVelocity;// * Time.deltaTime;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        touching--;
        if (touching == 0)
        {
            down = false;
        }
    }
}
