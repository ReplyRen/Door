using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attract : MonoBehaviour
{
    public float speed = 5f;
    private float timer = 0f;
    private float attractTime = 5f;
    private GameObject player;
    private GameObject bHPen;
    private bool selfAttract = false;
    private bool haveProtal = false;
    private float multiple = 5.5f;
    private void Start()
    {
        timer = 0f;
        player = GameObject.FindWithTag("Player");
        bHPen = GameObject.FindWithTag("bHPen");
        haveProtal = false;
    }
    private void FixedUpdate()
    {
        if (bHPen != null)
        {
            if (bHPen.GetComponent<BlackHoldPen>().isAttracting == true)
            {
                selfAttract = true;
            }
            else if (bHPen.GetComponent<BlackHoldPen>().isAttracting == false)
            {
                selfAttract = false;
                haveProtal = false;
            }

            if (selfAttract && !haveProtal)
            {
                Vector3 targetPos = new Vector3();
                Vector3 protalPos = new Vector3();
                targetPos = bHPen.GetComponent<BlackHoldPen>().bHPosList[0];
                protalPos = bHPen.GetComponent<BlackHoldPen>().bHPosList[1];
                timer += Time.deltaTime;
                Debug.Log(IsDisClose(targetPos));
                if (IsDisClose(targetPos))
                {
                    if (gameObject.tag == "Player")
                    {
                        player.GetComponent<GravitationalController>().enabled = false;
                        Move(targetPos, targetPos, protalPos);
                        player.GetComponent<GravitationalController>().enabled = true;
                    }
                    else if (gameObject.tag == "Store")
                    {
                        gameObject.GetComponent<Store>().enabled = false;
                        Move(targetPos, targetPos, protalPos);
                        gameObject.GetComponent<Store>().enabled = true;
                    }
                    else if (gameObject.tag == "Box")
                    {
                        gameObject.GetComponent<Box>().enabled = false;
                        Move(targetPos, targetPos, protalPos);
                        gameObject.GetComponent<Box>().enabled = true;
                    }
                    else if (gameObject.tag == "Pig")
                    {
                        gameObject.GetComponent<Pig>().enabled = false;
                        Move(targetPos, targetPos, protalPos);
                        gameObject.GetComponent<Pig>().enabled = true;
                    }
                    else if (gameObject.tag == "Bullet")
                    {
                        gameObject.GetComponent<Bullet>().enabled = false;
                        Move(targetPos, targetPos, protalPos);
                        gameObject.GetComponent<Bullet>().enabled = true;
                    }

                }
            }
        }

    }
    private void Move(Vector3 targetPos,Vector3 from,Vector3 to)
    {
        Vector3 dir = new Vector3();
        dir = (targetPos - transform.position) / (targetPos - transform.position).magnitude;
        if(transform.position!=to&&haveProtal==false)
            transform.Translate(dir * speed * Time.deltaTime,Space.World);
        if ((transform.position - from).magnitude < 0.05)
        {
            gameObject.transform.position = to;
            haveProtal = true;
            if (this.tag == "Pig")
                this.GetComponent<Pig>().isDead();
        }


    }
    private bool IsDisClose(Vector3 targetPos)
    {
        if ((transform.position - targetPos).magnitude < multiple * bHPen.GetComponent<BlackHoldPen>().R)
            return true;
        else
            return false;
    }
}
