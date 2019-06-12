using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftManager : MonoBehaviour
{
    public GameObject leftLift;
    public  GameObject rightLift;
    private Vector3 leftUpPos = new Vector3(-0.58f, 0.7064452f, 0f);
    private  Vector3 leftDownPos = new Vector3(-0.58f, -2.613555f, 0f);
    private  Vector3 rightUpPos = new Vector3(2.06f, 0.7064452f, 0f);
    private  Vector3 rightDownPos = new Vector3(2.06f, -2.613555f, 0f);
    public float leftWeight = 0f;
    public  float rightWeight = 0f;
    public float liftSpeed = 1f;
    private void Start()
    {
    }
    private void FixedUpdate()
    {
        if (rightWeight > leftWeight)
        {
            rightLift.transform.position = Vector3.Lerp(rightLift.transform.position, rightDownPos, liftSpeed * Time.deltaTime);
            leftLift.transform.position = Vector3.Lerp(leftLift.transform.position, leftUpPos, liftSpeed * Time.deltaTime);
        }
        else
        {
            rightLift.transform.position = Vector3.Lerp(rightLift.transform.position, rightUpPos, liftSpeed * Time.deltaTime);
            leftLift.transform.position = Vector3.Lerp(leftLift.transform.position, leftDownPos, liftSpeed * Time.deltaTime);
        }

    }
   
}
