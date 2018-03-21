using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideTimer : MonoBehaviour {
    // class variables
    public float lifeTime;
    public float xAdjust;
    public float yAdjust;
    private void Start()
    {
        if (GameObject.Find("Player"))
        {
            transform.position = new Vector2(GameObject.Find("Player").transform.position.x + xAdjust, GameObject.Find("Player").transform.position.y + yAdjust);
        }
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
