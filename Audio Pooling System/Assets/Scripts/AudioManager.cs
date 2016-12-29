using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace AudioPoolingSystem
{
    public class AudioManager : MonoBehaviour
    {
        #region Enums
        public enum SoundType
        {
            Switch,
            ComputerPanel,
            Trumpet,
            Exclamation
        }
        #endregion

        #region Fields
        public List<SoundType> soundTypeList;   // Holds all the sound types
        private int soundTypeEnumSize;

        public List<AudioClip> audioClipList;   // Holds all the audio clips

        private List<List<AudioClip>> audioClipTable = new List<List<AudioClip>>();

        public GameObject pooledObject;
        public int pooledAmount = 2;
        public bool willGrow = true;
        private List<GameObject> pooledObjects;
        private List<AudioSource> pooledObjectsAudioSource;    // Store the Audio Source component reference for fast access
        #endregion

        #region Properties
        // Static singleton property
        public static AudioManager Instance
        {
            get;
            private set;
        }
        #endregion

        #region Initialize
        // Use this for initialization
        void Awake()
        {
            // First we check if there are any other instances conflicting
            if (Instance != null && Instance != this)
            {
                // If that is the case, we destroy other instances
                Destroy(gameObject);
            }

            // Here we save our singleton instance
            Instance = this;

            // First of all calculate the size of the enum
            soundTypeEnumSize = Enum.GetNames(typeof(SoundType)).Length;

            AssignAllClipsToAudioClipTable();

            InitializePooledObjects();
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
                // Create a temporarily list, that we will add to the audioClipTable in the apropriate position
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
        /// <returns>The requsted audio clip</returns>
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
            int? nullableIndex = GetPooledObjectIndex();

            if (nullableIndex == null)
            {
                return;
            }

            int index = (int)nullableIndex;

            pooledObjects[index].SetActive(true);
            pooledObjectsAudioSource[index].spatialBlend = 0f;  // 2D
            pooledObjectsAudioSource[index].clip = GetAudioClip(soundType);
            pooledObjectsAudioSource[index].Play();
        }

        /// <summary>
        /// Plays a sound at the requested location
        /// </summary>
        /// <param name="soundType">The requested sound type</param>
        /// <param name="position">World position</param>
        public void PlaySound3D(SoundType soundType, Vector3 position)
        {
            int? nullableIndex = GetPooledObjectIndex();

            if (nullableIndex == null)
            {
                return;
            }

            int index = (int)nullableIndex;

            pooledObjects[index].SetActive(true);
            pooledObjects[index].transform.position = position;

            pooledObjectsAudioSource[index].spatialBlend = 1f;  // 3D
            pooledObjectsAudioSource[index].clip = GetAudioClip(soundType);
            pooledObjectsAudioSource[index].Play();
        }

        #endregion

        #region Pooling
        /// <summary>
        /// Initializes all the Pooled Objects (Audio Source Elements)
        /// </summary>
        private void InitializePooledObjects()
        {
            pooledObjects = new List<GameObject>();
            pooledObjectsAudioSource = new List<AudioSource>();

            for (int i = 0; i < pooledAmount; i++)
            {
                GameObject obj = Instantiate(pooledObject) as GameObject;
                // Add the Audio Source components, for faster access  
                pooledObjectsAudioSource.Add(obj.GetComponent<AudioSource>());  
                obj.transform.parent = this.transform;
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }

        /// <summary>
        /// Returns the index of the first inactive pooled object
        /// </summary>
        /// <returns>The index of the first inactive pooled object</returns>
        public int? GetPooledObjectIndex()
        {
            // Parse all pooled objects
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                // In case the pooled object is null create an pooled object and return it's index
                // Note: This actually is not going to be used, because we initialize 
                // all the pooled objects on Awake method
                if (pooledObjects[i] == null)
                {
                    GameObject obj = Instantiate(pooledObject) as GameObject;
                    pooledObjectsAudioSource.Add(obj.GetComponent<AudioSource>());
                    obj.transform.parent = this.transform;
                    obj.SetActive(false);
                    pooledObjects[i] = obj;
                    return i;
                }
                // If the object is inactive, return it's index
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return i;
                }
            }

            // If the list of the pooledObjects can grow, create a new pooled object and return it's index
            if (willGrow)
            {
                GameObject obj = Instantiate(pooledObject) as GameObject;
                pooledObjectsAudioSource.Add(obj.GetComponent<AudioSource>());
                obj.transform.parent = this.transform;
                pooledObjects.Add(obj);
                obj.SetActive(false);
                return (pooledObjects.Count - 1);
            }

            return null;
        }
        #endregion
    }
}