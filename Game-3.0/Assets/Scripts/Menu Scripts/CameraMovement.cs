using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float deltaX;
    public float limit;
    private bool goLeft = true;

    private void Update()
    {
        if (transform.position.x <= -limit)
            goLeft = false;


        else if (transform.position.x >= limit)
            goLeft = true;


        if (goLeft)
            transform.position = new Vector3(transform.position.x - deltaX, transform.position.y, transform.position.z);

        if (!goLeft)
            transform.position = new Vector3(transform.position.x + deltaX, transform.position.y, transform.position.z);
    }
}