using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideTimer : MonoBehaviour {
    // class variables
    public float lifeTime;
    private void Start()
    {
        transform.position = new Vector2(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y + 1);
    }
    // Update is called once per frame
    void Update () {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
	}
}
