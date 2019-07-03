using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public GameObject Player;
    public float minDistance;
    public float minDistancetoLock;
    private bool isFollowing;
    public float disMargin;
    public float smooth;
    public GameObject Door;
    public GameObject Lock;
    private bool isUnlocking;
    private float resttimer;
    private Vector3 randomPos;

    void Start()
    {
        
    }

    void Update()
    {
        float distoPlayer = (Player.transform.position - this.transform.position).magnitude;
        if (isFollowing == false && distoPlayer < minDistance)
        {
            AudioClip collectSound = GlobalController._instance.turnonSound;
            AudioSource.PlayClipAtPoint(collectSound, new Vector3(0, 0, 0));

            this.transform.localScale = new Vector2(this.transform.localScale.x / 2, this.transform.localScale.y / 2);
            this.transform.eulerAngles = new Vector3(0, 0, -90);
            isFollowing = true;
        }
        else if(isFollowing && distoPlayer > disMargin && isUnlocking == false)
        {
            this.transform.position = Vector2.Lerp(this.transform.position, Player.transform.position, smooth * Time.fixedDeltaTime);
            resttimer = 0;
            randomPos = new Vector3(0, 0, 0);
        }
        else if(isFollowing && distoPlayer <= disMargin && isUnlocking == false)
        {
            float timeTmp = resttimer;
            resttimer = timeTmp + Time.deltaTime;
            if(randomPos == new Vector3(0, 0, 0))
                randomPos = new Vector3(Player.transform.position.x + Random.Range(-2f, 2f), Player.transform.position.y + Random.Range(-2f, 2f), Player.transform.position.z);
            if(resttimer > 1)
            {
                this.transform.position = Vector2.Lerp(this.transform.position, randomPos, smooth * Time.fixedDeltaTime);
                if ((this.transform.position - randomPos).magnitude < 0.05)
                {
                    resttimer = 0;
                    randomPos = new Vector3(0, 0, 0);
                }
                Debug.Log(randomPos);
            }
        }

        float distoLock = (Lock.transform.position - this.transform.position).magnitude;
        if( distoLock < minDistancetoLock)
        {
            this.transform.position = Vector2.Lerp(this.transform.position, Lock.transform.position, smooth * Time.fixedDeltaTime);
            isUnlocking = true;
        }
        if( distoLock < minDistance)
        {
            float aTmp = this.GetComponent<SpriteRenderer>().color.a;
            float disSpeed = 2f;
            Vector4 newColor = new Color(this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.g, this.GetComponent<SpriteRenderer>().color.b, aTmp - disSpeed*Time.deltaTime);
            Lock.GetComponent<SpriteRenderer>().color = newColor;
            Door.GetComponent<SpriteRenderer>().color = newColor;
            this.gameObject.GetComponent<SpriteRenderer>().color = newColor;
            if (aTmp < 0.2)
            {
                AudioClip unlockSound = GlobalController._instance.unlockSound;
                AudioSource.PlayClipAtPoint(unlockSound, new Vector3(0, 0, 0));

                Lock.SetActive(false);
                Door.SetActive(false);
                this.gameObject.SetActive(false);
            }
        }
    }
}
