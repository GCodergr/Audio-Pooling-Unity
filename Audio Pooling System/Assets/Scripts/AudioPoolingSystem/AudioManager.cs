using UnityEngine;
using System.Collections.Generic;

namespace AudioPoolingSystem
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] 
        private AudioEntityCollection audioEntityCollection;
        
        private Dictionary<SoundType, List<AudioClip>> audioClipsBySoundType = new Dictionary<SoundType, List<AudioClip>>(new SoundTypeComparer());
        
        private AudioPooler audioPooler;
        
        // Static singleton property
        public static AudioManager Instance
        {
            get;
            private set;
        }
        
        #region Initialize

        private void Awake()
        {
            // First we check if there are any other instances conflicting
            if (Instance != null && Instance != this)
            {
                // If that is the case, we destroy other instances
                Destroy(gameObject);
            }

            // Here we save our singleton instance
            Instance = this;

            audioPooler = FindObjectOfType<AudioPooler>(); 
            
            MapAudioClipsToSoundType();
        }

        /// <summary>
        /// Maps all audio clips to their sound type 
        /// </summary>
        private void MapAudioClipsToSoundType()
        {
            // Iterate through all the audio entities 
            for (int i = 0; i < audioEntityCollection.audioEntities.Length; i++)
            {
                AddAudioClipToDictionary(audioEntityCollection.audioEntities[i].soundType,
                                         audioEntityCollection.audioEntities[i].audioClip);
            }
        }

        private void AddAudioClipToDictionary(SoundType soundType, AudioClip audioClip)
        {
            if (audioClipsBySoundType.ContainsKey(soundType))
            {
                List<AudioClip> audioClips = audioClipsBySoundType[soundType];
                if (audioClips.Contains(audioClip) == false)
                {
                    audioClips.Add(audioClip);
                }
            }
            else
            {
                List<AudioClip> audioClips = new List<AudioClip>();
                audioClips.Add(audioClip);
                audioClipsBySoundType.Add(soundType, audioClips);
            }
        }
        
        #endregion

        #region Audio Playback
        /// <summary>
        /// Returns a random audio clip, given the sound type.
        /// </summary>
        /// <param name="soundType">The requested sound type</param>
        /// <returns>The requested audio clip</returns>
        private AudioClip GetRandomAudioClip(SoundType soundType)
        {
            AudioClip requestedAudioClip = null;
            
            if (audioClipsBySoundType.TryGetValue(soundType, out List<AudioClip> audioClips))
            {
                int selectedAudioClipIndex = Random.Range(0, audioClips.Count);
                
                requestedAudioClip = audioClips[selectedAudioClipIndex];
            }
            
            return requestedAudioClip;
        }

        /// <summary>
        /// Plays a sound audio clip 2D
        /// </summary>
        /// <param name="soundType">The requested sound type</param>
        public void PlaySound2D(SoundType soundType)
        {
            AudioSourceElement audioSourceElement = audioPooler.GetAvailablePooledObject();

            if (audioSourceElement == null) return;
            
            audioSourceElement.PlayAudio2D(GetRandomAudioClip(soundType));
        }

        /// <summary>
        /// Plays a sound at the requested location
        /// </summary>
        /// <param name="soundType">The requested sound type</param>
        /// <param name="position">World position</param>
        public void PlaySound3D(SoundType soundType, Vector3 position)
        {
            AudioSourceElement audioSourceElement = audioPooler.GetAvailablePooledObject();

            if (audioSourceElement == null) return;

            audioSourceElement.PlayAudio3D(GetRandomAudioClip(soundType), position);
        }
        #endregion
    }
}