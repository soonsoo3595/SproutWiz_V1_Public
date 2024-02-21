using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownSound : MonoBehaviour
{
    public Button farmBtn;
    public Button storeBtn;
    public Button workshopBtn;

    // Start is called before the first frame update
    void Start()
    {
        farmBtn.onClick.AddListener(Farm);
        storeBtn.onClick.AddListener(Store);
        workshopBtn.onClick.AddListener(Workshop);

    }

    public void Farm()
    {
        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("enterFarm");
    }

    public void Store()
    {
        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("enterStore");
    }

    public void Workshop()
    {
        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("enterWorkShop");
    }
}
