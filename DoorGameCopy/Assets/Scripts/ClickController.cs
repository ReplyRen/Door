using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    private Vector3 mousePositionOnScreen;
    private Vector3 mousePositionInWorld;

    public GameObject blackholePen;
    public GameObject normalPen;
    public Sprite blackholeSprite;
    public Sprite portalSprite;
    public GameObject changeButton;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePositionOnScreen = Input.mousePosition;
        mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);

        if (Input.GetMouseButtonDown(0))
        {

            if (Physics2D.Linecast(mousePositionInWorld, mousePositionInWorld, 1 << LayerMask.NameToLayer("Changepen")))
            {
                Debug.DrawLine(mousePositionInWorld, mousePositionInWorld, Color.red, 0.5f);
                blackholePen.SetActive(!blackholePen.activeSelf);
                normalPen.SetActive(!normalPen.activeSelf);
                if (changeButton.GetComponent<SpriteRenderer>().sprite == blackholeSprite)
                    changeButton.GetComponent<SpriteRenderer>().sprite = portalSprite;
                else
                    changeButton.GetComponent<SpriteRenderer>().sprite = blackholeSprite;
            }
        }
    }
}
