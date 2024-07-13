using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    AudioSource audioSource;
    List<AudioSource> audioSources = new();
    public int audioSum;
    [HideInInspector]
    public enum AudioName
    {
        shout, noPower, wallTouch, dead, getItem,
    }
    public AudioClip audio_shout;
    public AudioClip audio_noPower;
    public AudioClip audio_wallTouch;
    public AudioClip audio_dead;
    public AudioClip audio_getItem;

    private void Awake()
    {
        Transform audioObj = transform.Find("Audio");
        for (int i = 0; i < audioSum; i++)
        {
            AudioSource audioSource = audioObj.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSources.Add(audioSource);
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioName type)
    {
        AudioSource source = audioSource;
        foreach (AudioSource audio in audioSources)
        {
            if (!audio.isPlaying)
            {
                source = audio;
                break;
            }
        }
        source.clip = type switch
        {
            AudioName.shout => audio_shout,
            AudioName.noPower => audio_noPower,
            AudioName.dead => audio_dead,
            AudioName.wallTouch => audio_wallTouch,
            AudioName.getItem => audio_getItem,
            _ => null,
        };
        if (type == AudioName.wallTouch)
        {
            source.volume = 1;
            source.pitch = Random.Range(1.0f, 2.0f);
        }
        else if (type == AudioName.noPower)
        {
            source.volume = 0.1f;
            source.pitch = 1;
        }
        else
        {
            source.volume = 1;
            source.pitch = 1;
        }
        source.Play();
    }
}
