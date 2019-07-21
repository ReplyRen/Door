using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    void Start()
    {
        Debug.Log(Vector3.Distance(a.transform.position, b.transform.position));
        Debug.Log(Vector3.Distance(b.transform.position, a.transform.position));
    }
}
