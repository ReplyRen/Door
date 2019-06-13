using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachController : MonoBehaviour
{
    public GameObject player;
    public GameObject[] animations;
    public GameObject[] reminders;
    public Vector3[] triggerPos;

    void Start()
    {
        animations[0].SetActive(true);
        animations[1].SetActive(true);
        animations[2].SetActive(false);
        animations[3].SetActive(false);

        reminders[0].SetActive(true);
        reminders[1].SetActive(false);
        reminders[2].SetActive(false);
    }

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            GameObject.Destroy(animations[0]);
            GameObject.Destroy(animations[1]);
            GameObject.Destroy(reminders[0]);
        }
        if(animations[2])
        {
            if (animations[2].activeSelf == true && player.transform.position.y > 1)
            {
                GameObject.Destroy(animations[2]);
                GameObject.Destroy(reminders[1]);
            }
        }
        if(animations[3])
        {
            if (animations[3].activeSelf == true && Input.GetKeyDown(KeyCode.Space))
            {
                GameObject.Destroy(animations[3]);
                GameObject.Destroy(reminders[2]);
            }
        }

        float distance = (player.transform.position - triggerPos[0]).magnitude;
        if(distance < 0.5 && animations[2])
        {
            animations[2].SetActive(true);
            reminders[1].SetActive(true);
        }
        distance = (player.transform.position - triggerPos[1]).magnitude;
        if(distance < 0.5 && animations[3])
        {
            animations[3].SetActive(true);
            reminders[2].SetActive(true);
        }
    }
}
