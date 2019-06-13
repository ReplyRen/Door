using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestinationController : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject player;
    public Vector3 changeSpeed;
    public float disappearSpeed;
    public GameObject origin;
    private float originRotationSpeed;

    void Start()
    {
        player.GetComponent<GravitationalController>().enabled = false;
        player.transform.position = origin.transform.position;
        player.transform.localScale = new Vector3(0, 0, 0);
        originRotationSpeed = rotationSpeed;
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        float rotationTmp = this.transform.eulerAngles.z;
        this.transform.eulerAngles = new Vector3(0, 0, this.transform.eulerAngles.z - rotationSpeed);

        float distance = (this.transform.position - player.transform.position).magnitude;
        if(distance < 0.5)
        {
            player.GetComponent<GravitationalController>().enabled = false;
            Vector3 tmpScale = player.transform.localScale;
            player.transform.localScale = tmpScale - changeSpeed;
            if(player.transform.localScale.x <= player.transform.localScale.x * 0.1)
            {
                player.SetActive(false);
                rotationSpeed += 1;

                float aTmp = this.GetComponent<SpriteRenderer>().color.a;
                Vector4 newColor = new Color(this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.g, this.GetComponent<SpriteRenderer>().color.b, aTmp - disappearSpeed * Time.deltaTime);
                this.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                if (aTmp < 0.2)
                {
                    this.gameObject.SetActive(false);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    player.transform.localScale = new Vector3(0, 0, 0);
                }
            }
        }

        if (origin)
        {
            rotationTmp = this.transform.eulerAngles.z;
            origin.transform.eulerAngles = new Vector3(0, 0, origin.transform.eulerAngles.z + originRotationSpeed);
            if (player.transform.localScale.x < 0.12)
            {
                Vector3 tmp = player.transform.localScale;
                player.transform.localScale = tmp + changeSpeed;
            }
            else
            {
                player.GetComponent<GravitationalController>().enabled = true;
                originRotationSpeed += 1;

                float aTmp = origin.GetComponent<SpriteRenderer>().color.a;
                Vector4 newColor = new Color(origin.GetComponent<SpriteRenderer>().color.r, origin.GetComponent<SpriteRenderer>().color.g, origin.GetComponent<SpriteRenderer>().color.b, aTmp - disappearSpeed * Time.deltaTime);
                origin.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                if (aTmp < 0.2)
                {
                    Destroy(origin);
                }
            }
        }
    }

}
