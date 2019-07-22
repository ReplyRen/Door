using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialcloudController : MonoBehaviour
{
    private float timer;
    private Vector3 targetScale;
    private Vector3 originScale;
    private Vector3 step;
    private bool isLarge;
    public SpriteRenderer Lightning;
    public Transform boxTransform;
    private bool boxFollow; //控制箱子跟随的布尔变量

    public float MarginLeft;
    public float MarginRight;
    private float moveSpeed;
    private bool isForwardRight;
    public bool isDark;

    private void Start()
    {
        isLarge = true;
        targetScale = this.transform.localScale * 0.95f;
        originScale = this.transform.localScale;
        step = (originScale - targetScale) * 0.02f;
        moveSpeed = Screen.width / 400f;

        boxFollow = true;
    }

    private void Update()
    {
        if (isLarge)
        {
            this.transform.localScale -= step;
            if (Lightning != null)
                Lightning.color = new Color(Lightning.color.r, Lightning.color.g, Lightning.color.b, 0.5f);
            if (this.transform.localScale.x <= targetScale.x)
            {
                isLarge = false;
                if(isDark)
                    AudioSource.PlayClipAtPoint(GlobalController._instance.lightningSound, new Vector3(0, 0, 0));
            }
        }
        else
        {
            this.transform.localScale += step;
            if (Lightning != null)
                Lightning.color = new Color(Lightning.color.r, Lightning.color.g, Lightning.color.b, 1f);
            if (this.transform.localScale.x >= originScale.x)
                isLarge = true;
        }
        if(Lightning != null)
            Lightning.gameObject.transform.position = new Vector3(this.transform.position.x + 0.1f, Lightning.gameObject.transform.position.y, Lightning.gameObject.transform.position.z);

        if (isForwardRight)
        {
            this.transform.position = new Vector3(this.transform.position.x + moveSpeed * Time.deltaTime, this.transform.position.y, this.transform.position.z);
            if(this.transform.position.x >= MarginRight)
                isForwardRight = false;
        }
        else if (!isForwardRight)
        {
            this.transform.position = new Vector3(this.transform.position.x - moveSpeed * Time.deltaTime, this.transform.position.y, this.transform.position.z);
            if (this.transform.position.x <= MarginLeft)
                isForwardRight = true;
        }

        if(boxFollow && boxTransform != null)
        {
            boxTransform.position = new Vector3(this.transform.position.x, boxTransform.position.y, boxTransform.position.z);
        }
    }
}
