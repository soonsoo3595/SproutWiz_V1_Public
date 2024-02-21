using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

#region ��ũ�� �� Dictionary
[Serializable]
public class BlockDictionary<T>
{
    public List<BlockData<T>> data;
    public Dictionary<string, T> dict = new Dictionary<string, T>();

    public Dictionary<string, T> getDict()
    {
        for(int i = 0; i < data.Count; i++)
        {
            dict.Add(data[i].key, data[i].value);
        }
        return dict;
    }

}

[Serializable]
public class BlockData<T>
{
    public string key;
    public T value;

}

#endregion

public class BlockImage : MonoBehaviour
{
    private Camera cam;

    public static BlockImage instance;
    public BlockDictionary<Sprite> blockDictionary;

    public int imageWidth = 128;
    public int imageHeight = 128;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        cam = GetComponent<Camera>();
        blockDictionary.dict = blockDictionary.getDict();
    }

    // �� �������� ����� ��ũ���� ����� ��� �̹����� �����ϴ� �۾�
    public void Generate(GameObject prefab)
    {
        if (prefab == null)
            return;

        GameObject instance = Instantiate(prefab);
        instance.transform.localPosition = transform.localPosition;
        instance.transform.localScale = Vector3.one;

        DisableComponents(instance);

        RenderTexture rt = new RenderTexture(imageWidth, imageHeight, 24);
        cam.targetTexture = rt;

        cam.Render();
        instance.SetActive(false);

        Texture2D image = new Texture2D(imageWidth, imageHeight, TextureFormat.RGB24, false);
        RenderTexture.active = rt;
        image.ReadPixels(new Rect(0, 0, imageWidth, imageHeight), 0, 0);
        image.Apply();

        Sprite sprite = Sprite.Create(image, new Rect(0, 0, imageWidth, imageHeight), new Vector2(0.5f, 0.5f));
        blockDictionary.dict.Add(prefab.name, sprite);

        string fileName = $"{prefab.name}.png";
        string filePath = Application.dataPath + "/Resources/ScrollBlock/" + fileName;

        if (!File.Exists(filePath))
        { 
            byte[] bytes = image.EncodeToPNG();
            File.WriteAllBytes(filePath, bytes);
        }
        
        cam.targetTexture = null;
        RenderTexture.active = null;

        Destroy(instance);
    }

    void DisableComponents(GameObject obj)
    {
        foreach (Behaviour behavior in obj.GetComponentsInChildren<Behaviour>())
        {
#pragma warning disable CS0184 // 'is' ���� ������ ���� ������ ������ �ƴմϴ�.
            if (!(behavior is Transform || behavior is MeshFilter || behavior is MeshRenderer))
            {
                behavior.enabled = false;
            }
#pragma warning restore CS0184 // 'is' ���� ������ ���� ������ ������ �ƴմϴ�.
        }
    }

}
