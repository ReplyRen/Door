using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicController : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            this.GetComponent<AudioSource>().pitch = 0.7f;
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
