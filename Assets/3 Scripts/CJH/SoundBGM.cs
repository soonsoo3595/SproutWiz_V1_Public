using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBGM : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip titleBGM;
    public AudioClip townBGM;
    public AudioClip farmBGM;
    public AudioClip storeBGM;
    public AudioClip workShopBGM;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBGM()
    {
        audioSource.clip = null;

        string curScene = GameMgr.Instance.curScene;

        if (curScene == "Farm")
            audioSource.clip = farmBGM;
        else if(curScene == "Store")
            audioSource.clip = storeBGM;
        else if(curScene == "Workshop")
            audioSource.clip = workShopBGM;
        else if(curScene == "Town")
            audioSource.clip = townBGM;
        else if(curScene == "Title")
            audioSource.clip = farmBGM;

        audioSource.Play();
    }

    public void NullSoundEffect() => audioSource.clip = null;
    public void StopSoundBGM() => audioSource.Stop();
}
