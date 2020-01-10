using UnityEngine;
using System.Collections.Generic;

namespace AudioPoolingSystem
{
    public class AudioPooler : MonoBehaviour
    {
        [SerializeField]
        private GameObject pooledObjectPrefab;
        [SerializeField]
        private int pooledAmount = 2;
        [SerializeField]
        private bool willGrow = true;
        
        private List<GameObject> pooledObjects;
        private List<AudioSourceElement> audioSourceElements;
        
        private void Awake()
        {
            InitializePooledObjects();
        }
        
        /// <summary>
        /// Initializes all the Pooled Objects (Audio Source Elements)
        /// </summary>
        private void InitializePooledObjects()
        {
            pooledObjects = new List<GameObject>();
            audioSourceElements = new List<AudioSourceElement>();
            
            for (int i = 0; i < pooledAmount; i++)
            {
                InstantiatePooledObject();
            }
        }

        private AudioSourceElement InstantiatePooledObject()
        {
            GameObject obj = Instantiate(pooledObjectPrefab, gameObject.transform);

            var audioSourceElement = obj.GetComponent<AudioSourceElement>();
            audioSourceElements.Add(audioSourceElement); // Cache the Audio Source Element
            obj.SetActive(false);
            pooledObjects.Add(obj);

            return audioSourceElement;
        }

        /// <summary>
        /// Returns the index of the first inactive pooled object
        /// </summary>
        /// <returns>The index of the first inactive pooled object</returns>
        public AudioSourceElement GetAvailablePooledObject()
        {
            for (int i = 0; i < audioSourceElements.Count; i++)
            {
                // Is the object inactive?
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return audioSourceElements[i];
                }
            }
            
            // If the pooler can grow, create a new pooled object
            if (willGrow)
            {
                return InstantiatePooledObject();
            }            
            
            return null;
        }
    }
}