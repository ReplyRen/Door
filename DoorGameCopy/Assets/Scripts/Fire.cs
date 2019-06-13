using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject bullet;
    private float timer = 0f;
    public float restTime = 0.5f;
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > restTime)
        {
            GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
            timer = 0;
        }
    }

}
