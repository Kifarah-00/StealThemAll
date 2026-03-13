using System;
using System.Collections;
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

        AudioManager.instance.Play("MenuMusic");
    }


    public void Play(string name)
    {
        AudioSound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " wurde nicht gefunden!");
            return;
        }
        s.source.Play();
    }


    public void FadeIn(string name, float duration)
    {
        AudioSound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null) StartCoroutine(FadeSource(s, duration, s.volume, true));
    }

    public void FadeOut(string name, float duration)
    {
        AudioSound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null) StartCoroutine(FadeSource(s, duration, 0, false));
    }

    private IEnumerator FadeSource(AudioSound s, float duration, float targetVolume, bool playOnStart)
    {
        float startVolume = s.source.volume;

        if (playOnStart && !s.source.isPlaying)
        {
            s.source.volume = 0;
            s.source.Play();
        }

        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            s.source.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }

        s.source.volume = targetVolume;
        if (!playOnStart && targetVolume <= 0) s.source.Stop();
    }

    // --- Vorhandene Methoden ---

    public void Stop(string name)
    {
        AudioSound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Stop();
    }

    private void OnValidate()
    {
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
}