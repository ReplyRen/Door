using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PenChanger : MonoBehaviour
{
    public GameObject blackholePen;
    public GameObject portalPen;
    public GameObject concealedPen;

    public Image currentPen;
    public Sprite blackholeSprite;
    public Sprite portalSprite;
    public Sprite concealedSprite;
    public Text times;
    private int blackholeTimes;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 3 && SceneManager.GetActiveScene().buildIndex < 6)
            times.text = "- " + "∞";
        else if (SceneManager.GetActiveScene().buildIndex >= 6 && SceneManager.GetActiveScene().buildIndex < 12)
        {
            if (SceneManager.GetActiveScene().buildIndex != 6)
            {
                currentPen.sprite = blackholeSprite;
                blackholeTimes = blackholePen.GetComponent<BlackHoldPen>().limitCount;
                times.text = "- " + blackholeTimes;
            }
            else
            {
                currentPen.sprite = portalSprite;
                times.text = "- " + "∞";
            }
        }
        else if(SceneManager.GetActiveScene().buildIndex >= 12) { }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 6 && SceneManager.GetActiveScene().buildIndex < 12)
        {
            blackholeTimes = blackholePen.GetComponent<BlackHoldPen>().limitCount - blackholePen.GetComponent<BlackHoldPen>().usageCount;

            if (blackholePen.activeSelf)
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
}
