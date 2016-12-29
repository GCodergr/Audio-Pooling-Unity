using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioPoolingSystem;

public class Button : MonoBehaviour
{
    public AudioManager.SoundType sound;

    public void PlaySound()
    {
        Debug.Log(sound.ToString() +  " is playing");
    }

}
