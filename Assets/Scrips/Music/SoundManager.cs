using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip MainMenuMusic;
    public static AudioClip PlayerJump;
    public static AudioClip MapType1;
    public static AudioClip MapType2;
    public static AudioClip MapType3;
    public static AudioClip Coin;
    private float musicVolume = 1f;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    private void Awake()
    {
        MainMenuMusic = Resources.Load<AudioClip>("Music/Menus/MainMenu");
        PlayerJump = Resources.Load<AudioClip>("Music/Player/Jump");
        Coin = Resources.Load<AudioClip>("Music/Player/CoinSound");
        MapType1 = Resources.Load<AudioClip>("Music/Maps/Type1");
        MapType2 = Resources.Load<AudioClip>("Music/Maps/Type2");
        MapType3 = Resources.Load<AudioClip>("Music/Maps/Type3");
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSrc.volume = musicVolume;
    }

    public static void PlaySound(AudioClip sound)
    {
        audioSrc.PlayOneShot(sound);
    }

    public void SetVolume (float vol)
    {
        musicVolume = vol;
    }
}
