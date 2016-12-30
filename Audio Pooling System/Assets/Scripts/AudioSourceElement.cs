using UnityEngine;
using System.Collections;

namespace AudioPoolingSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceElement : MonoBehaviour
    {
        private AudioSource audioSource;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
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