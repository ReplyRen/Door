using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letRedPen : MonoBehaviour
{
    private LineRenderer cableline;
    private List<Vector3> posList = new List<Vector3>();
    private List<Vector3> portalList = new List<Vector3>();
    public GameObject proPen;
    private int cableProtalID = -1;
    private enum vec3 { top, bottom, left, right, center }


    void Update()
    {
        cableline = proPen.GetComponent<ProtalPen>().cableline;
        if (cableline == null) return;
        posList = proPen.GetComponent<ProtalPen>().posList;
        portalList = proPen.GetComponent<ProtalPen>().portalList;
        cableProtalID = proPen.GetComponent<ProtalPen>().cableProtalID;
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
        //Debug.Log(cablecarpos + " " + disVec);
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
        var centerPoint = GetPoint(posList, vec3.center);
        if(cableProtalID != -1) portalList[cableProtalID] = centerPoint;
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
