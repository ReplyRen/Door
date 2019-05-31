using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if ((float)(Screen.width / (float)Screen.height) >= ((float)768 / (float)1024)) //当设备比较宽的时候
        {
            float size = 768 / 2 / ((float)(Screen.width / (float)Screen.height));
            //if (size<=800) {
            //    size = 800;
            //}
            transform.GetComponent<Camera>().orthographicSize = 512;
        }
        else
        { //当设备比较窄的时候
            float size = 768 / 2 / ((float)(Screen.width / (float)Screen.height));

            transform.GetComponent<Camera>().orthographicSize = size;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
