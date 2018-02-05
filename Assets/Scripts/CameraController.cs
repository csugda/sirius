using UnityEngine;

public class CameraController : MonoBehaviour {
    // defines a variable that the camera will target
    public Transform target;

    // speed camera will track to player
    public float trackSpeed = 0.125f;

    // enable and set max Y value
    public bool yMaxEnabled = false;
    public float yMaxValue = 0;

    // enable and set min Y value
    public bool yMinEnabled = false;
    public float yMinValue = 0;

    // enable and set max X value
    public bool xMaxEnabled = false;
    public float xMaxValue = 0;

    // enable and set min X value
    public bool xMinEnabled = false;
    public float xMinValue = 0;

    // creates a variable offset for camera in unity
    public Vector3 cameraOffSet;

    // use late update so it is called last
    private void FixedUpdate()
    {
        // sets position of camera to the player + indicated offset
        Vector3 targetPosition = target.position + cameraOffSet;

        // vertical clamping
        if(yMinEnabled && yMaxEnabled)
        {
            targetPosition.y = Mathf.Clamp(target.position.y, yMinValue, yMaxValue);
        }

        // horizontal clamping
        if (xMinEnabled && xMaxEnabled)
        {
            targetPosition.x = Mathf.Clamp(target.position.x, xMinValue, xMaxValue);
        }

        // smooths movement of camera as it follows player
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, trackSpeed*Time.deltaTime);
        transform.position = smoothedPosition;

       
    }
}
