using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tile : MonoBehaviour
{
    CropSprite cropSprite;
    TileSprite tileSprite;
    SpriteRenderer tileBackground;
    [SerializeField] SpriteRenderer Marker;

    [SerializeField] Sprite[] enableSprite;
    [SerializeField] Sprite[] disableSprite;

    Node node;

    public Vector2Int coordinates = new Vector2Int();

    public bool OnBlock = false;
    private bool debugMode = false;

    Vector2 enableSpriteSize = new Vector2(0.20f, 0.20f);
    Vector2 disableSpriteSize = new Vector2(0.25f, 0.25f);

    private void Awake()
    {
        cropSprite = GetComponentInChildren<CropSprite>();
        tileSprite = GetComponentInChildren<TileSprite>();
        Marker.enabled = false;

        MakeCoordinates();
    }

    private void Start()
    {
        node = GridManager.instance.GetNode(coordinates);
        node.cropRenderer = cropSprite.GetComponent<SpriteRenderer>();
        node.tileRenderer = tileSprite.GetComponent<SpriteRenderer>();
        node.tile = this;

        tileBackground = GetComponent<SpriteRenderer>();
        node.ReloadCropSprite();

        SetTileColor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            debugMode = !debugMode;
        }

        if (debugMode)
        {
            tileBackground.enabled = true;
            SetTileColor();
        }
        else
        {
            tileBackground.enabled = false;
        }
    }

    private void OnMouseDown()
    {
        if(node.growthStep == Growth.Bloom)
        {
            GridManager.instance.HarvestCrop(node);
            SetTileColor();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnBlock = true;
        SetTileColor();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnBlock = false;
        SetTileColor();
    }

    public void SetTileColor()
    {
        TileMark();

        if (node.isActivate)
        {
            tileSprite.GetComponent<SpriteRenderer>().enabled = true;
        
            if (OnBlock)
            {
                tileBackground.color = Color.red;
            }
            else if(node.onPreView)
            {
                tileBackground.color = Color.yellow;
            }
            else
            {     
                switch (node.element)
                {
                    case Element.Non:
                        tileBackground.color = Color.green;
                        break;
                    case Element.Fire:
                        tileBackground.color = new Color(1f, 0.5f, 0.5f, 1);
                        break;
                    case Element.Water:
                        tileBackground.color = Color.cyan;
                        break;
                    case Element.Grass:
                        tileBackground.color = new Color(0.2f, 0.5f, 0.2f, 1);
                        break;
                    default:
                        break;
                }
            }
        
            Color color = tileBackground.color;
        
            if (node.growthStep == Growth.Bloom)
            {
                color.a = 0.3f;   
            }
            else
            {
                color.a = 0.6f;
            }
        
            tileBackground.color = color;
        }
        else
        {
            tileSprite.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void TileMark()
    {
        if(node.onPreView)
        {
            Marker.enabled = true;

            if (node.IsActiveBlock)
            {
                Marker.transform.localScale = enableSpriteSize;
                Marker.sprite = enableSprite[(int)node.preElement];
            }
            else
            {
                Marker.transform.localScale = disableSpriteSize;
                Marker.sprite = disableSprite[(int)node.preElement];
            }

            Marker.transform.DOScale(Marker.transform.localScale - new Vector3(0.02f, 0.02f, 0), 0.1f);
        }
        else
        {
            Marker.enabled = false;
        }
    }

    private void MakeCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.position.x / 10);
        coordinates.y = Mathf.RoundToInt(transform.position.y / 10);
    }

}
