using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
    public static GlobalController _instance;
    public AudioClip gameBgm;
    public AudioClip clickSound;
    public AudioClip turnonSound;
    public AudioClip crashSound;
    public AudioClip flowSound;
    public AudioClip unlockSound;
    public AudioClip springSound;
    public AudioClip translateSound;
    public AudioClip jumpSound;
    public AudioClip deadSound;
    public AudioClip midfalldownSound;
    public AudioClip bigfalldownSound;
    public AudioClip startbuttonSound;
    public AudioClip lightningSound;
    public AudioClip gamebeginSound;
    public AudioClip forestBgm;

    private void Awake()
    {
        if(_instance == null)
        {
            DontDestroyOnLoad(this);
            _instance = this;
        }
    }
}
