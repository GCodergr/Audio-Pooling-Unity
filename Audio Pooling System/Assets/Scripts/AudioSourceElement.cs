using UnityEngine;
using System.Collections;

namespace AudioPoolingSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceElement : MonoBehaviour
    {
        private AudioSource audioSource;

        // Use this for initialization
        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
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