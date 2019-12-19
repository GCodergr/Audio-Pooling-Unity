using UnityEngine;
using System;

namespace AudioPoolingSystem
{
    [Serializable]
    public class AudioEntity
    {
        public SoundType soundType;
        public AudioClip audioClip;
    }
}