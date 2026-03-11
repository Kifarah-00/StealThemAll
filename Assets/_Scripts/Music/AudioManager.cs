using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (AudioSound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        AudioSound s = Array.Find(sounds, sound => sound.name == name);
        
        // Sicherheitscheck: Wenn s null ist, wurde der Name im Inspector nicht gefunden
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " wurde nicht gefunden!");
            return;
        }
        s.source.Play();
    }
    
    private void OnValidate()
    {
        // Diese Methode wird NUR im Unity-Editor aufgerufen, 
        // wenn du einen Wert im Inspector änderst!
        if (sounds == null) return;

        foreach (AudioSound s in sounds)
        {
            if (s.source != null)
            {
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
            }
        }
    }
    public void Stop(string name)
    {
        AudioSound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound zum Stoppen nicht gefunden: " + name);
            return;
        }
        s.source.Stop();
    }
}