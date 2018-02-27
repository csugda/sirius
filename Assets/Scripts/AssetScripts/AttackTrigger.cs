using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    public int damage;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.CompareTag("Enemy"))
        {
            //FIXME do damage.        
        }
    }
}
