using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
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
    private Vector3 nextPos = new Vector3(-1f, 0, 0);
    public float verticalForce = 100f;
    public float horizontalForce = 100f;

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
        verticalPos = verticalSpeed * Time.deltaTime;//通过速度计算下一个竖直方向的位置
        VerticalRayDetection();
    }
    public void InitRay()
    {
        leftRay = new Ray2D[h];
        rightRay = new Ray2D[h];
        InitRayDir(rightRay, Vector2.right);
        InitRayDir(leftRay, Vector2.left);
        leftRay[0].origin = transform.position + new Vector3(-lenth / 2, -height / 2, 0f) + new Vector3(-0.01f, 0f, 0f);
        leftRay[leftRay.Length - 1].origin = transform.position + new Vector3(-lenth / 2, height / 2, 0f) + new Vector3(-0.01f, 0f, 0f);
        float interval = height / (leftRay.Length - 1);
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
    private void VerticalRayDetection()
    {
        RaycastHit2D[] leftHit = new RaycastHit2D[h];
        RaycastHit2D[] rightHit = new RaycastHit2D[h];
        for (int i = 0; i < leftHit.Length; i++)
        {
            leftHit[i] = Physics2D.Linecast(leftRay[i].origin, leftRay[i].origin + new Vector2(-0.1f, 0f));
            if (leftHit[i].collider != null)
            {
                if (leftHit[i].collider.tag == "Player")
                {
                    leftHit[i].collider.gameObject.GetComponent<Force>().horizontalSpeed = -horizontalForce;
                    leftHit[i].collider.gameObject.transform.position += new Vector3(0, 0.1f, 0);
                    leftHit[i].collider.gameObject.GetComponent<GravitationalController>().verticalSpeed = verticalForce;
                    leftHit[i].collider.gameObject.GetComponent<GravitationalController>().isDead();
                }
            }
        }
        for (int i = 0; i < rightHit.Length; i++)
        {
            rightHit[i] = Physics2D.Linecast(rightRay[i].origin, rightRay[i].origin + new Vector2(-0.1f, 0f));
            if (rightHit[i].collider != null)
            {

                if (rightHit[i].collider.tag == "Player")
                {
                    rightHit[i].collider.gameObject.GetComponent<Force>().horizontalSpeed = horizontalForce;
                    rightHit[i].collider.gameObject.transform.position += new Vector3(0, 0.1f, 0);
                    rightHit[i].collider.gameObject.GetComponent<GravitationalController>().verticalSpeed = verticalForce;
                    rightHit[i].collider.gameObject.GetComponent<GravitationalController>().isDead();
                }
            }
        }
    }
}

