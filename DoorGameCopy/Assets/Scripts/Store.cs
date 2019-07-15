using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public int status = 0;
    Ray2D[] downRay;
    Ray2D[] rightRay;
    Ray2D[] leftRay;
    private BoxCollider2D playercol;//物体的碰撞器
    private float lenth;//物体的长度
    private float height;//物体的高度
    private int l;//长度上的射线根数
    private int h;//宽度上的射线根数
    public float density = 0.1f;//射线密度
    public float verticalSpeed = 0f;//y方向的速度
    private float verticalPos;//y方向的位置
    public float gNormal = 9.8f;//正常的重力值
    public float g;
    public float gSmooth = 2f;//达到最高点时的重力值
    public float smoothCriticalSpeed = 10f;
    public float fallMaxSpeed = 150f;

    private void Start()
    {
        playercol = gameObject.GetComponent<BoxCollider2D>();
        lenth = playercol.bounds.size.x;
        height = playercol.bounds.size.y;
        
        l = (int)(lenth / density);
        h = (int)(height / density);
    }
    private void FixedUpdate()
    {
        InitRay();//初始化射线 
        DownRayDetection();
        VerticalRayDetection();
        if (verticalSpeed < smoothCriticalSpeed && verticalSpeed > -smoothCriticalSpeed)//接近最高点时平滑的速度
            g = gSmooth;
        else
            g = gNormal;
        if (status == 3)
            verticalSpeed -= g;
        verticalPos = verticalSpeed * Time.deltaTime;//通过速度计算下一个竖直方向的位置
        if (verticalSpeed < -fallMaxSpeed)
            verticalSpeed = -fallMaxSpeed;
        if (status == 3)//悬空的移动
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, verticalPos, 0f), 4f * Time.deltaTime);
        if (status == 0)
            verticalSpeed = 0f;
    }
    public void InitRay()
    {
        downRay = new Ray2D[l];
        rightRay = new Ray2D[h];
        leftRay = new Ray2D[h];
        InitRayDir(downRay, Vector2.down);
        InitRayDir(rightRay, Vector2.right);
        InitRayDir(leftRay, Vector2.left);
        downRay[0].origin = transform.position + new Vector3(-lenth / 2, -height / 2+playercol.offset.y, 0f) + new Vector3(0f, -0.01f, 0f);
        downRay[downRay.Length - 1].origin = transform.position + new Vector3(lenth / 2, -height / 2+ playercol.offset.y, 0f) + new Vector3(0f, -0.01f, 0f);
        float interval = lenth / (downRay.Length - 1);
        for (int i = 1; i < downRay.Length - 1; i++)
        {
            downRay[i].origin = downRay[i - 1].origin + new Vector2(interval, 0f);
        }
        leftRay[0].origin = transform.position + new Vector3(-lenth / 2, -height / 2, 0f) + new Vector3(-0.01f, 0f, 0f);
        leftRay[leftRay.Length - 1].origin = transform.position + new Vector3(-lenth / 2, height / 2, 0f) + new Vector3(-0.01f, 0f, 0f);
        interval = height / (leftRay.Length - 1);
        for (int i = 1; i < leftRay.Length - 1; i++)
        {
            leftRay[i].origin = leftRay[i - 1].origin + new Vector2(0f, interval);
        }
        rightRay[0].origin = transform.position + new Vector3(lenth / 2, -height / 2, 0f) + new Vector3(0.01f, 0f, 0f);
        rightRay[rightRay.Length - 1].origin = transform.position + new Vector3(lenth / 2, height / 2, 0f) + new Vector3(0.01f, 0f, 0f);
        for (int i = 1; i < rightRay.Length - 1; i++)
        {
            rightRay[i].origin = rightRay[i - 1].origin + new Vector2(0f, interval);
        }
        DrawRay(downRay, Color.red);
        DrawRay(leftRay, Color.red);
        DrawRay(rightRay, Color.red);

    }
    private void InitRayDir(Ray2D[] ray, Vector2 dir)
    {
        for (int i = 0; i < ray.Length; i++)
        {
            ray[i].direction = dir;
        }
    }
    private void DrawRay(Ray2D[] ray, Color color)
    {
        for (int i = 0; i < ray.Length; i++)
        {
            Debug.DrawRay(ray[i].origin, ray[i].direction, color);
        }
    }
    private void DownRayDetection()
    {
        RaycastHit2D[] downHit = new RaycastHit2D[l];
        int[] downHitStatus = new int[l];
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
            else if (downHit[i].collider.tag == "Box" || downHit[i].collider.tag == "floor" || downHit[i].collider.tag == "Pig"||downHit[i].collider.tag == "Store"||
                downHit[i].collider.tag == "LeftLift" || downHit[i].collider.tag == "RightLift")
            {
                downHitStatus[i] = 0;
            }
            else if (downHit[i].collider.tag == "Canvas"|| downHit[i].collider.tag =="Water" )
            {
                downHitStatus[i] = 3;
            }
            q += downHitStatus[i];
            if (q == downHitStatus.Length * 3)
            {
                status = 3;
            }
            if (downHitStatus[i] == 0)
                status = 0;
        }
    }
    private void VerticalRayDetection()
    {
        RaycastHit2D[] leftHit = new RaycastHit2D[h];
        RaycastHit2D[] rightHit = new RaycastHit2D[h];
        for (int i = 0; i < leftHit.Length; i++)
        {
            leftHit[i] = Physics2D.Linecast(leftRay[i].origin, leftRay[i].origin + new Vector2(-0.1f, 0f));
            rightHit[i] = Physics2D.Linecast(rightRay[i].origin, rightRay[i].origin + new Vector2(0.1f, 0f));
            if (leftHit[i].collider != null)
            {
                if (leftHit[i].collider.tag == "floor")
                {
                    gameObject.GetComponent<Force>().horizontalSpeed = 0;
                }
            }
            if (rightHit[i].collider != null)
            {
                if (rightHit[i].collider.tag == "floor")
                {
                    gameObject.GetComponent<Force>().horizontalSpeed = 0;
                }
            }
        }
    }
}
