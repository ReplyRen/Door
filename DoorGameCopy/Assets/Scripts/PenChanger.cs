using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PenChanger : MonoBehaviour
{
    public GameObject blackholePen;
    public GameObject portalPen;

    public Image currentPen;
    public Sprite blackholeSprite;
    public Sprite portalSprite;
    public Text times;
    private int blackholeTimes;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (blackholePen.activeSelf == true)
            {
                currentPen.sprite = portalSprite;
                blackholePen.SetActive(false);
                portalPen.SetActive(true);
            }
            else
            {
                currentPen.sprite = blackholeSprite;
                blackholePen.SetActive(true);
                portalPen.SetActive(false);
            }
        }
    }
}
