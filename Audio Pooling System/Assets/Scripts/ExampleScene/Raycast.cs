using UnityEngine;
using UnityEngine.UI;

namespace ExampleScene
{
    public class Raycast : MonoBehaviour
    {
        private float raycastRange = 3f;
        private Vector3 rayOrigin;

        private Text raycastText;
        private Camera fpsCamera;

        private void Awake()
        {
            raycastText = GameObject.Find("Canvas/Raycast Text").GetComponent<Text>();
            fpsCamera = GetComponent<Camera>();
        }

        #region Update methods   

        private void Update()
        {
            RaycastCheck();
        }

        private void RaycastCheck()
        {
            rayOrigin = fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, fpsCamera.transform.forward, out hit, raycastRange))
            {
                if (hit.collider.CompareTag("Button"))
                {
                    DisplayButtonMessage(hit.collider.name);

                    CheckForButtonActivation(hit.collider);
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

        private void CheckForButtonActivation(Collider hitCollider)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Button buttonScript = hitCollider.GetComponent<Button>();
                buttonScript.PlaySound();
            }
        }

        #endregion

        #region Raycast Text

        private void DisplayButtonMessage(string buttonName)
        {
            raycastText.text = $"Press (E) to activate {buttonName} sound";
        }

        private void ClearButtonMessage()
        {
            raycastText.text = "";
        }

        #endregion
    }
}