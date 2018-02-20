using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideTimer : MonoBehaviour {
    // class variables
    public float lifeTime;
	// Update is called once per frame
	void Update () {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
	}
}
