using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineMove : MonoBehaviour
{
    public GameObject obs;
    private GameObject clone;
    private LineRenderer line;
    public float lineWidth = 0.05f;
    float m_Speed = 0.05f;
    private int i;
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clone = (GameObject)Instantiate(obs, obs.transform.position, transform.rotation);//克隆一个带有LineRender的物体   
            line = clone.GetComponent<LineRenderer>();//获得该物体上的LineRender组件  
            line.startColor = Color.black;//设置颜色  
            line.startWidth = lineWidth;//设置宽度  
            i = 0;
        }
        if (Input.GetMouseButton(0))
        {
            if (clone != null)
            {
                i++;
                Vector3 pos = new Vector3();
                line.positionCount = i;//设置顶点数  
                pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15));
                line.SetPosition(i - 1, pos);//设置顶点位置
            }
        }
    }
    private void LateUpdate()
    {
        if (clone == null) return;
        int cnt = line.positionCount;
        for(int i = 0; i < cnt; i++)
        {
            line.SetPosition(i, line.GetPosition(i) +  Vector3.left * m_Speed);
        }
    }
}
