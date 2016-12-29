using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    private float raycastRange = 20f;
    private Vector3 rayOrigin;

    private Text notificationText;
    private Camera fpsCamera;

    void Awake()
    {
        notificationText = GameObject.Find("Canvas/Notification Text").GetComponent<Text>();
        fpsCamera = GetComponent<Camera>();


    }

    void Start()
    {
        StartCoroutine(HideTutorialMessage());
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
            //if (hit.collider.CompareTag)
            //{

            //}
        }
    }

    private IEnumerator HideTutorialMessage()
    {
        yield return new WaitForSeconds(2f);

        notificationText.text = "";
    }
    

}
