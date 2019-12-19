using UnityEngine;
using System;
using System.Collections.Generic;

namespace AudioPoolingSystem
{
    public class AudioManager : MonoBehaviour
    {
        // TODO: This is going to be used, instead of the soundTypeList and audioClipList lists 
        [SerializeField] private AudioEntityCollection audioEntityCollection;
        
        public List<SoundType> soundTypeList;   // Holds all the sound types
        private int soundTypeEnumSize;

        public List<AudioClip> audioClipList;   // Holds all the audio clips

        private List<List<AudioClip>> audioClipTable = new List<List<AudioClip>>();
        
        private AudioPooler audioPooler;
        
        #region Properties
        // Static singleton property
        public static AudioManager Instance
        {
            get;
            private set;
        }
        #endregion

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
            
            // First of all calculate the size of the enum
            soundTypeEnumSize = Enum.GetNames(typeof(SoundType)).Length;

            AssignAllClipsToAudioClipTable();
        }

        /// <summary>
        /// Assigns all audio clips to the Audio Clip Table based on two lists:
        /// 1) The Sound Type List and 2) The Audio Clip List 
        /// </summary>
        private void AssignAllClipsToAudioClipTable()
        {
            // Iterate through all the enum sound types 
            for (int enumElement = 0; enumElement < soundTypeEnumSize; enumElement++)
            {
                // Create a temporarily list, that we will add to the audioClipTable in the appropriate position
                List<AudioClip> sublist = new List<AudioClip>();

                // Parse all clips in audio clip list
                for (int i = 0; i < audioClipList.Count; i++)
                {
                    // Check if the audio clip that we parse has the sound type we want
                    if (soundTypeList[i] == (SoundType)enumElement)
                    {
                        // If the audio clip belongs to the sound type we want, add it to the temporarily list
                        sublist.Add(audioClipList[i]);
                    }
                }

                // Finally we have to add the sublist to the audioClipTable
                audioClipTable.Add(sublist);
            }
        }
        #endregion

        #region Audio Playback
        /// <summary>
        /// Returns the audio clip for the requested sound type. If there are
        /// more that one audio clips, we get a random one 
        /// </summary>
        /// <param name="soundType">The requested sound type</param>
        /// <returns>The requested audio clip</returns>
        public AudioClip GetAudioClip(SoundType soundType)
        {
            AudioClip wantedAudioClip;

            int tableIndex = (int)soundType;

            int numberOfClips = audioClipTable[tableIndex].Count;

            int selectedAudioClipIndex = UnityEngine.Random.Range(0, numberOfClips);

            //Debug.Log(numberOfClips);


            List<AudioClip> sublist = new List<AudioClip>();
            // Get all the audio clips based on the sound type 
            sublist = audioClipTable[tableIndex];

            wantedAudioClip = audioClipTable[tableIndex][selectedAudioClipIndex];

            return wantedAudioClip;
        }

        /// <summary>
        /// Plays a sound audio clip 2D
        /// </summary>
        /// <param name="soundType">The requested sound type</param>
        public void PlaySound2D(SoundType soundType)
        {
            AudioSourceElement audioSourceElement = audioPooler.GetAvailablePooledObject();

            if (audioSourceElement == null) return;
            
            audioSourceElement.PlayAudio2D(GetAudioClip(soundType));
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

            audioSourceElement.PlayAudio3D(GetAudioClip(soundType), position);
        }
        #endregion
    }
}