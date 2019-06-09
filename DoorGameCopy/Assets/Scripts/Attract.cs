using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attract : MonoBehaviour
{
    private float speed = 1f;
    private float timer = 0f;
    private float attractTime = 5f;
    private GameObject player;
    private GameObject bHPen;
    private bool selfAttract = false;
    private bool haveAttract = false;
    private void Start()
    {
        timer = 0f;
        player = GameObject.FindWithTag("Player");
        bHPen = GameObject.FindWithTag("bHPen");
    }
    private void FixedUpdate()
    {
        if (bHPen.GetComponent<BlackHoldPen>().isAttracting == true)
        {
            selfAttract = true;
        }
        else if(haveAttract==true|| bHPen.GetComponent<BlackHoldPen>().isAttracting == false)
            selfAttract = false;
        if (selfAttract)
        {
            Vector3 targetPos = new Vector3();
            Vector3 protalPos = new Vector3();
            targetPos = bHPen.GetComponent<BlackHoldPen>().bHPosList[0];
            protalPos = bHPen.GetComponent<BlackHoldPen>().bHPosList[1];
            timer += Time.deltaTime;
            if (gameObject.tag == "Player")
            {
                player.GetComponent<GravitationalController>().enabled = false;
                Move(targetPos, targetPos, protalPos);
                player.GetComponent<GravitationalController>().enabled = true;
            }
            else
                Move(targetPos, targetPos, protalPos);

        }

    }
    private void Move(Vector3 targetPos,Vector3 from,Vector3 to)
    {
        Vector3 dir = new Vector3();
        dir = (targetPos - transform.position) / (targetPos - transform.position).magnitude;
        if(transform.position!=to)
            transform.Translate(dir * speed * Time.deltaTime);
        if ((transform.position-from).magnitude<0.05)
            player.transform.position = to;
        
    }
    private void Portal(Vector3 from, Vector3 to)
    {
        if (((player.transform.position - from).sqrMagnitude < 0.1f))
        {
            player.transform.position = to;
        }
    }
}
