using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickFloor : MonoBehaviour
{
    Ray2D[] upRay;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
         InitRay();
        UpRayDetection();
    }
    private void InitRay()
    {
        upRay = new Ray2D[5];
        upRay[0].origin = new Vector2(2.655f, 2.028f);
        upRay[0].direction = Vector2.up;
        for(int i = 1; i < 5; i++)
        {
            upRay[i].origin = upRay[i-1].origin + new Vector2(0.1f, 0f);
            upRay[i].direction = upRay[0].direction;
        }
        DrawRay(upRay, Color.red);
    }
    private void DrawRay(Ray2D[] ray, Color color)
    {
        for (int i = 0; i < ray.Length; i++)
        {
            Debug.DrawRay(ray[i].origin, ray[i].direction, color);
        }
    }
    private void UpRayDetection()
    {
        RaycastHit2D[] upHit = new RaycastHit2D[5];
        for (int i = 0; i < upHit.Length; i++)
        {
            upHit[i] = Physics2D.Linecast(upRay[i].origin, upRay[i].origin + new Vector2(0f, 0.1f));
            if (upHit[i].collider != null)
            {
                if (upHit[i].collider.name == "Stick")
                    upHit[i].collider.gameObject.GetComponent<Stick>().startRotate = false;
            }
        }
    }
}
