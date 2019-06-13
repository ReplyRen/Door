using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    Ray2D[] rightRay;
    private BoxCollider2D playercol;//物体的碰撞器
    private float lenth;//物体的长度
    private float height;//物体的高度
    private int l;//长度上的射线根数
    private int h;//宽度上的射线根数
    public float density = 0.1f;//射线密度
    public bool startRotate = false;
    public GameObject point;
    private float speed = 1f;
    private void Start()
    {
        //playercol = gameObject.GetComponent<BoxCollider2D>();
        //lenth = playercol.bounds.size.x;
        //height = playercol.bounds.size.y;
        //l = (int)(lenth / density);
        //h = (int)(height / density);
    }
    private void FixedUpdate()
    {
        //InitRay();
        //VerticalRayDetection();

        if (startRotate)
        {
            speed += Time.deltaTime*6;
            transform.RotateAround(point.transform.position, Vector3.forward, -30*speed * Time.deltaTime);
        }

    }
    //public void InitRay()
    //{
    //    rightRay = new Ray2D[h];
    //    InitRayDir(rightRay, Vector2.right);
    //    float interval = height / (rightRay.Length - 1);
    //    rightRay[0].origin = transform.position + new Vector3(lenth / 2, -height *2/ 5, 0f) + new Vector3(0.01f, 0f, 0f);
    //    rightRay[rightRay.Length - 1].origin = transform.position + new Vector3(lenth / 2, height / 2, 0f) + new Vector3(0.01f, 0f, 0f);
    //    for (int i = 1; i < rightRay.Length - 1; i++)
    //    {
    //        rightRay[i].origin = rightRay[i - 1].origin + new Vector2(0f, interval);
    //    }
    //    DrawRay(rightRay, Color.red);

    //}
    //private void InitRayDir(Ray2D[] ray, Vector2 dir)
    //{
    //    for (int i = 0; i < ray.Length; i++)
    //    {
    //        ray[i].direction = dir;
    //    }
    //}
    //private void DrawRay(Ray2D[] ray, Color color)
    //{
    //    for (int i = 0; i < ray.Length; i++)
    //    {
    //        Debug.DrawRay(ray[i].origin, ray[i].direction, color);
    //    }
    //}
    //private void VerticalRayDetection()
    //{
    //    RaycastHit2D[] rightHit = new RaycastHit2D[h];
    //    for (int i = 0; i < rightHit.Length; i++)
    //    {
    //        rightHit[i] = Physics2D.Linecast(rightRay[i].origin, rightRay[i].origin + new Vector2(0.1f, 0f));
    //        if (rightHit[i].collider != null)
    //        {
    //            if (rightHit[i].collider.tag == "floor"&&rightHit[i].collider.gameObject!=gameObject)
    //            {
    //                startRotate = false;
    //            }
    //            Debug.Log(i);
    //            Debug.Log(rightHit[i].collider);
    //        }
    //    }
        
    //}
}
