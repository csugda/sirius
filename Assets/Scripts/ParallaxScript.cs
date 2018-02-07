using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    // class variables
    public bool scrolling, parallax;
    public float backgroundSize;
    public float paralaxSpeed;
    
    private Transform cameraTransform;
    private Transform[] layers;
    private float viewZone = 10f;
    private int leftIndex;
    private int rightIndex;
    private float lastCameraX;

    private void Start()
    {
        // grabs the main camera
        cameraTransform = Camera.main.transform;
        // tracks camera position
        lastCameraX = cameraTransform.position.x;

        // creates an array of length equal to the number of tiles 
        layers = new Transform[transform.childCount];

        // populates the array with each child/tile
        for (int i = 0; i < transform.childCount; i++)
            layers[i] = transform.GetChild(i);

        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }

    private void Update()
    {
        if (parallax)
        { 
            float deltaX = cameraTransform.position.x - lastCameraX;
            transform.position += Vector3.right * (deltaX * paralaxSpeed);
        }
        // updates cameras last known position
        lastCameraX = cameraTransform.position.x;

        if (scrolling)
        {
            if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
                ScrollLeft();

            if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
                ScrollRight();
        }
    }
    // called when going to far to left/right
    private void ScrollLeft()
    {
        // temp variable to keep track of the last index
        int lastRight = rightIndex;

        // takes very right image and puts it on the left 
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);

        // now redefine indices
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
            rightIndex = layers.Length - 1;
    }
    private void ScrollRight()
    {
        // temp variable to keep track of the last index
        int lastLeft = leftIndex;

        // takes very left image and puts it on the right 
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
    
        // now redefine indices
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
            leftIndex = 0;
    }
}
