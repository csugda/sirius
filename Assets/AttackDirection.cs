using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDirection : MonoBehaviour {
    private SpriteRenderer attackBox;

    public float xAdjust = 0.7f;
    public float yAdjust = 0.968f;

    private void Awake()
    {
        attackBox = GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>();
    }

    void Update () {
        if (attackBox.flipX)
        {
            transform.position = new Vector3((GameObject.Find("Player").transform.position.x - xAdjust), GameObject.Find("Player").transform.position.y + yAdjust);
        }
        else
        {
            transform.position = new Vector3((GameObject.Find("Player").transform.position.x + xAdjust), GameObject.Find("Player").transform.position.y + yAdjust);
        }
	}
}
