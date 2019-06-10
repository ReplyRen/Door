using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationController : MonoBehaviour
{
    public float rotationSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        float rotationTmp = this.transform.eulerAngles.z;
        this.transform.eulerAngles = new Vector3(0, 0, this.transform.eulerAngles.z - rotationSpeed);
    }
}
