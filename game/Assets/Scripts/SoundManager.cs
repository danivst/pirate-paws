using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager main;

    public AudioMixer mixer;

    public GameObject Source;
    List<AudioSource> sources = new List<AudioSource>();

    public AudioMixerGroup master;
    public AudioMixerGroup musicMixer;
    [Header("Settings")]
    public bool Ambient;
    public bool Music;
    [Header("Clips")]
    public AudioClip ambientSounds;
    public AudioClip music;

    public AudioClip uiClick;
    public AudioClip levelUp;
    public AudioClip trashSound;
    public AudioClip buySound;
    public AudioClip recycleSound;
    public AudioClip sellSound;
    public AudioClip catchSound;

    private void Awake()
    {
        main = this;
    }

    void Start()
    {
        if (Ambient)
        {
            PlaySound(ambientSounds, true,true);
        }
        if (Music)
        {
            PlaySound(music, true, true);
        }
    }
    public void SetVolume(int master = -1, int music = -1)
    {
        if (master != -1)
        {
            mixer.SetFloat("MasterVolume", master - 80);
        }
        if (music != -1)
        {
            mixer.SetFloat("MusicVolume", music - 80);
        }
         
    }
    float timer = 0f;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            List<AudioSource> sourcesToDestroy = new List<AudioSource>();

            foreach (AudioSource source in sources)
            {
                if (source == null || !source.isPlaying)
                {
                    sourcesToDestroy.Add(source);
                }
            }

            foreach (AudioSource source in sourcesToDestroy)
            {
                if (source != null)
                {
                    Destroy(source.gameObject);
                    sources.Remove(source); 
                }
            }
        }
    }
    void PlaySound(AudioClip clip, bool looping = false, bool isMusic = false)
    {
        GameObject clone = Instantiate(Source, transform);
        AudioSource source = clone.GetComponent<AudioSource>();
        source.loop = looping;
        source.clip = clip;
        if (isMusic)
        {
            source.outputAudioMixerGroup = musicMixer;
        }
        source.Play();
        sources.Add(source);
    }
    public void UiClick()
    {
        PlaySound(uiClick, false);
    }
    public void LevelUpSound()
    {
        PlaySound(levelUp, false);
    }
    public void TrashSound()
    {
        PlaySound(trashSound, false);
    }
    public void BuySound()
    {
        PlaySound(buySound, false);
    }
    public void RecycleSound()
    {
        PlaySound(recycleSound, false);
    }
    public void SellSound()
    {
        PlaySound(sellSound, false);
    }
    public void CatchSound()
    {
        PlaySound(catchSound, false);
    }


}
