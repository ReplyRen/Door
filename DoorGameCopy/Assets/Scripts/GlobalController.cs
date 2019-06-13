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

    private void Awake()
    {
        if(_instance == null)
        {
            DontDestroyOnLoad(this);
            _instance = this;
        }
    }
}
