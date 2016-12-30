using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioPoolingSystem;

public class Button : MonoBehaviour
{
    public AudioManager.SoundType sound;

    public Transform speaker;

    public void PlaySound()
    {
        AudioManager.Instance.PlaySound3D(sound, speaker.position);
    }

}
