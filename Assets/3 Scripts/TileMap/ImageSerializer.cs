using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSerializer : MonoBehaviour
{
    [Header("Seed Image")]
    public Sprite seedImage;

    [Header("Tile Images")]
    public Sprite defaultTileImage;

    public void UpdateImage(Node node)
    {
        Sprite sprite = null;
        PlantItem plant = node.plant;

        switch (node.element)
        {
            case Element.Non:
                sprite = null;
                break;
            case Element.Fire:
                sprite = plant.firePlants[GetIndex(node.growPoint)];
                break;
            case Element.Water:
                sprite = plant.waterPlants[GetIndex(node.growPoint)];
                break;
            case Element.Grass:
                sprite = plant.grassPlants[GetIndex(node.growPoint)];
                break;
            default:
                break;
        }

        if (node.growthStep == Growth.Seed)
        {
            sprite = seedImage;
        }

        node.ChangeCropSprite(sprite);
    }

    public int GetIndex(int growpoint)
    {
        switch(growpoint)
        {
            case 1:
            case 2:
            case 3:
                return 0;
            case 4:
            case 5:
                return 1;
            case 6:
            case 7:
                return 2;
            case 8:
            case 9:
                return 3;
        }

        return -1;
    }
}
