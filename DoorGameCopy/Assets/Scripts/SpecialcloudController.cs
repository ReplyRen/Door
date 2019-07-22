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
    private bool boxFollow;

    public float MarginLeft;
    public float MarginRight;
    private float moveSpeed;
    private bool isForwardRight;

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
            if(Lightning != null)
                Lightning.color = new Color(Lightning.color.r, Lightning.color.g, Lightning.color.b, 0.5f);
            if (this.transform.localScale.x <= targetScale.x)
                isLarge = false;
        }
        else
        {
            this.transform.localScale += step;
            if(Lightning != null)
                Lightning.color = new Color(Lightning.color.r, Lightning.color.g, Lightning.color.b, 1f);
            if (this.transform.localScale.x >= originScale.x)
                isLarge = true;
        }
        if(Lightning != null)
            Lightning.gameObject.transform.position = new Vector3(this.transform.position.x, Lightning.gameObject.transform.position.y, Lightning.gameObject.transform.position.z);

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
    }
}
