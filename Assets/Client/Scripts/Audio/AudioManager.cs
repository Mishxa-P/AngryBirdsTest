using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    public static AudioManager Singleton { get; private set; }

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            Initialize();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Initialize()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.currentVolume = s.volume;
            s.currentVolume *= PlayerPrefs.GetFloat("GlobalVolume", 0.5f);
            if (s.isMusic)
            {
                s.currentVolume *= PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            }
            else
            {
                s.currentVolume *= PlayerPrefs.GetFloat("SoundsVolume", 0.5f);
            }
            s.source.volume = s.currentVolume;

            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    public void UpdateVolumeSettings()
    {
        foreach (Sound s in sounds)
        {
            s.currentVolume = s.volume;
            s.currentVolume *= PlayerPrefs.GetFloat("GlobalVolume", 0.5f);
            if (s.isMusic)
            {
                s.currentVolume *= PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            }
            else
            {
                s.currentVolume *= PlayerPrefs.GetFloat("SoundsVolume", 0.5f);
            }
            s.source.volume = s.currentVolume;
        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Audio clip: " + name + " has not been found");
            return;
        }
        s.source.volume = s.currentVolume;
        s.source.Play();
    }
    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Audio clip: " + name + " has not been found");
            return false;
        }
        return s.source.isPlaying; 
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Audio clip: " + name + " has not been found");
            return;
        }
        s.source.Stop();
    }
    public void StopAll()
    {
        foreach (Sound s in sounds)
        {
            if (s != null)
            {
                s.source.Stop();
            }
        }
    }
    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Audio clip: " + name + " has not been found");
            return;
        }
        s.source.Pause();
    }
    public void UnPause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Audio clip: " + name + " has not been found");
            return;
        }
        s.source.UnPause();
    }
}
