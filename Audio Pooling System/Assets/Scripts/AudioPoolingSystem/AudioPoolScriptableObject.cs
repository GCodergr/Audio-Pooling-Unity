using UnityEngine;

namespace AudioPoolingSystem
{
    [CreateAssetMenu(fileName = "audioPoolScriptableObject", menuName = "AudioPooler/AudioPoolScriptableObject", order = 1)]
    public class AudioPoolScriptableObject : ScriptableObject
    {
        public AudioPoolItem[] audioPoolCollection;
    }
}