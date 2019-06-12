using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    private BoxCollider2D playercol;//物体的碰撞器
    private float lenth;//物体的长度
    private float height;//物体的高度
    private int l;//长度上的射线根数
    private int h;//宽度上的射线根数
    public float density = 0.1f;//射线密度
    private LiftManager liftManager;
    private GameObject player;
    Ray2D[] upRay;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playercol = gameObject.GetComponent<BoxCollider2D>();
        liftManager = GameObject.FindWithTag("LiftManager").GetComponent<LiftManager>();
        lenth = playercol.bounds.size.x;
        height = playercol.bounds.size.y;
        l = (int)(lenth / density);
        h = (int)(height / density);
        
    }
    private void FixedUpdate()
    {
        InitRay();
        if(gameObject.tag=="LeftLift")
            LeftUpRayDetection();
    }
    public void InitRay()
    {
        upRay = new Ray2D[l];
        InitRayDir(upRay, Vector2.up);
        float interval = lenth / (upRay.Length - 1);
        upRay[0].origin = transform.position + new Vector3(-lenth / 2, height / 2, 0f) + new Vector3(0f, 0.01f, 0f);
        upRay[upRay.Length - 1].origin = transform.position + new Vector3(lenth / 2, height / 2, 0f) + new Vector3(0f, 0.01f, 0f);
        for (int i = 1; i < upRay.Length - 1; i++)
        {
            upRay[i].origin = upRay[i - 1].origin + new Vector2(interval, 0f);
        }
        DrawRay(upRay, Color.red);
    }
    private void LeftUpRayDetection()
    {
        RaycastHit2D[] upHit = new RaycastHit2D[l];
        int upStatus = 0;
        for (int i = 0; i < upHit.Length; i++)
        {
            upHit[i] = Physics2D.Linecast(upRay[i].origin, upRay[i].origin + new Vector2(0f, 0.1f));
            Debug.Log(upHit[i].collider);
            if (upHit[i].collider == null)
                upStatus++;
            else
            {
                if (upHit[i].collider.tag=="Player")
                    upHit[i].collider.transform.position= new Vector3(upHit[i].collider.transform.position.x, transform.position.y, 0f) + new Vector3(0, 0.7f, 0);
                if(upHit[i].collider.tag == "Store")
                    upHit[i].collider.transform.position = new Vector3(upHit[i].collider.transform.position.x, transform.position.y, 0f) + new Vector3(0, 0.5f, 0);
                if (upHit[i].collider.tag == "Box")
                    upHit[i].collider.transform.position = new Vector3(upHit[i].collider.transform.position.x, transform.position.y, 0f) + new Vector3(0, 0.5f, 0);
            }
        }
        if (upStatus == upHit.Length)
            liftManager.leftWeight = 0;

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
}
