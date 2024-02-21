using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    public static GameMgr Instance = null;

    [Header("Audio")]
    public SoundEffect soundEffect;
    public SoundBGM soundBGM;

    [Header("Library")]
    [SerializeField] private PlantLibrary plants;
    public static PlantLibrary Plants => Instance.plants;

    [Header("Material")]
    [SerializeField] private MaterialLibrary materials;
    public static MaterialLibrary Materials => Instance.materials;

    [Header("Product")]
    [SerializeField] private ProductLibrary products;
    public static ProductLibrary Products => Instance.products;

    [Header("Scene")]
    public string curScene = null;
    public string prevScene = null;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this) Destroy(gameObject);

        curScene = SceneManager.GetActiveScene().name;

    }

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(BackMgr.instance != null && BackMgr.instance.st.Count > 0) 
            {
                BackMgr.instance.Pop();
            }
            else
            {
                if(curScene == "Town" || prevScene == "")
                {
                // 게임 종료할 수 있게
                Debug.Log("게임종료");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
                }
                else
                {
                    ChangeScene changeScene = FindObjectOfType<ChangeScene>();
                    changeScene.sceneName = prevScene;
                    changeScene.MoveScene();
                }
            }
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void SaveData()
    {

    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        soundBGM.PlayBGM();
    }

    public void GameEixt()
    {
        Application.Quit();
    }
}
