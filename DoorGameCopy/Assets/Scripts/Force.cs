using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour
{
    public float horizontalSpeed = 0f;
    private float nextx;
    public float reduce = 2f;
    private void Start()
    {
        
    }
    private void FixedUpdate()
    {
        nextx = horizontalSpeed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, transform.position+new Vector3(nextx, 0, 0), 4 * Time.deltaTime);
        if (horizontalSpeed > 0)
            horizontalSpeed -= reduce;
        if (horizontalSpeed < 0)
            horizontalSpeed += reduce;

    }
}
