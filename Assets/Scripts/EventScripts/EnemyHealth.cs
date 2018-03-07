using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour {
    // Use this for initialization
    public float currentHealth, maxHealth;


    void Start () {
        Events.HitEnemy.AddListener(takeDamage);
        currentHealth = maxHealth;
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void takeDamage(float damage)
    {
        currentHealth = currentHealth - damage;
    }
}
