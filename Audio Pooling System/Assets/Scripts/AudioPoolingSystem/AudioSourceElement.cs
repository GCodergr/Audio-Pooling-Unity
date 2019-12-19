using UnityEngine;

namespace AudioPoolingSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceElement : MonoBehaviour
    {
        private Transform myTransform;
        private AudioSource audioSource;

        private void Awake()
        {
            myTransform = gameObject.transform;
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            // Check if the audio clip has stopped playing
            if (!audioSource.isPlaying)
            {
                // Disable this game object
                gameObject.SetActive(false);
            }
        }

        public void PlayAudio2D(AudioClip audioClip)
        {
            gameObject.SetActive(true);
            
            audioSource.spatialBlend = 0f; // 2D
            PlayAudioClip(audioClip);
        }

        public void PlayAudio3D(AudioClip audioClip, Vector3 position)
        {
            myTransform.position = position;
            gameObject.SetActive(true);
            
            audioSource.spatialBlend = 1f; // 3D
            PlayAudioClip(audioClip);
        }

        private void PlayAudioClip(AudioClip audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}