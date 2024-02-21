using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    private Image image;

    public static FadeInOut instance;
    public float duration = 1.0f;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(FadeIn());
    }

    public void SwitchScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alpha = 1 - (elapsedTime / duration);
            image.color = new Color(0f, 0f, 0f, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        image.color = new Color(0f, 0f, 0f, 0f);
    }

    private IEnumerator FadeOut(string sceneName)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alpha = elapsedTime / duration;
            image.color = new Color(0f, 0f, 0f, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        image.color = Color.black;
        SceneManager.LoadScene(sceneName);
    }
}
