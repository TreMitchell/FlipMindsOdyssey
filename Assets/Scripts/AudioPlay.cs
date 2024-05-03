using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPlay : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    void Start()
    {
        // Add a listener for mouse clicks on this GameObject
        gameObject.GetComponent<Button>().onClick.AddListener(PlayClipOnClick);
    }

    // Method to play the audio clip
    void PlayClipOnClick()
    {
        if (audioClip != null && audioSource != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioClip or AudioSource not set!");
        }
    }
}
