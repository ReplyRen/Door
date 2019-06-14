using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalController : controller
{
    private int walk_Hash;

    private void Awake()
    {
        //arriveMaxHeight = false;
        walk_Hash = Animator.StringToHash("Player_walk");
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

        if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).GetHashCode() == walk_Hash && this.GetComponent<AudioSource>().isPlaying == false)
            this.GetComponent<AudioSource>().Play();
        else if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).GetHashCode() != walk_Hash)
            this.GetComponent<AudioSource>().Pause();
    }
  
}
