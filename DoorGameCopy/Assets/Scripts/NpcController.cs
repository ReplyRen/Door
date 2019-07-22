using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NpcController : MonoBehaviour
{
    public string[] contents;
    public Text talkLayout;
    public Image talkBackground;
    static bool firstTalkEnd;
    static bool secondTalkEnd;
    private bool talkStart;
    public GameObject Player;
    private int currentNum;
    public Text reminder;

    public Text times;
    public Sprite blackholeSprite;
    public Sprite concealedSprite;
    public Image currentPen;
    public GameObject blackholePen;
    public GameObject concealedPen;

    private void Start()
    {
        currentNum = 0;
        talkLayout.enabled = false;
        talkBackground.enabled = false;
    }

    private void Update()
    {
        if (Player.GetComponent<GravitationalController>().status == 0)
            talkStart = true;
        if(talkStart && !firstTalkEnd && SceneManager.GetActiveScene().buildIndex == 6)
        {
            talkLayout.enabled = true;
            talkBackground.enabled = true;
            Player.GetComponent<GravitationalController>().enabled = false;
            talkLayout.text = contents[currentNum];
            talkBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(contents[currentNum].Length * 20f, talkBackground.GetComponent<RectTransform>().rect.height);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                reminder.enabled = false;
                currentNum += 1;
                if (currentNum == contents.Length)
                {
                    firstTalkEnd = true;
                    Player.GetComponent<GravitationalController>().enabled = true;
                    talkLayout.enabled = false;
                    talkBackground.enabled = false;

                    currentPen.sprite =blackholeSprite;
                    times.text = "- " + blackholePen.GetComponent<BlackHoldPen>().limitCount;
                }
            }
        }
    }
}
