using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private Text tutorialText;

    void Awake()
    {
        tutorialText = GetComponent<Text>();
    }

    void Start()
    {
        StartCoroutine(HideTutorialMessage());
    }

    private IEnumerator HideTutorialMessage()
    {
        yield return new WaitForSeconds(2f);

        tutorialText.text = "";
    }
}
