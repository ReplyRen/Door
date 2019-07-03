using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PenChanger : MonoBehaviour
{
    public GameObject blackholePen;
    public GameObject portalPen;

    public Image currentPen;
    public Sprite blackholeSprite;
    public Sprite portalSprite;
    public Text times;
    private int blackholeTimes;

    private void Start()
    {
        blackholeTimes = blackholePen.GetComponent<BlackHoldPen>().limitCount;
        times.text = "- " + blackholeTimes;
    }

    private void Update()
    {
        blackholeTimes = blackholePen.GetComponent<BlackHoldPen>().limitCount - blackholePen.GetComponent<BlackHoldPen>().usageCount;
        if(blackholePen.activeSelf)
            times.text = "- " + blackholeTimes;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (blackholePen.activeSelf == true)
            {
                currentPen.sprite = portalSprite;
                blackholePen.SetActive(false);
                portalPen.SetActive(true);
                times.text = "- " + "∞";
            }
            else
            {
                currentPen.sprite = blackholeSprite;
                blackholePen.SetActive(true);
                portalPen.SetActive(false);
                times.text = "- " + blackholeTimes;
            }
        }
    }
}
