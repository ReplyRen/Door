﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftManager : MonoBehaviour
{
    public GameObject leftLift;
    public  GameObject rightLift;
    public Vector3 leftUpPos = new Vector3(-0.58f, 0.7064452f, 0f);
    public Vector3 leftDownPos = new Vector3(-0.58f, -2.613555f, 0f);
    public Vector3 rightUpPos = new Vector3(2.06f, 0.7064452f, 0f);
    public Vector3 rightDownPos = new Vector3(2.06f, -2.613555f, 0f);
    public float leftWeight = 0f;
    public float rightWeight = 0f;
    public float liftSpeed = 2f;
    public GameObject[] stone;
    public GameObject[] box;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void FixedUpdate()
    {

        AudioSource audioSource = this.GetComponent<AudioSource>();
        if (rightWeight > leftWeight)
        {
            leftLift.transform.position = Vector3.MoveTowards(leftLift.transform.position, leftUpPos, liftSpeed * Time.deltaTime);
            rightLift.transform.position = Vector3.MoveTowards(rightLift.transform.position, rightDownPos, liftSpeed * Time.deltaTime);

            float distance = (leftLift.transform.position - leftUpPos).magnitude;
            if (audioSource.isPlaying == false)
                audioSource.Play();
            if (distance < 0.2)
                audioSource.Pause();
        }
        else if (rightWeight < leftWeight)
        {
            rightLift.transform.position = Vector3.MoveTowards(rightLift.transform.position, rightUpPos, liftSpeed * Time.deltaTime);
            leftLift.transform.position = Vector3.MoveTowards(leftLift.transform.position, leftDownPos, liftSpeed * Time.deltaTime);

            float distance = (leftLift.transform.position - leftDownPos).magnitude;
            if (audioSource.isPlaying == false)
                audioSource.Play();
            if (distance < 0.2)
                audioSource.Pause();
        }
        else
        {
            audioSource.Pause();
        }
    }
   
}
