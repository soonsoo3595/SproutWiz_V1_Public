using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;

    Button btn;
    GameMgr gameMgr;

    void Start()
    {
        gameMgr = GameMgr.Instance;

        btn = GetComponent<Button>();
        btn.onClick.AddListener(MoveScene);
    }

    public void MoveScene()
    {
        gameMgr.prevScene = gameMgr.curScene;
        gameMgr.curScene = sceneName;

        GameMgr.Instance.soundBGM.StopSoundBGM();
        FadeInOut.instance.SwitchScene(sceneName);
        // SceneManager.LoadScene(sceneName);
    }

}
