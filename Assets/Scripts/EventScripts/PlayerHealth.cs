using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerHealth : MonoBehaviour {
    public int currentHealth, maxHealth;
    public float collisionDamage;
    public GameObject UI_Full, UI_1, UI_2, UI_Empty;
    // Use this for initialization
    void Start () {
        Events.HitPlayer.AddListener(OnHealthChange);
        maxHealth = 4;
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

    private void OnHealthChange(int healthChange)
    {
        currentHealth = currentHealth - healthChange;
        switch (currentHealth) {
            case 4:
                UI_Full.SetActive(true);
                UI_1.SetActive(false);
                UI_2.SetActive(false);
                UI_Empty.SetActive(false);
                break;
            case 3:
                UI_Full.SetActive(false);
                UI_1.SetActive(true);
                break;
            case 2:
                UI_1.SetActive(false);
                UI_2.SetActive(true);
                break;
            case 1:
                UI_2.SetActive(false);
                UI_Empty.SetActive(true);
                break;
            case 0:
                Debug.LogError("Player Died");
                break;
        }

    }

}
