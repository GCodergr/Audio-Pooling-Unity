using System;
using UnityEngine;

namespace AudioPoolingSystem
{
    [Serializable]
    public class AudioPoolItem
    {
        public SoundType soundType;
        public AudioClip audioClip;
        public int amountToPool;
        public bool willGrow;
    }
}