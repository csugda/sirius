using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
   // variable to hold a reference to SpriteRenderer component
    private SpriteRenderer sRenderer;

    // This is called only once, at load 
    private void Awake()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (sRenderer != null)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                sRenderer.flipX = true;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                sRenderer.flipX = false;
            }
        }
        
    }
}
