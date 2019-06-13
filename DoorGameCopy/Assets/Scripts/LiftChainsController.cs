using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftChainsController : MonoBehaviour
{
    public GameObject[] Chains;
    public float downSpeed;
    private float chainLength;
    private float Counter;
    public float bottomMargin;
    public float topMargin;
    public bool isTriggered;
    private int chainNum;
    public GameObject connectedButton;

    void Start()
    {
        chainLength = Chains[0].GetComponent<SpriteRenderer>().size.y;
        Chains[chainNum].SetActive(true);
    }

    void FixedUpdate()
    {
        isTriggered = connectedButton.GetComponent<Button>().isDown;

        if (isTriggered && this.transform.position.y > bottomMargin)
        {
            float Pos_y = this.transform.position.y;
            this.transform.position = new Vector2(this.transform.position.x, Pos_y - downSpeed);
            Counter += downSpeed;
            if (Counter >= chainLength)
            {
                if (chainNum + 1 != 3) chainNum += 1;
                Chains[chainNum].SetActive(true);
                Counter = 0;
            }

            if (this.GetComponent<AudioSource>().isPlaying == false)
                this.GetComponent<AudioSource>().Play();
        }
        else if (isTriggered == false && this.transform.position.y < topMargin)
        {
            float Pos_y = this.transform.position.y;
            this.transform.position = new Vector2(this.transform.position.x, Pos_y + downSpeed);
            Counter += downSpeed;
            if (Counter >= chainLength)
            {
                if (chainNum != 0)
                {
                    Chains[chainNum].SetActive(false);
                    chainNum -= 1;
                }
                Counter = 0;
            }

            if (this.GetComponent<AudioSource>().isPlaying == false)
                this.GetComponent<AudioSource>().Play();
        }
        else
        {
            Counter = 0;
            if (this.GetComponent<AudioSource>().isPlaying == true)
                this.GetComponent<AudioSource>().Pause();
        }
    }
}
