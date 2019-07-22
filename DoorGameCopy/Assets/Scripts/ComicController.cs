using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicController : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            this.GetComponent<AudioSource>().pitch = 0.7f;
        else if (SceneManager.GetActiveScene().buildIndex == 16)
            GlobalController._instance.GetComponent<AudioSource>().clip = null;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else if (SceneManager.GetActiveScene().buildIndex == 16)
            {
                SceneManager.LoadScene("StartMenu");
                AudioSource audio = GlobalController._instance.GetComponent<AudioSource>();
                audio.pitch = 1f;
                audio.volume = 0.5f;
                audio.clip = GlobalController._instance.forestBgm;
                audio.Play();
            }
        }
    }
}
