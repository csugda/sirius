//Author: Jonathan Griego
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePush : MonoBehaviour {
    // Class variables
    public float coolDown;
    public float radius;
    public Vector2 force;
    public float damage;
    
    public GameObject pushEffect;

    float coolDownTimer;
    bool onCooldown = false;
	// Use this for initialization
	void Start () {
        coolDownTimer = coolDown;
	}
	
	// Update is called once per frame
	void Update () {

        coolDownTimer -= Time.deltaTime;

        if (coolDownTimer <= 0)
        {
            onCooldown = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && !onCooldown)
        {
            push();
            onCooldown = true;
            coolDownTimer = coolDown;
        }
    }

    void push()
    {
        
        //Show Effect
        Instantiate(pushEffect, transform.position, transform.rotation);
        try
        {
           //GameObject.Find("PlasmaExplosionEffect(Clone)").layer = ;

        }
        catch (System.Exception)
        {

            throw;
        }

        //stop effect
        //float effectDuration = 2.00f;
        //while (effectDuration > 0)
        //{
        //    effectDuration -= Time.deltaTime;
        //    if (effectDuration <= 0)
        //    {
        //        try
        //        {
        //            //Destroy(GameObject.Find("PlasmaExplosionEffect(Clone)"));
        //        }
        //        catch (System.Exception)
        //        {

        //            throw;
        //        }
        //    }
        //}

        //Get nearby objects
        Collider2D[] nearbyColliders= Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D nearbyObject in nearbyColliders)
        {
            Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();
            //Make sure there is a rigid body and the object is an "Enemy"
            if (rb != null && nearbyObject.gameObject.CompareTag("Enemy")){
                //Add force
                rb.AddForce(force, ForceMode2D.Impulse);
                //Do damage(?)
            }
        }
    }
}
