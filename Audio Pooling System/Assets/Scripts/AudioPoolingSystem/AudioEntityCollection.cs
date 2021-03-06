﻿using UnityEngine;

namespace AudioPoolingSystem
{
    [CreateAssetMenu(fileName = "AudioEntityCollection", menuName = "AudioPooler/EntityCollection", order = 1)]
    public class AudioEntityCollection : ScriptableObject
    {
        public AudioEntity[] audioEntities;
    }
}