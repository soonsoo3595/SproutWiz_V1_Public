using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    AudioSource audioSource;

    Dictionary<string, AudioClip> audioDictionary = new Dictionary<string, AudioClip>();

    [Header("UI")]
    [SerializeField] AudioClip clickClip;
    [SerializeField] AudioClip dropClip;
    [SerializeField] AudioClip paperClip_s;
    [SerializeField] AudioClip paperClip_l;
    [SerializeField] AudioClip plusClip;
    [SerializeField] AudioClip minusClip;
    [SerializeField] AudioClip sortClip;


    [Header("Farm")]
    [SerializeField] AudioClip enterFarmClip;
    [SerializeField] AudioClip plantClip;
    [SerializeField] AudioClip shovelClip;
    [SerializeField] AudioClip harvestClip;
    [SerializeField] AudioClip growClip;
    [SerializeField] AudioClip dragClip;
    [SerializeField] AudioClip rotateClip;
    [SerializeField] AudioClip checkClip;
    [SerializeField] AudioClip cancelClip;


    [Header("Store")]
    [SerializeField] AudioClip enterStoreClip;
    [SerializeField] AudioClip cashClip;

    [Header("WorkShop")]
    [SerializeField] AudioClip enterWorkShopClip;
    [SerializeField] AudioClip makeScrollClip;

    [Header("Scroll")]
    [SerializeField] AudioClip basicScrollClip;
    [SerializeField] AudioClip fireScrollClip;
    [SerializeField] AudioClip waterScrollClip;
    [SerializeField] AudioClip grassScrollClip;

    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    void Start()
    {
        Allocate();
    }

    public void PlayOneShotSoundEffect(string str)
    {
        Debug.Log(str);

        AudioClip clip;
        bool isContain = audioDictionary.TryGetValue(str, out clip);
        if (isContain)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("No Clip");
        }
    }


    public void NullSoundEffect() => audioSource.clip = null;
    public void StopSoundEffect() => audioSource.Stop();

    public bool IsPlaying() => audioSource.isPlaying;

    public void PlayScroll(ItemData item)
    {
        ScrollItem scroll = item as ScrollItem;

        if (scroll.element == Element.Fire)
        {
            GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("fireScroll");
        }
        else if (scroll.element == Element.Water)
        {
            GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("waterScroll");
        }
        else if (scroll.element == Element.Grass)
        {
            GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("grassScroll");
        }
    }

    public void Allocate()
    {
        // UI
        audioDictionary.Add("click", clickClip);
        audioDictionary.Add("drop", dropClip);
        audioDictionary.Add("paper_s", paperClip_s);
        audioDictionary.Add("paper_l", paperClip_l);
        audioDictionary.Add("plus", plusClip);
        audioDictionary.Add("minus", minusClip);
        audioDictionary.Add("sort", sortClip);


        // Farm
        audioDictionary.Add("enterFarm", enterFarmClip);
        audioDictionary.Add("plant", plantClip);
        audioDictionary.Add("shovel", shovelClip);
        audioDictionary.Add("harvest", harvestClip);
        audioDictionary.Add("grow", growClip);
        audioDictionary.Add("drag", dragClip);
        audioDictionary.Add("rotate", rotateClip);
        audioDictionary.Add("check", checkClip);
        audioDictionary.Add("cancel", cancelClip);


        // Store
        audioDictionary.Add("enterStore", enterStoreClip);
        audioDictionary.Add("cash", cashClip);

        // WorkShop
        audioDictionary.Add("enterWorkShop", enterWorkShopClip);
        audioDictionary.Add("makeScroll", makeScrollClip);

        // Scroll
        audioDictionary.Add("basicScroll", basicScrollClip);
        audioDictionary.Add("fireScroll", fireScrollClip);
        audioDictionary.Add("waterScroll", waterScrollClip);
        audioDictionary.Add("grassScroll", grassScrollClip);
    }

}
