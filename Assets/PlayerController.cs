using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D player;
    public float moveForce, decelModifier, maxSpeed;
	// Use this for initialization
	void Start () {
        player = this.gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow) && player.velocity.x > -maxSpeed)
            player.AddForce(new Vector2(-moveForce, 0));
        else if (Input.GetKey(KeyCode.RightArrow) && player.velocity.x < maxSpeed)
            player.AddForce(new Vector2(moveForce, 0));

        if (player.velocity.x != 0)
        {
            player.velocity = (Vector2.Lerp(player.velocity, new Vector2(0, player.velocity.y), decelModifier)); 
        }
    }
}
