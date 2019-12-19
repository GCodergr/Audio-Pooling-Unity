using UnityEngine;
using System.Collections.Generic;

namespace AudioPoolingSystem
{
    public class AudioPooler : MonoBehaviour
    {
        [SerializeField]
        private int pooledAmount = 2;
        
        [SerializeField]
        private bool willGrow = true;
           
        [SerializeField]
        private GameObject pooledObjectPrefab;

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
                GameObject obj = Instantiate(pooledObjectPrefab);
                
                // Add the Audio Source components, for faster access  
                audioSourceElements.Add(obj.GetComponent<AudioSourceElement>());  
                obj.transform.parent = gameObject.transform;
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
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

            // TODO: Add the grow mechanism

            return null;
        }
    }
}