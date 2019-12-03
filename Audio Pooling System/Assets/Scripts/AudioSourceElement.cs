using UnityEngine;

namespace AudioPoolingSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceElement : MonoBehaviour
    {
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            // Check if the audio clip has stopped playing
            if (!audioSource.isPlaying)
            {
                // Disable this game object
                this.gameObject.SetActive(false);
            }
        }
    }
}