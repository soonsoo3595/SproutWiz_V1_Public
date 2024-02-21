using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    TextMeshPro label;
    Node node;

    Vector2Int coordinates = new Vector2Int();
    
    bool debugMode;

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        DisplayCoordinate();
        label.enabled = false;
    }

    private void Start()
    {
        if (Application.isPlaying)
        {
            node = GridManager.instance.GetNode(coordinates);
        }    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            debugMode = !debugMode;

        if (!Application.isPlaying)
        {
            DisplayCoordinate();
            UpdateObjectName();
            label.enabled = true;
        }
        else
        {
            if(debugMode)
            {
                DisplayTileState();
                label.enabled = true;
            }
            else
            {
                label.enabled = false;
            }
        }
    }


    private void DisplayCoordinate()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / 10);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.y / 10);

        label.text = $"{coordinates.x},{coordinates.y}";
    }

    private void DisplayTileState()
    {
        int growPoint = node.growPoint;
        Element element = node.element;

        label.text = $"{coordinates.x},{coordinates.y}\n" +
                      $"Point:{growPoint}\n" +
                      $"E:{element}";
    }

    private void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
