using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private AudioClip clickSound;
    private float timer;
    static bool GameOn;
    private AudioSource audio;

    private void Start()
    {
        clickSound = GlobalController._instance.clickSound;
        audio = GlobalController._instance.GetComponent<AudioSource>();
    }

    public void Load_1()
    {
        SceneManager.LoadScene("1");
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
        audio.pitch = 1f;
        audio.loop = false;
        audio.clip = GlobalController._instance.gamebeginSound;
        audio.Play();
    }

    public void Load_2()
    {
        SceneManager.LoadScene("2");
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
        audio.pitch = 1f;
        audio.loop = false;
        audio.clip = GlobalController._instance.gamebeginSound;
        audio.Play();
    }

    public void Load_3()
    {
        SceneManager.LoadScene("3");
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
        audio.pitch = 1f;
        audio.loop = false;
        audio.clip = GlobalController._instance.gamebeginSound;
        audio.Play();
    }

    public void Load_4()
    {
        SceneManager.LoadScene("4");
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
        audio.pitch = 1f;
        audio.loop = false;
        audio.clip = GlobalController._instance.gamebeginSound;
        audio.Play();
    }

    public void Load_5()
    {
        SceneManager.LoadScene("5");
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
        audio.pitch = 1f;
        audio.loop = false;
        audio.clip = GlobalController._instance.gamebeginSound;
        audio.Play();
    }

    public void Load_6()
    {
        SceneManager.LoadScene("6");
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
        audio.pitch = 1f;
        audio.loop = false;
        audio.clip = GlobalController._instance.gamebeginSound;
        audio.Play();
    }

    public void Load_7()
    {
        SceneManager.LoadScene("7");
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
        audio.pitch = 1f;
        audio.loop = false;
        audio.clip = GlobalController._instance.gamebeginSound;
        audio.Play();
    }

    public void Load_8()
    {
        SceneManager.LoadScene("8");
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
        audio.pitch = 1f;
        audio.loop = false;
        audio.clip = GlobalController._instance.gamebeginSound;
        audio.Play();
    }

    public void Load_9()
    {
        SceneManager.LoadScene("9");
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
        audio.pitch = 1f;
        audio.loop = false;
        audio.clip = GlobalController._instance.gamebeginSound;
        audio.Play();
    }

    public void Load_10()
    {
        SceneManager.LoadScene("10");
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
        audio.pitch = 1f;
        audio.loop = false;
        audio.clip = GlobalController._instance.gamebeginSound;
        audio.Play();
    }

    public void Load_11()
    {
        SceneManager.LoadScene("11");
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
        audio.pitch = 1f;
        audio.loop = false;
        audio.clip = GlobalController._instance.gamebeginSound;
        audio.Play();
    }

    public void Load_12()
    {
        SceneManager.LoadScene("12");
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
        audio.pitch = 1f;
        audio.loop = false;
        audio.clip = GlobalController._instance.gamebeginSound;
        audio.Play();
    }

    public void Load_13()
    {
        SceneManager.LoadScene("13");
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
        audio.pitch = 1f;
        audio.loop = false;
        audio.clip = GlobalController._instance.gamebeginSound;
        audio.Play();
    }

    public void StartGame()
    {
        AudioSource.PlayClipAtPoint(GlobalController._instance.startbuttonSound, new Vector3(0,0,0));

        StartCoroutine(Begin());
    }

    public void ReStart()
    {
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        audio.Play();
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
        SceneManager.LoadScene("Choose");

        audio.clip = GlobalController._instance.gameBgm;
        audio.pitch = 0.7f;
        audio.Pause();
        GameOn = true;
    }

    public void GameOver()
    {
        AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
        Application.Quit();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            timer += Time.deltaTime;
            if (timer >= 5 && GameOn)
            {
                GlobalController._instance.GetComponent<AudioSource>().Play();
                GameOn = false;
                timer = 0;
            }
        }
    }

    IEnumerator Begin()
    {
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        audio.clip = GlobalController._instance.gameBgm;
        audio.pitch = 0.7f;
        audio.volume = 0.1f;
        audio.Pause();
        GameOn = true;
    }
}
