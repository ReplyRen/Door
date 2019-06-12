using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class controller : MonoBehaviour
{
    public float speed = 4f;
    public int status = 0;//状态：0为在地面上，1为起跳，2为上方碰到物体，3为悬空
    public float gNormal = 9.8f;//正常的重力值
    public float g;
    public float gSmooth = 2f;//达到最高点时的重力值
    public float jumpInitSpeed = 150f;
    public float fallMaxSpeed = -100f;
    public float smoothCriticalSpeed = 10f;
    private float lerp;//每帧y方向的位移差
    public float verticalSpeed=0f;//y方向的速度
    private float verticalPos;//y方向的位置
    private Vector3 startPos;
    private Vector3 newPos;//与上一个变量均用于计算每帧位移差
    private float angle;//地面倾角
    private BoxCollider2D playercol;//物体的碰撞器
    private float lenth;//物体的长度
    private float height;//物体的高度
    private int l;//长度上的射线根数
    private int h;//宽度上的射线根数
    private int verticalStatus;//水平方向的状态
    private float maxUpPos;
    public float density=0.1f;//射线密度
    private  float timer = 0f;
    [HideInInspector]
    public bool Jump = false;
    Ray2D[] downRay;
    Ray2D[] upRay;
    Ray2D[] rightRay;
    Ray2D[] leftRay;
    private float upMarigin;
    private float downMargin;
    private bool onMashroom = false;
    public float mashroomSpeed = 300f;
    private void Start()
    {
        angle = 0f;
        startPos = transform.position;
        playercol = gameObject.GetComponent<BoxCollider2D>();
        lenth = playercol.bounds.size.x;
        height = playercol.bounds.size.y;
        l = (int)(lenth / density);
        h = (int)(height/ density);
        verticalStatus = 0;
        maxUpPos = 100f;
        timer = 0f;

        upMarigin = 10;
        downMargin = -30;
    }

    public void FixedUpdate()
    {
        InitRay();//初始化射线
        newPos = transform.position;
        float h = Input.GetAxis("Horizontal");//获得水平键入
        if (h < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
        if (h > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);//旋转
        Vector2 movement = new Vector2(h, 0);
        onMashroom = false;
        Move(movement);
        startPos = newPos;

        Animator anim = this.GetComponent<Animator>();
        if (Input.GetAxis("Horizontal") != 0)
            anim.SetBool("IsWalking", true);
        else anim.SetBool("IsWalking", false);

        if (verticalSpeed > upMarigin && anim.GetInteger("State") != 1)
            anim.SetInteger("State", 1);
        else if (verticalSpeed < downMargin && anim.GetInteger("State") != 2)
            anim.SetInteger("State", 2);
        else if (verticalSpeed >= downMargin && verticalSpeed <= upMarigin && anim.GetInteger("State") != 0)
            anim.SetInteger("State", 0);
    }
    public void Move(Vector2 deltaPos)
    {
        DownRayDetection();//下方的射线检测
        VerticalRayDetection(deltaPos);//水平 射线检测
        if (Input.GetKey(KeyCode.Space) && status == 0)//跳跃
        {
            status = 1;
        }
        UpRayDetection();//上方射线检测
        if (verticalSpeed < smoothCriticalSpeed && verticalSpeed > -smoothCriticalSpeed)//接近最高点时平滑的速度
            g = gSmooth;
        else
            g = gNormal;
        lerp = newPos.y - startPos.y;
        verticalPos = verticalSpeed * Time.deltaTime;//通过速度计算下一个竖直方向的位置
        if (maxUpPos < verticalPos)
        {
            verticalPos = maxUpPos;
        }
        if (verticalStatus == 2 && deltaPos.x < 0)
            deltaPos.x = 0;
        if (verticalStatus == 1 && deltaPos.x > 0)
            deltaPos.x = 0;       //水平方向一左一右的射线检测  
        if (status == 0)        //在地面上的移动
        {
            if (lerp >= 0)
                transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(deltaPos.x * Mathf.Cos(Mathf.PI * angle / 180), Mathf.Abs(deltaPos.x) * Mathf.Sin(Mathf.PI * angle / 180), 0f), speed * Time.deltaTime);
            else if (lerp < 0)
                transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(deltaPos.x * Mathf.Cos(Mathf.PI * angle / 180), -Mathf.Abs(deltaPos.x) * Mathf.Sin(Mathf.PI * angle / 180), 0f), speed * Time.deltaTime);
        }

        else if (status == 1||status==4)//起跳的移动
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(deltaPos.x,verticalPos, 0f), 4f * Time.deltaTime);
        }
        else if (status == 3)//悬空的移动
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(deltaPos.x, verticalPos, 0f), 4f * Time.deltaTime);
        else if (status == 2)//上方碰到东西的移动
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(deltaPos.x, -2.0f, 0f), 4f * Time.deltaTime);
            verticalSpeed = 0f;
            status = 3;
        }
    }
    public void InitRay()
    {
        downRay = new Ray2D[l];
        upRay = new Ray2D[l];
        rightRay = new Ray2D[h];
        leftRay = new Ray2D[h];
        InitRayDir(downRay, Vector2.down);
        InitRayDir(upRay, Vector2.up);
        InitRayDir(rightRay, Vector2.right);
        InitRayDir(leftRay, Vector2.left);
        downRay[0].origin = transform.position + new Vector3(-lenth / 2, -height / 2, 0f) + new Vector3(0f, -0.01f, 0f);
        downRay[downRay.Length - 1].origin = transform.position + new Vector3(lenth / 2, -height / 2, 0f) + new Vector3(0f, -0.01f, 0f);
        float interval = lenth / (downRay.Length - 1);
        for(int i=1; i < downRay.Length - 1; i++)
        {
            downRay[i].origin = downRay[i - 1].origin + new Vector2(interval, 0f);
        }
        upRay[0].origin = transform.position + new Vector3(-lenth / 2, height/ 2, 0f) + new Vector3(0f, 0.01f, 0f);
        upRay[upRay.Length - 1].origin = transform.position + new Vector3(lenth / 2, height / 2, 0f) + new Vector3(0f, 0.01f, 0f);
        for (int i = 1; i < upRay.Length - 1; i++)
        {
            upRay[i].origin = upRay[i - 1].origin + new Vector2(interval, 0f);
        }
        leftRay[0].origin = transform.position + new Vector3(-lenth / 2, -height / 2, 0f) + new Vector3(-0.01f, 0f, 0f);
        leftRay[leftRay.Length - 1].origin = transform.position + new Vector3(-lenth / 2,height / 2, 0f) + new Vector3(-0.01f, 0f, 0f);
        interval = height / (leftRay.Length - 1);
        for (int i = 1; i <leftRay.Length - 1; i++)
        {
            leftRay[i].origin = leftRay[i - 1].origin + new Vector2(0f,interval);
        }
        rightRay[0].origin = transform.position + new Vector3(lenth/ 2, -height / 2, 0f) + new Vector3(0.01f, 0f, 0f);
        rightRay[rightRay.Length - 1].origin = transform.position + new Vector3(lenth / 2, height / 2, 0f) + new Vector3(0.01f, 0f, 0f);
        for (int i = 1; i < rightRay.Length - 1; i++)
        {
            rightRay[i].origin = rightRay[i - 1].origin + new Vector2(0f, interval);
        }
        DrawRay(upRay, Color.red);
        DrawRay(downRay, Color.red);
        DrawRay(leftRay, Color.red);
        DrawRay(rightRay, Color.red);

    }
    private void DownRayDetection()
    {
        RaycastHit2D[] downHit = new RaycastHit2D[l];
        int[] downHitStatus = new int[l];
        float[] ang = new float[l];
        int q = 0;
        for (int i = 0; i < downHit.Length; i++)
        {
            downHit[i] = Physics2D.Linecast(downRay[i].origin, downRay[i].origin + new Vector2(0, -0.1f - Mathf.Abs(verticalSpeed / 1000)));
            if (status == 0)
                downHitStatus[i] = 0;
            if (downHit[i].collider == null)
            {
                downHitStatus[i] = 3;
            }
            else if (downHit[i].collider.tag == "Platform" || downHit[i].collider.tag == "floor" || downHit[i].collider.tag == "Water" || downHit[i].collider.tag == "store")
            {
                downHitStatus[i] = 0;
                ang[i] = Vector2.Angle(downHit[i].normal, Vector2.up);
                if (downHit[i].collider.tag == "Water")
                {
                    isDead();
                }
            }
            else if (downHit[i].collider.tag == "Canvas")
            {
                downHitStatus[i] = 3;
            }
            else if (downHit[i].collider.tag == "Mushroom")
                onMashroom = true;
            q += downHitStatus[i];
            if (q == downHitStatus.Length * 3)
            {
                status = 3;
            }
            if (downHitStatus[i] == 0)
                status = 0;
            if (onMashroom)
                status = 4;
        }
        angle = PX(ang);//挑出最大的
    }
    private void UpRayDetection()
    {
        RaycastHit2D[] upHit = new RaycastHit2D[l];
        int upStatus = 0;
        for (int i = 0; i < upHit.Length; i++)
        {
            upHit[i] = Physics2D.Linecast(upRay[i].origin, upRay[i].origin + new Vector2(0f, 0.1f + Mathf.Abs( verticalSpeed / 1000)));
            if (upHit[i].collider == null)
            {
                upStatus += 1;
                continue;
            }
            else if (upHit[i].collider.tag == "Platform"||upHit[i].collider.tag == "floor")
            {
                status = 2;
            }
        }
        if (upStatus == l)
            maxUpPos = 100f;
    }
    private void VerticalRayDetection(Vector2 dir)
    {
        RaycastHit2D[] leftHit = new RaycastHit2D[h];
        RaycastHit2D[] rightHit = new RaycastHit2D[h];
        int[] leftHitStatus = new int[h];
        int[] rightHitStatus = new int[h];
        int q = 0;
        if (dir.x < 0)
        {
            for (int i = 0; i < leftHit.Length; i++)
            {
                leftHit[i] = Physics2D.Linecast(leftRay[i].origin, leftRay[i].origin + new Vector2(-0.1f, 0f));
                if (leftHit[i].collider == null)
                {
                    leftHitStatus[i] = 0;
                }
                else if (leftHit[i].collider.tag == "floor"|| leftHit[i].collider.tag =="Mushroom"||
                     leftHit[i].collider.tag == "Store")
                {
                    leftHitStatus[i] = 1;
                }
                else if (leftHit[i].collider.tag == "Box")
                {
                    leftHitStatus[i] = 1;
                    if(status!=3)
                        leftHit[i].collider.gameObject.GetComponent<Force>().horizontalSpeed = -45;

                }
                q += leftHitStatus[i];
            }
            if (q == 0)
                verticalStatus = 0;
            else
                verticalStatus = 2;
        }
        else if (dir.x > 0)
        {
            int p = 0;
            for (int i = 0; i < rightHit.Length; i++)
            {
                rightHit[i] = Physics2D.Linecast(rightRay[i].origin, rightRay[i].origin + new Vector2(0.1f, 0f));
                if (rightHit[i].collider == null)
                {
                    rightHitStatus[i] = 0;
                }
                else if (rightHit[i].collider.tag == "floor" || rightHit[i].collider.tag == "Mushroom"|| rightHit[i].collider.tag=="Store")
                {
                    rightHitStatus[i] = 1;
                }
                else if (rightHit[i].collider.tag == "Box")
                {
                    rightHitStatus[i] = 1;
                    if(status!=3)
                        rightHit[i].collider.gameObject.GetComponent<Force>().horizontalSpeed = 45;

                }

                p += rightHitStatus[i];
            }
            if (p == 0)
                verticalStatus = 0;
            else
                verticalStatus = 1;
        }
    }
    private void InitRayDir(Ray2D[] ray,Vector2 dir)
    {
        for(int i=0; i < ray.Length; i++)
        {
            ray[i].direction = dir;
        }
    }
    private void DrawRay(Ray2D[] ray,Color color)
    {
        for(int i=0; i < ray.Length; i++)
        {
            Debug.DrawRay(ray[i].origin, ray[i].direction, color);
        }
    }
    private float PX(float[] arry) 
    {
        for(int i=0; i < arry.Length-1; i++)
        {
            if (arry[i] > arry[i + 1])
                arry[i + 1] = arry[i];
        }
        return arry[arry.Length-1];
    }

    public void isDead()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
    }
}
