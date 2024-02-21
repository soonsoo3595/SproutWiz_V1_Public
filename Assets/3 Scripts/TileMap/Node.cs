using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector2Int coordinates;
    public Element element, preElement;
    public Growth growthStep;
    public bool isActivate;
    public int growPoint;
    public SpriteRenderer cropRenderer;
    public SpriteRenderer tileRenderer;
    public Tile tile;
    private Sprite cropSprite;

    public bool onPreView;
    public bool IsActiveBlock;

    // 이거 어떻게 사용되는지 확인 필요
    public int maxGrowPoint = 5;

    public PlantItem plant; // 현재 이 노드에 있는 식물 정보
    
    public Node(Vector2Int coordinates, bool isActivate)
    {
        element = Element.Non;
        growthStep = Growth.Non;
        this.coordinates = coordinates;
        this.isActivate = isActivate;
        growPoint = 0;

        plant = null;
    }

    public void ChangeCropSprite(Sprite sprite)
    {
        cropRenderer.sprite = sprite;
        cropSprite = sprite;
        cropRenderer.GetComponent<CropSprite>().PlayAni();
    }

    public void ReloadCropSprite()
    {
        cropRenderer.sprite = cropSprite;
    }

    public void ToggleActivate()
    {
        isActivate = !isActivate;
        tile.SetTileColor();
    }

    public void ToggleOnPreView(bool b, Element element)
    {
        onPreView = b;
        preElement = element;
        tile.SetTileColor();
    }

    public void ToggleIsActiveBlock(bool b, Element element)
    {
        IsActiveBlock = b;
        preElement = element;
        tile.SetTileColor();
    }

    public void IncreaseGrowPoint(int point)
    {
        if(growthStep != Growth.Bloom)
        {
            growPoint = Mathf.Clamp(growPoint + point, 0, maxGrowPoint);

            UpdateGrowStep(growPoint);
        }
    }

    public void DecreaseGrowPoint(int point)
    {
        if(growthStep != Growth.Seed)
        {
            growPoint = Mathf.Clamp(growPoint - point, 0, maxGrowPoint);
            if(growPoint <= 0) resetNode();

            UpdateGrowStep(growPoint);
        }
    }

    public void UpdateGrowStep(int currentGrowPoint)
    {
        if (currentGrowPoint <= 0)
            growthStep = Growth.Non;
        else if (currentGrowPoint == 1 && element == Element.Non)
            growthStep = Growth.Seed;
        else if (currentGrowPoint < maxGrowPoint)
            growthStep = Growth.Sprout;
        else if (currentGrowPoint >= maxGrowPoint)
            growthStep = Growth.Bloom;
        else
            Debug.Log("errror");
    }

    public void resetNode()
    {
        element = Element.Non;
        growthStep = Growth.Non;  
        growPoint = 0;
        plant = null;
    }

}
