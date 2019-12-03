using UnityEngine;
using AudioPoolingSystem;

namespace ExampleScene
{
    public class Button : MonoBehaviour
    {
        public SoundType sound;

        public Transform speaker;

        public void PlaySound()
        {
            AudioManager.Instance.PlaySound3D(sound, speaker.position);
        }
    }
}
