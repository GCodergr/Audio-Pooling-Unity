using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    private float raycastRange = 3f;
    private Vector3 rayOrigin;

    private Text raycastText;
    private Camera fpsCamera;

    void Awake()
    {
        raycastText = GameObject.Find("Canvas/Raycast Text").GetComponent<Text>();
        fpsCamera = GetComponent<Camera>();
    }



    void Update()
    {
        CheckForButton();
    }

    private void CheckForButton()
    {
        rayOrigin = fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, fpsCamera.transform.forward, out hit, raycastRange))
        {
            if (hit.collider.CompareTag("Button"))
            {
                DisplayButtonMessage(hit.collider.name);
            }
            else
            {
                ClearButtonMessage();
            }
        }
        else
        {
            ClearButtonMessage();
        }
    }

    #region Raycast Text


    private void DisplayButtonMessage(string buttonName)
    {
        raycastText.text = "Press (E) to activate " + buttonName + " sound";
    }

    private void ClearButtonMessage()
    {
        raycastText.text = "";
    }       
    #endregion

}
