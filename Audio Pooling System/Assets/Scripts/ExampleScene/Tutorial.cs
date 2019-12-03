using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ExampleScene
{
    public class Tutorial : MonoBehaviour
    {
        private Text tutorialText;

        private readonly WaitForSeconds tutorialMessageLifetime = new WaitForSeconds(2f);

        private void Awake()
        {
            tutorialText = GetComponent<Text>();
        }

        private void Start()
        {
            StartCoroutine(HideTutorialMessage());
        }

        private IEnumerator HideTutorialMessage()
        {
            yield return tutorialMessageLifetime;

            tutorialText.text = "";
        }
    }
}