using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDirection : MonoBehaviour {
    private SpriteRenderer attackBox;
    //private Vector2 originalPos = new Vector3(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y);
    //private Vector2 flippedPos = new Vector3(-0.7f, 1.21f);
    private void Awake()
    {
        attackBox = GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
        if (attackBox.flipX)
        {
            transform.position = new Vector3((GameObject.Find("Player").transform.position.x - 0.7f), GameObject.Find("Player").transform.position.y + 0.968f);
        }
        else
        {
            transform.position = new Vector3((GameObject.Find("Player").transform.position.x + 0.7f), GameObject.Find("Player").transform.position.y + 0.968f);
        }
	}
}
