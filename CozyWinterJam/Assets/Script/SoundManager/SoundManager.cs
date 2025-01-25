using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")] public List<Sound> sounds;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }
    
    public void PlayMusic(string name)
    {
        AudioClip clip = sounds.Find(s => s.name == name).clip;
        if (clip == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }
    
    public void PlayGlobalSFX(string name)
    {
        AudioClip clip = sounds.Find(s => s.name == name).clip;
        if (clip == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        
        sfxSource.PlayOneShot(clip);
    }
    
    public void PlayPanSFX(string name)
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (player == null)
        {
            Debug.LogWarning("Player not found!");
            return;
        }
        
        float cameraWidth = Camera.main.orthographicSize * Camera.main.aspect * 2;
        var pan = Mathf.Clamp((player.position.x - gameObject.transform.position.x) / (cameraWidth / 2), -0.7f, 0.7f);
        
        AudioClip clip = sounds.Find(s => s.name == name).clip;
        if (clip == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        
        sfxSource.panStereo = pan;
        sfxSource.PlayOneShot(clip);
    }
    
    public void StopMusic()
    {
        musicSource.Stop();
    }
    
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
