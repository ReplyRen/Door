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
            case 4:
                verticalSpeed = mashroomSpeed;
                break;

        }
        if (verticalSpeed < -fallMaxSpeed)
            verticalSpeed = -fallMaxSpeed;
    }

    public void Playjumpsound()
    {
        AudioClip jumpSound = GlobalController._instance.jumpSound;
        AudioSource.PlayClipAtPoint(jumpSound, new Vector3(0, 0, 0));
    }
}
