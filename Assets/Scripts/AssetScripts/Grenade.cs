//Author: Jonathan Griego
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    // Apple of Exploding
    //class variables
    public float fuseTime; // time till detonation, can be set in the inspector
    float countDown;
    bool hasDetonated = false;

    void Start()
    {
        //set countdown time to match fuse
        countDown = fuseTime;
    }

    void Update()
    {
        // reduce countdown every frame
        countDown -= Time.deltaTime;

        // once countdown is complete and if it has not detonated yet
        if (countDown <= 0f && !hasDetonated)
        {
            detonate();
            hasDetonated = true;
            Destroy(gameObject);
        }
         
    }

    void detonate()
    {
        Debug.Log("KABLOOY");
    }
}