﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtalPen : MonoBehaviour
{
    private GameObject clone;
    private LineRenderer line;
    public bool isStatic = true;
    public GameObject followParent;
    private int i;
    public GameObject obs;
    public float lineWidth = 0.05f;
    public float precision = 0.05f;
    private List<Vector3> posList = new List<Vector3>();
    private List<Vector3> portalList = new List<Vector3>();
    private bool isPortalOpen = false;
    public float minDoorHeight = 0.1f;
    public float minDoorWidth = 0.1f;
    private float characterHeight;
    private float characterWidth;
    private float closeCanvasDis = 1f;
    private GameObject player;
    public KeyCode protalKey = KeyCode.Q;
    private bool open = false;

    private enum vec3 { top, bottom, left, right, center }

    private Vector3 vec3plus (Vector3 A, Vector3 B)
    {
        return new Vector3(A.x + B.x, A.y + B.y, A.z + B.z);
    }
    private Vector3 vec3plus (Vector3 A, float B)
    {
        return new Vector3(A.x + B, A.y + B, A.z + B);
    }

    private float isCloseCanvas(Vector3[] linePoints, float dis)
    {
        linePoints = new Vector3[line.positionCount];
        if (portalList.Count < 1) return -1;
        line.GetPositions(linePoints);
        Vector3 centerPos = new Vector3(0,0,0);
        for(int i = 0; i < line.positionCount; i++)
        {
            centerPos = vec3plus(centerPos,linePoints[i]);
        }
        centerPos /= line.positionCount;
        dis = (centerPos - followParent.transform.position).sqrMagnitude;
        if (dis < closeCanvasDis) return dis;
        return -1f;
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        characterWidth = player.GetComponent<BoxCollider2D>().size.x;
        characterHeight = player.GetComponent<BoxCollider2D>().size.y;
        portalList.Clear();
    }
    void FixedUpdate()
    {
        if (!isStatic && posList.Count > 0)
        {
            Vector3[] linePoints = new Vector3[line.positionCount];
            float dis = 0;
            //Debug.Log(linePoints[0]);
            if (isCloseCanvas(linePoints, dis) > 0)
            {
                Debug.Log(linePoints[0]);
                for (int i = 0; i < line.positionCount; i++)
                {
                    vec3plus(linePoints[i], dis);
                }
                line.SetPositions(linePoints);
            }
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (portalList.Count >= 2)
            {
                foreach (GameObject x in GameObject.FindGameObjectsWithTag("clonedLine"))
                    Destroy(x);
                portalList.Clear();
            }
            clone = (GameObject)Instantiate(obs, obs.transform.position, transform.rotation);//克隆一个带有LineRender的物体   
            line = clone.GetComponent<LineRenderer>();//获得该物体上的LineRender组件
            line.tag = "clonedLine"; 
            line.SetColors(Color.red, Color.red);//设置颜色  
            line.SetWidth(lineWidth, lineWidth);//设置宽度
            i = 0;
            posList.Clear();
            isPortalOpen = false;
            open = false;
        }
        if (Input.GetMouseButton(0)&&!MousePositionDetection())
        {
            if (clone != null)
                Destroy(clone);
        }
        if (Input.GetMouseButton(0) && MousePositionDetection())
        {
            if (clone != null)
            {
                i++;
                Vector3 pos = new Vector3();
                line.positionCount = i;//设置顶点数  
                pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15));
                line.SetPosition(i - 1, pos);//设置顶点位置 
                if (posList.Count > 10)
                {
                    for (int n = 0; n < posList.Count - 10; n++)
                    {
                        if (IsClose(pos, posList[n]))
                        {
                            isPortalOpen = true;
                        }
                    }
                }
                if (posList.Count == 0)
                    posList.Add(pos);
                else if (pos != posList[posList.Count - 1])
                {
                    posList.Add(pos);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && MousePositionDetection())
        {
            if (clone != null)
            {
                List<Vector3> changeList = new List<Vector3>();
                changeList = posList;
                Vector3 topPoint = new Vector3();
                Vector3 bottomPoint = new Vector3();
                Vector3 leftPoint = new Vector3();
                Vector3 rightPoint = new Vector3();
                Vector3 centerPoint = new Vector3();
                topPoint = posList[posList.Count - 1];
                bottomPoint = GetPoint(changeList, vec3.bottom);
                rightPoint = GetPoint(changeList, vec3.right);
                leftPoint = GetPoint(changeList, vec3.left);
                centerPoint = GetPoint(changeList, vec3.center);

                if (isPortalOpen)
                {
                    if ((topPoint.y - bottomPoint.y) < minDoorHeight || (rightPoint.x - leftPoint.x) < minDoorWidth)
                    {
                        Debug.Log("太小了");
                        Destroy(clone);

                    }
                    else
                    {
                        Debug.Log("传送门" + centerPoint);
                        portalList.Add(centerPoint);
                        open = true;
                    }
                }
                else
                {
                    Destroy(clone);
                }
            }
        }
        else if(!open) Destroy(clone);
        if (portalList.Count == 2)
        {
            Portal(portalList[0], portalList[1]);
        }

    }
    private bool IsClose(Vector3 v1, Vector3 v2)
    {
        if ((v1 - v2).sqrMagnitude < precision)
            return true;
        else
            return false;
    }
    private Vector3 GetPoint(List<Vector3> list, vec3 vec)
    {

        if (vec.ToString() == "top")
        {
            for (int m = 0; m < list.Count - 1; m++)
            {
                if (list[m].y > list[m + 1].y)
                {
                    Vector3 q = new Vector3();
                    q = list[m];
                    list[m] = list[m + 1];
                    list[m + 1] = q;
                }
            }
        }
        if (vec.ToString() == "bottom")
        {
            for (int m = 0; m < list.Count - 1; m++)
            {
                if (list[m].y < list[m + 1].y)
                {
                    Vector3 q = new Vector3();
                    q = list[m];
                    list[m] = list[m + 1];
                    list[m + 1] = q;
                }
            }
        }
        if (vec.ToString() == "left")
        {
            for (int m = 0; m < list.Count - 1; m++)
            {
                if (list[m].x < list[m + 1].x)
                {
                    Vector3 q = new Vector3();
                    q = list[m];
                    list[m] = list[m + 1];
                    list[m + 1] = q;
                }
            }
        }
        if (vec.ToString() == "right")
        {
            for (int m = 0; m < list.Count - 1; m++)
            {
                if (list[m].x > list[m + 1].x)
                {
                    Vector3 q = new Vector3();
                    q = list[m];
                    list[m] = list[m + 1];
                    list[m + 1] = q;
                }
            }
        }
        if (vec.ToString() == "center")
        {
            Vector3 t = new Vector3();
            Vector3 b = new Vector3();
            Vector3 l = new Vector3();
            Vector3 r = new Vector3();
            t = GetPoint(list, vec3.top);
            b = GetPoint(list, vec3.bottom);
            l = GetPoint(list, vec3.left);
            r = GetPoint(list, vec3.right);
            float x = (l.x + r.x) / 2;
            float y = (t.y + b.y) / 2;

            return new Vector3(x, y, 0f);
        }
        return list[list.Count - 1];

    }

    private void Portal(Vector3 from, Vector3 to)
    {
        if (((player.transform.position - from).sqrMagnitude < 0.5f) && Input.GetKeyDown(protalKey))
        {
            player.transform.position = to;
        }
        else if (((player.transform.position - to).sqrMagnitude < 0.5f)&&Input.GetKeyDown(protalKey))
        {
            player.transform.position = from;
        }
    }
    private bool MousePositionDetection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit)
        {
            if (hit.collider.tag == "Canvas")
                return true;
            else
                return false;
        }
        else
            return false;
    }
}
