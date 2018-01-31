using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour {
    private Transform player;
    private BoxCollider2D playerCollider;
    public  Vector3 velocity;
    // Use this for initialization
    void Start () {
        player = this.gameObject.transform;
        playerCollider = this.gameObject.GetComponent<BoxCollider2D>();
        velocity = new Vector3(0, 0, 0);
	}

    public float speed;
    public float gravity;
    public float jumpPower;
    private bool down = false;
    private bool canDoubleJump  = true;
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey(KeyCode.LeftArrow))
        {
            velocity.x = -speed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            velocity.x = speed;
        }
        else
        {
            velocity.x = 0;
        }

        if (!down)
        {
            velocity.y -= gravity * Time.deltaTime;
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
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        //Debug.Log("hit");
        if (velocity.y < 0)
        {
            down = true;
        }
        velocity.y = 0;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        down = false;
    }
}
