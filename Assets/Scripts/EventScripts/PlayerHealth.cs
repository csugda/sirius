using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerHealth : MonoBehaviour {
    public float currentHealth, maxHealth;
    public float collisionDamage;

    // Use this for initialization
    void Start () {
        Events.HitPlayer.AddListener(OnHealthChange);
        maxHealth = 3;
        currentHealth = maxHealth;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //test invoke
        if (collision.gameObject.tag == "Enemy")
        {
            Events.HitEnemy.Invoke(collisionDamage);
        }
    }

    private void OnHealthChange(float healthChange)
    {
        currentHealth = currentHealth - healthChange;
    }

}
