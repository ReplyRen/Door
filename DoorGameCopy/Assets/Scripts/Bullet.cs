using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    Ray2D[] rightRay;
    private BoxCollider2D playercol;//物体的碰撞器
    private float lenth;//物体的长度
    private float height;//物体的高度
    private int h;//宽度上的射线根数
    public float density = 0.1f;//射线密度
    private float verticalForce = 100f;
    private float horizontalForce = 100f;
    private float lastPosX;
    private float newPosX;
    private float timer = 0f;
    private void Start()
    {
        newPosX = transform.position.x;
        lastPosX = newPosX;
    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        newPosX = transform.position.x;

            playercol = gameObject.GetComponent<BoxCollider2D>();
            height = playercol.bounds.size.y;
            h = (int)(height / density);
            InitRay();//初始化射线 

            if ((newPosX - lastPosX) < 0)
                horizontalForce = -Mathf.Abs(horizontalForce);
            if ((newPosX - lastPosX) > 0)
                horizontalForce = Mathf.Abs(horizontalForce);

            VerticalRayDetection();
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        lastPosX = newPosX;
    }
    public void InitRay()
    {
        rightRay = new Ray2D[h];
        InitRayDir(rightRay, Vector2.right);
        float interval = height / (rightRay.Length - 1);
        rightRay[0].origin = transform.position + new Vector3(lenth / 2, -height / 2, 0f) + new Vector3(0.53f, 0f, 0f);
        rightRay[rightRay.Length - 1].origin = transform.position + new Vector3(lenth / 2, height / 2, 0f) + new Vector3(0.53f, 0f, 0f);
        for (int i = 1; i < rightRay.Length - 1; i++)
        {
            rightRay[i].origin = rightRay[i - 1].origin + new Vector2(0f, interval);
        }
        DrawRay(rightRay, Color.red);

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
    private void VerticalRayDetection()
    {
        RaycastHit2D[] rightHit = new RaycastHit2D[h];
        for (int i = 0; i < rightHit.Length; i++)
        {
            rightHit[i] = Physics2D.Linecast(rightRay[i].origin, rightRay[i].origin + new Vector2(0.1f, 0f));
            if (rightHit[i].collider != null)
            {
                if(rightHit[i].collider.tag!="Canvas")
                    Destroy(gameObject);
                if(rightHit[i].collider.tag ==  "Player" || rightHit[i].collider.tag == "Box" || rightHit[i].collider.tag == "Pig" || rightHit[i].collider.tag == "Store")
                {
                    rightHit[i].collider.gameObject.GetComponent<Force>().horizontalSpeed = horizontalForce;
                    rightHit[i].collider.gameObject.transform.position += new Vector3(0, 0.1f, 0);
                    if (rightHit[i].collider.tag == "Player")
                        rightHit[i].collider.gameObject.GetComponent<GravitationalController>().verticalSpeed = verticalForce;
                    else if (rightHit[i].collider.tag == "Box")
                        rightHit[i].collider.gameObject.GetComponent<Box>().verticalSpeed = verticalForce;
                    else if (rightHit[i].collider.tag == "Pig")
                        rightHit[i].collider.gameObject.GetComponent<Pig>().verticalSpeed = verticalForce;
                    else if (rightHit[i].collider.tag == "Store")
                        rightHit[i].collider.gameObject.GetComponent<Store>().verticalSpeed = verticalForce;
                }
                    

            }
        }

    }
}
