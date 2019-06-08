using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attract : MonoBehaviour
{
    private float smooth = 1f;
    private void FixedUpdate()
    {
        GetMessage(new Vector3(0, 0, 0));
    }
    public void GetMessage(Vector3 targetPos)
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, smooth * Time.deltaTime);
    }
}
