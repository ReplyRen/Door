using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalController : controller
{

    private void Awake()
    {
        //arriveMaxHeight = false;
}
    void FixedUpdate()
    {
        base.FixedUpdate();
        switch (status)
        {
            case 1:
                verticalSpeed = jumpInitSpeed;
                break;
            case 0:
                verticalSpeed = 0f;
                break;
            case 3:
                verticalSpeed -= g;
                break;

        }
        if (verticalSpeed < -fallMaxSpeed)
            verticalSpeed = -fallMaxSpeed;
    }
  
}
