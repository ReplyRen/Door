using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    public GameObject[] cloudPrefabs; //云的Prefabs数组
    public Cloud[] Clouds; //云的数组

    private float leftMargin; //屏幕最左端对应的世界坐标
    private float rightMargin; //屏幕最右端
    private float topMargin; //最上端
    private float bottomMargin; //最下端

    private float minSpeed; //最小速度
    private float maxSpeed; //最大速度

    void Start()
    {
        minSpeed = Screen.width / 900f;
        maxSpeed = Screen.width / 600f;
        //初始化最小与最大速度为合适的数值

        leftMargin = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
        rightMargin = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
        topMargin = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
        bottomMargin = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
        //获取各边界对应的世界坐标

        Clouds = new Cloud[4];
        for(int i=0; i<4; i++)
        {
            GameObject objTmp = GameObject.Instantiate(cloudPrefabs[Random.Range(0, 17)], new Vector2(Random.Range(leftMargin, rightMargin), 
                Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y/2, topMargin)), Quaternion.identity);
            float speedTmp = Random.Range(minSpeed, maxSpeed);
            Clouds[i] = new Cloud(objTmp, speedTmp);
        }
        //给Clouds实例化对象并给予一个随机的位置和速度
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Cloud one in Clouds)
        {
            one.cloudObj.transform.position = new Vector2(one.cloudObj.transform.position.x + one.cloudSpeed * Time.fixedDeltaTime, one.cloudObj.transform.position.y);
            if (one.cloudObj.transform.position.x > Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 6 / 5, 0)).x)
            {
                GameObject.Destroy(one.cloudObj);
                one.cloudObj = GameObject.Instantiate(cloudPrefabs[Random.Range(0, 17)],new Vector2(Camera.main.ScreenToWorldPoint(new Vector2(-Screen.width * 1 / 5, 0)).x,
                                Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y / 3, topMargin)),Quaternion.identity);
                one.cloudSpeed = Random.Range(minSpeed, maxSpeed);
            }
        }
        //每一帧控制前进，当x位置大于最右边界时销毁当前对象后重新实例化新的对象，并给予新的速度
    }
}

public class Cloud
{
    public GameObject cloudObj;
    public float cloudSpeed;

    public Cloud(GameObject cloud, float speed)
    {
        this.cloudObj = cloud;
        this.cloudSpeed = speed;
    }
}
//云的类，类中有实例化的GameObject参数与其对应的速度
