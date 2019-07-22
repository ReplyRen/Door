using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letbHPen : MonoBehaviour
{
    private LineRenderer cableline;
    private List<Vector3> posList = new List<Vector3>();
    private List<Vector3> bHPosList = new List<Vector3>();
    public GameObject bHPen;
    private enum vec3 { top, bottom, left, right, center }

    // Update is called once per frame
    void Update()
    {
        cableline = bHPen.GetComponent<BlackHoldPen>().cableline;
        if (cableline == null) return;
        posList = bHPen.GetComponent<BlackHoldPen>().posList;
        bHPosList = bHPen.GetComponent<BlackHoldPen>().bHPosList;
        int cntPoints = cableline.positionCount;
        Vector3 middlePoint = new Vector3(0, 0, 0);
        for (int j = 0; j < cntPoints; j++)
        {
            middlePoint += cableline.GetPosition(j);
        }
        middlePoint /= cntPoints;
        var cablecar = GameObject.FindWithTag("CableCar");
        var cablecarfa = cablecar.transform.parent.gameObject;
        var cablecarpos = cablecarfa.transform.TransformPoint(cablecar.transform.localPosition);
        Vector3 disVec = cablecarpos - middlePoint; disVec.z = 0;
//        Debug.Log(cablecarpos + " " + disVec);
        for (int j = 0; j < cntPoints; j++)
        {
            cableline.SetPosition(j, cableline.GetPosition(j) + disVec);
        }
        middlePoint = new Vector3(0, 0, 0);
        for (int j = 0; j < posList.Count; j++)
            middlePoint += posList[j];
        middlePoint /= posList.Count;
        disVec = cablecarpos - middlePoint; disVec.z = 0;
        for (int j = 0; j < posList.Count; j++)
            posList[j] += disVec;
        Vector3 centerPoint = GetPoint(posList, vec3.center);
        bHPosList[bHPosList.Count - 1] = centerPoint;
    }

    private void FixedUpdate()
    {

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
}
