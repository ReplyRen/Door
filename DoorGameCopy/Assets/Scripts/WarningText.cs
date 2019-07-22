using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningText : MonoBehaviour
{
    private Color currentCol;
    public float fadeSpeed;
    private bool isFading;

    private void Start()
    {
        currentCol = this.GetComponent<Text>().color;
        isFading = true;
    }

    private void Update()
    {
        if(currentCol.a >= 0.1f && isFading)
        {
            currentCol = new Color(currentCol.r, currentCol.g, currentCol.b, currentCol.a - fadeSpeed);
            isFading = false;
        }
        else if(currentCol.a <= 0.8f && !isFading)
        {
            currentCol = new Color(currentCol.r, currentCol.g, currentCol.b, currentCol.a + fadeSpeed);
            isFading = true;
        }
    }
}
