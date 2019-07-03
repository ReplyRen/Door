using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    static private bool first = true;
    private int lastNum;
    public Text smallTalk;
    public string[] content;
    public Vector3 triggerPoint;
    public GameObject player;
    private float timeCounter;
    private float resttime;
    private bool talkStart;
    private bool talkEnd;
    private int stringnum;

    private void Start()
    {
        int index = Random.Range(0, 3);
        if (index == 0)
            smallTalk.text = "咳咳。。。";
        else if (index == 1)
            smallTalk.text = "咳咳咳。。。";
        else
            smallTalk.text = "咳咳咳咳。。。";
        if (first) lastNum = 5;
        else lastNum = 1;
    }

    private void Update()
    {
        float timeTmp = timeCounter;
        timeCounter = timeTmp + Time.deltaTime;

        float distance = (player.transform.position - triggerPoint).magnitude;
        if (distance < 0.5)
        {
            if (talkStart == false)
                timeCounter = 0;
            talkStart = true;
        }
        if (talkStart == false || talkEnd == true)
        {
            if (timeCounter > 4)
            {
                smallTalk.text = "";
                if (timeCounter >= 7)
                {
                    int index = Random.Range(0, 3);
                    if (index == 0)
                        smallTalk.text = "咳咳。。。";
                    else if (index == 1)
                        smallTalk.text = "咳咳咳。。。";
                    else
                        smallTalk.text = "咳咳咳咳。。。";

                    timeCounter = 0;
                }
            }
        }
        else if (talkStart && talkEnd != true)
        {
            if (timeCounter > 3)
            {
                smallTalk.text = "";
                if (timeCounter >= 5)
                {
                    smallTalk.text = content[stringnum];
                    stringnum += 1;
                    timeCounter = 0;
                    if (stringnum == lastNum)
                    {
                        talkEnd = true;
                        timeCounter = 0;
                        first = false;
                    }
                }
            }
        }
    }
}
