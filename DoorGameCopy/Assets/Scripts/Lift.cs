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
        if (gameObject.tag == "LeftLift")
            LeftUpRayDetection();
        if (gameObject.tag == "RightLift")
            RightUpRayDetection();
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
        List<GameObject> leftList = new List<GameObject>();
        RaycastHit2D[] upHit;
        leftList.Clear();
        for (int i = 0; i < upRay.Length; i++)
        {
            upHit = Physics2D.RaycastAll(upRay[i].origin, upRay[i].direction, 10f);
            if (upHit.Length > 0)
            {
                for (int j = 0; j < upHit.Length; j++)
                {
                    if (!Exist(leftList, upHit[j].collider.gameObject) && upHit[j].collider.tag != "LeftLift")
                        leftList.Add(upHit[j].collider.gameObject);
                }
            }
        }
        if (leftList.Count == 0)
            ;
        else
        {
            for (int i = 0; i < leftList.Count; i++)
            {
                if (transform.position != liftManager.leftUpPos && transform.position != liftManager.leftDownPos)
                {
                    if (leftList[i].transform.parent == null)
                    {
                        leftList[i].transform.parent = gameObject.transform;
                    }
                }
                else
                {
                    leftList[i].transform.parent = null;
                }
            }
        }
        liftManager.leftWeight = Weight(leftList);
    }
    private void RightUpRayDetection()
    {

        List<GameObject> rightList = new List<GameObject>();
        RaycastHit2D[] upHit;
        rightList.Clear();
        for (int i = 0; i < upRay.Length; i++)
        {
            upHit = Physics2D.RaycastAll(upRay[i].origin, upRay[i].direction, 10f);
            if (upHit.Length > 0)
            {
                for (int j = 0; j < upHit.Length; j++)
                {
                    if (!Exist(rightList, upHit[j].collider.gameObject) && upHit[j].collider.tag != "Canvas" && upHit[j].collider.tag != "RightLift")
                        rightList.Add(upHit[j].collider.gameObject);
                }
            }
        }
        if (rightList.Count == 0)
            ;
        else
        {
            for (int i = 0; i < rightList.Count; i++)
            {
                if (transform.position != liftManager.rightUpPos && transform.position != liftManager.rightDownPos)
                {
                    if (rightList[i].transform.parent == null)
                    {
                        rightList[i].transform.parent = gameObject.transform;
                    }
                }
                else
                {
                    rightList[i].transform.parent = null;
                }
            }
        }
        liftManager.rightWeight = Weight(rightList);
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
    private bool Exist(List<GameObject> list, GameObject game)
    {
        if (list.Count == 0)
            return false;
        for (int n = 0; n < list.Count; n++)
        {
            if (game == list[n])
                return true;
        }
        return false;
    }
    private float Weight(List<GameObject> list)
    {
        float num = 0f;
        if (list != null)
        {
            for (int n = 0; n < list.Count; n++)
            {
                if (list[n].tag == "Player")
                {
                    num += list[n].GetComponent<Weight>().weight;
                }

                else if (list[n].tag == "Box")
                {
                    if (list[n].GetComponent<Box>().verticalSpeed == 0)
                        num += list[n].GetComponent<Weight>().weight;
                }
                else if (list[n].tag == "Store")
                {
                    if (list[n].GetComponent<Store>().verticalSpeed == 0)
                        num += list[n].GetComponent<Weight>().weight;
                }


            }
        }
        return num;
    }
}
