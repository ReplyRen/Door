using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightLiift : MonoBehaviour
{
    private Vector3 bottomPos = new Vector3(7.03f, -6.81f, 0f);
    public Vector3 topPos = new Vector3(7.03f, -3.02f, 0f);
    public bool buttonDown = false;
    public GameObject connecterButton;
    private void FixedUpdate()
    {
        AudioSource audioSource = this.GetComponent<AudioSource>();
        buttonDown = connecterButton.GetComponent<Button>().isDown;

        if (buttonDown == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, topPos, 2 * Time.deltaTime);
            if(audioSource.isPlaying == false)
                audioSource.Play();
            float distance = (this.transform.position - topPos).magnitude;
            if (distance < 0.2)
                audioSource.Pause();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, bottomPos, 2 * Time.deltaTime);
            if (audioSource.isPlaying == false)
                audioSource.Play();
            float distance = (this.transform.position - bottomPos).magnitude;
            if (distance < 0.2)
                audioSource.Pause();
        }
    }
}
