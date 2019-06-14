using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject BHPen;
    public GameObject ProPen;
    public void BhButton()
    {
        BHPen.SetActive(true);
        ProPen.SetActive(false);
    }
    public void ProButton()
    {
        BHPen.SetActive(false);
        ProPen.SetActive(true);
    }
}
