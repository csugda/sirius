using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateTimer : MonoBehaviour {
    public float duration;
    private float timer;
    
    void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        timer = duration;
    }
	
	void Update () {
		if (gameObject.GetComponent<BoxCollider2D>().enabled == true)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            timer = duration;
        }
	}
}
