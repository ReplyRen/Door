using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcealedPen : MonoBehaviour
{
    private GameObject clone;
    private LineRenderer line;
    private int i;
    public GameObject obs;
    private List<Vector3> posList = new List<Vector3>();
    private bool isConcealedOpen = false;
    public float lineWidth = 0.05f;
    private float DoorHeight;
    private float DoorWidth;
    [HideInInspector]
    public bool isAttracting = false;
    private float timer = 0f;
    public float attractTime = 3f;
    private float angle = 0f;
    private GameObject[] stores;
    private GameObject[] boxes;
    private GameObject[] pigs;
    private bool open = false;
    private GameObject targetObject;
    private Vector3 targetPos;
    public float concealedTime = 3f;
    private bool moved = false;
    private enum vec3 { top, bottom, left, right, center }
    private void Start()
    {
        isAttracting = false;
        angle = 0f;
        stores = GameObject.FindGameObjectsWithTag("Store");
        boxes = GameObject.FindGameObjectsWithTag("Box");
        pigs = GameObject.FindGameObjectsWithTag("Pig");

    }
    private void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {

            clone = (GameObject)Instantiate(obs, obs.transform.position, transform.rotation);//克隆一个带有LineRender的物体   
            line = clone.GetComponent<LineRenderer>();//获得该物体上的LineRender组件  
            line.startColor = new Color32(255, 215, 0, 50);
            line.endColor = new Color32(255, 215, 0, 50);
            line.SetWidth(lineWidth, lineWidth);//设置宽度  
            i = 0;
            posList.Clear();
            isConcealedOpen = false;
            open = false;
            moved = false;
        }
        if (Input.GetMouseButton(0) && MousePositionDetection() == 0)
        {
            if (clone != null)
                Destroy(clone);
        }
        if (Input.GetMouseButton(0) && MousePositionDetection() != 0)
        {
            if (clone != null)
            {
                i++;
                Vector3 pos = new Vector3();
                line.positionCount = i;//设置顶点数  
                pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15));
                line.SetPosition(i - 1, pos);//设置顶点位置
                if (posList.Count == 0)
                    posList.Add(pos);
                else if (pos != posList[posList.Count - 1])
                {
                    posList.Add(pos);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (clone != null)
            {
                IsCOpen(posList);
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
                if (isConcealedOpen)
                {
                    Debug.Log("YX门");
                    open = true;
                    targetObject = GetClosestObejct(stores, boxes, pigs, centerPoint);
                    targetPos = centerPoint;
                    Debug.Log(targetObject.tag);
                    timer = 0f;
                }
                else
                {
                    Destroy(clone);
                }
            }

        }
        else if (!open)
            Destroy(clone);
        if (isConcealedOpen)
        {
            Attract(targetObject, targetPos);
        }

    }
    private void IsCOpen(List<Vector3> list)
    {
        int xChangeCount = 0;
        int yChangeCount = 0;
        for (int n = 1; n < list.Count - 1; n++)
        {
            int q = n;
            while ((q < list.Count - 1) && ((list[q].y - list[q + 1].y) == 0))
            {
                q++;
            }
            if (q == list.Count - 1)
                ;
            else if ((list[n - 1].y - list[n].y) * (list[q].y - list[q + 1].y) < 0)
            {
                yChangeCount++;
            }
            q = n;
            while ((q < list.Count - 1) && ((list[q].x - list[q + 1].x) == 0))
            {
                q++;
            }
            if (q == list.Count - 1)
                ;
            else if ((list[n - 1].x - list[n].x) * (list[q].x - list[q + 1].x) < 0)
                xChangeCount++;
        }
        if (xChangeCount >= 2 && yChangeCount >= 2)
            isConcealedOpen = true;
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
    private int MousePositionDetection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit)
        {
            if (hit.collider.tag == "CableCar")
                return 2;
            else if (hit.collider.tag == "Canvas")
                return 1;
            else
                return 0;
        }
        else
            return 0;
    }
    private GameObject GetClosestObejct(GameObject[] a, GameObject[] b, GameObject[] c, Vector3 self)
    {
        GameObject game = new GameObject();
        float distance = 100f;
        if (a != null)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (game == null)
                {
                    game = a[i];
                    distance = Vector3.Distance(self, a[i].transform.position);
                    continue;
                }
                if (Vector3.Distance(self, a[i].transform.position) < distance)
                {
                    distance = Vector3.Distance(self, a[i].transform.position);
                    game = a[i];
                }

            }
        }
        if (b != null)
        {
            for (int i = 0; i < b.Length; i++)
            {
                if (game == null)
                {
                    game = b[i];
                    distance = Vector3.Distance(self, b[i].transform.position);
                    continue;
                }
                if (Vector3.Distance(self, b[i].transform.position) < distance)
                {
                    distance = Vector3.Distance(self, b[i].transform.position);
                    game = b[i];
                }

            }
        }
        if (c != null)
        {
            for (int i = 0; i < c.Length; i++)
            {
                if (game == null)
                {
                    game = c[i];
                    distance = Vector3.Distance(self, c[i].transform.position);
                    continue;
                }
                if (Vector3.Distance(self, c[i].transform.position) < distance)
                {
                    distance = Vector3.Distance(self, c[i].transform.position);
                    game = c[i];
                }
            }
        }
        return game;
    }
    private void Attract(GameObject from, Vector3 to)
    {
        switch (from.tag)
        {
            case "Store":
                from.GetComponent<Store>().enabled = false;
                break;
            case "Pig":
                from.GetComponent<Pig>().enabled = false;
                break;
            case "Box":
                from.GetComponent<Box>().enabled = false;
                break;
            default:
                Debug.Log("ERROR");
                break;   
        }
        Vector3 dir = (to - from.transform.position) / Vector3.Distance(from.transform.position, to);
        from.transform.Translate(dir * 5 * Time.deltaTime,Space.World);
        if (Vector3.Distance(from.transform.position, to) < 0.1f)
        {
            moved = true;
        }
        if (moved)
        {
            timer += Time.deltaTime;
            if (timer <= concealedTime)
            {
                from.transform.position = new Vector3(100f, 100f);
            }
            else
            {
                from.transform.position = to;
                switch (from.tag)
                {
                    case "Store":
                        from.GetComponent<Store>().enabled = true;
                        break;
                    case "Pig":
                        from.GetComponent<Pig>().enabled = true;
                        break;
                    case "Box":
                        from.GetComponent<Box>().enabled = true;
                        break;
                    default:
                        Debug.Log("ERROR");
                        break;
                }
                isConcealedOpen = false;
                moved = false;
                timer = 0f;
                Destroy(clone);
            }

        }
        
    }
}