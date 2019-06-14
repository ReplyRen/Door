using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private AudioClip clickSound;

    private void Start()
    {
        clickSound = GlobalController._instance.clickSound;
    }

    public void Load_0()
    {
        SceneManager.LoadScene("0");
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
    }

    public void Load_1()
    {
        SceneManager.LoadScene("1");
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
    }

    public void Load_2()
    {
        SceneManager.LoadScene("2");
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
    }

    public void Load_3()
    {
        SceneManager.LoadScene("3");
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
    }

    public void Load_4()
    {
        SceneManager.LoadScene("4");
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
    }

    public void Load_5()
    {
        SceneManager.LoadScene("5");
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
    }

    public void Load_6()
    {
        SceneManager.LoadScene("6");
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
    }

    public void Load_7()
    {
        SceneManager.LoadScene("7");
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
    }

    public void Load_8()
    {
        SceneManager.LoadScene("8");
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        AudioSource audioSource = GlobalController._instance.GetComponent<AudioSource>();
        audioSource.clip = GlobalController._instance.gameBgm;
        audioSource.pitch = 0.8f;
        audioSource.Play();
    }

    public void ReStart()
    {
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayFlowSound()
    {
        AudioClip flowSound = GlobalController._instance.flowSound;
        AudioSource.PlayClipAtPoint(flowSound, new Vector3(0, 0, 0));
    }

    public void BackMenu()
    {
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
        GlobalController._instance.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(1);
    }
}
