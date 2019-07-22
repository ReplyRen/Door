using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMove : MonoBehaviour
{
    private Transform spiderTrans;
    public float topPos;
    public float bottomPos;
    public Vector3 speed;
    private float timer;
    private bool forwardDown;

    private void Start()
    {
        spiderTrans = this.transform;
        forwardDown = true;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 10)
        {
            if (spiderTrans.position.y >= bottomPos && forwardDown)
                transform.position -= speed;
            else if (spiderTrans.position.y < bottomPos && forwardDown)
            {
                timer = 0;
                forwardDown = !forwardDown;
            }
            else if (spiderTrans.position.y <= topPos && !forwardDown)
                transform.position += speed;
            else if (spiderTrans.position.y > topPos && !forwardDown)
            {
                timer = 0;
                forwardDown = !forwardDown;
            }
        }
    }
}
